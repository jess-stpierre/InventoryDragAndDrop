﻿
///Permission to distribute belongs to Jess_StPierre on the Unity Asset Store. If you bought this asset, you have permission to use it in your project.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is used for attempting to check if an object is pickable if it has a trigger collider and a pop up has been shown.
/// </summary>
public class PlayerInventory : MonoBehaviour
{
	[Header("Default button to interact is E, changeable below")]
	[SerializeField] KeyCode interactKey = KeyCode.E;
    [Header(" ")]
    [SerializeField] private bool havePressInteractPopUp = true;
	private GameObject interactedOBJ;

    void Update()
    {
        PickupOBJ();
    }

    private void PickupOBJ()
    {
        if (havePressInteractPopUp && UIEventBroker.TriggerOnCheckPopupStatus()) //returns true if there is a pop up showed on screen saying "press E to interact"
        {
            if (Input.GetKeyDown(interactKey))
            {
                PlayerEventBroker.TriggerOnAttemptPickup(interactedOBJ); //send to all objects with the "Object" script attached that we want to pickup the object in the input
            }
        }
        else if (havePressInteractPopUp == false)
        {
            if (Input.GetKeyDown(interactKey))
            {
                PlayerEventBroker.TriggerOnAttemptPickup(interactedOBJ); //send to all objects with the "Object" script attached that we want to pickup the object in the input
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
}
