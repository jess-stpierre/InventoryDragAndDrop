﻿
///Permission to distribute belongs to Jess_StPierre on the Unity Asset Store. If you bought this asset, you have permission to use it in your project.

using UnityEngine;

/// <summary>
/// This script is used for attempting to check if an object is pickable if it has a trigger collider and a pop up has been shown.
/// </summary>
public class PlayerInventory : MonoBehaviour
{
	[Header("Default button to interact is E, changeable below")]
	[SerializeField] KeyCode interactKey = KeyCode.E;
    [Header(" ")]
    [Tooltip("If you want players to have a 'press e to interact' popup before they can pickup items than make this true, else make this false")]
    [SerializeField] private bool havePressInteractPopUp = true;

	private GameObject interactedOBJ;

    void Update()
    {
        //if inventory UI is not active than we can allow player to pickup object
        if(UIEventBroker.TriggerOnCheckInventoryStatus() == false) PickupOBJ();
    }

    /// <summary>
    /// Attempt to pickup the object within the trigger zone
    /// </summary>
    private void PickupOBJ()
    {
        if (havePressInteractPopUp && UIEventBroker.TriggerOnCheckPopupStatus()) //returns true if there is a pop up showed on screen saying "press E to interact"
        {
            if (Input.GetKeyDown(interactKey))
            {
                if (interactedOBJ != null)
                {
                    interactedOBJ.GetComponent<Object>().AttemptPickup();
                    interactedOBJ = null;
                }
            }
        }
        else if (havePressInteractPopUp == false && interactedOBJ != null)
        {
            if (Input.GetKeyDown(interactKey))
            {
                interactedOBJ.GetComponent<Object>().AttemptPickup();
                interactedOBJ = null;
            }
        }
    }

    /// <summary>
    /// On Trigger Stay calls every frame when this player hits an object with a trigger collider
    /// </summary>
    private void OnTriggerStay(Collider other)
    {
        interactedOBJ = other.gameObject; //save the value of the object we want to pickup
    }

    /// <summary>
    /// Makes sure that we Hide the "Press E" popup on each Object that we leave the trigger volume from
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == interactedOBJ)
        {
            interactedOBJ.GetComponent<Object>().hidePopup.Invoke();
        }
        else other.gameObject.GetComponent<Object>().hidePopup.Invoke();
    }
}
