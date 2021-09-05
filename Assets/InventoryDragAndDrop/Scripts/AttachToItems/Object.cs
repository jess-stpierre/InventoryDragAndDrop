
///Permission to distribute belongs to Jess_StPierre on the Unity Asset Store. If you bought this asset, you have permission to use it in your project.

using System;
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

    /// <summary>
    /// For once the item is equipped
    /// </summary>
    [HideInInspector] public int currentDurability;
    /// <summary>
    /// Unity Events allow you to hook up any public function in a script
    /// </summary>
    [SerializeField] private UnityEvent showPopup; 
    [SerializeField] private UnityEvent hidePopup;

    private bool popupActive = false;

    private void Awake()
    {
        PlayerEventBroker.OnAttemptPickup += SetOnAttemptPickup;
        UIEventBroker.OnCheckPopupStatus += SetOnCheckPopupStatus;
    }

	private void OnEnable()
	{
        currentDurability = inventoryItem.totalDurability;
    }

	private void OnDestroy() 
    {
        PlayerEventBroker.OnAttemptPickup -= SetOnAttemptPickup;
        UIEventBroker.OnCheckPopupStatus -= SetOnCheckPopupStatus;
        currentDurability = 0;
    }

    private void OnDisable() 
    {
        PlayerEventBroker.OnAttemptPickup -= SetOnAttemptPickup;
        UIEventBroker.OnCheckPopupStatus -= SetOnCheckPopupStatus;
        currentDurability = 0;
    }

	private bool SetOnCheckPopupStatus()
	{
        return popupActive;
	}

	private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            //if the player is within the trigger zone collider than show "press e to interact" popup
            showPopup.Invoke();
            popupActive = true;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            hidePopup.Invoke();
            popupActive = false;
        }
    }

    /// <summary>
    /// For when player attempts to pickup the object it interacted with
    /// </summary>
    private void SetOnAttemptPickup(GameObject obj)
    {
        if(obj == this.gameObject)
        {
            UIEventBroker.TriggerOnAddToSlots(this.gameObject, inventoryItem);
            this.gameObject.SetActive(false);
            hidePopup.Invoke();
        }
    }
}
