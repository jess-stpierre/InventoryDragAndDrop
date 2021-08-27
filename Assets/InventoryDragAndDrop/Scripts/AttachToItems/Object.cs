
///Permission to distribute belongs to Jess_StPierre on the Unity Asset Store. If you bought this asset, you have permission to use it in your project.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Please attach this script to any gameObject that you want to be pickable and placed inside this inventory system
/// </summary>
public class Object : MonoBehaviour
{
    public InventoryItem inventoryItem;
    public LoadoutItem loadoutItem;

    /// <summary>
    /// Unity Events allow you to hook up any public function in a script
    /// </summary>
    [SerializeField] private UnityEvent showPopup; 
    [SerializeField] private UnityEvent hidePopup;

    private void Awake()
    {
        PlayerEventBroker.OnAttemptPickup += SetOnAttemptPickup;
    }

    private void OnDestroy() 
    {
        PlayerEventBroker.OnAttemptPickup -= SetOnAttemptPickup;
    }

    private void OnDisable() 
    {
        PlayerEventBroker.OnAttemptPickup -= SetOnAttemptPickup;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            //if the player is within the trigger zone collider than show "press e to interact" popup
            showPopup.Invoke();
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            hidePopup.Invoke();
        }
    }

    /// <summary>
    /// For when player attempts to pickup the object it interacted with
    /// </summary>
    private void SetOnAttemptPickup(GameObject obj)
    {
        if(obj == this.gameObject)
        {
            UIEventBroker.TriggerOnAddToSlots(this.gameObject, inventoryItem, loadoutItem);
            this.gameObject.SetActive(false);
            UIEventBroker.TriggerOnHidePopup();
        }
    }
}
