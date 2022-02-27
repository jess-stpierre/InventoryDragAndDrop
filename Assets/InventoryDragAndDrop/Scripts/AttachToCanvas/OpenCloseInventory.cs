
///Permission to distribute belongs to Jess_StPierre on the Unity Asset Store. If you bought this asset, you have permission to use it in your project.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseInventory : MonoBehaviour
{
    [Header("Default button for inventory is Q, changeable below")]
    [SerializeField] KeyCode inventoryKey = KeyCode.Q;

	void Update()
    {
        OpenOrCloseInventory();
    }

    private void OpenOrCloseInventory()
    {
        if(Input.GetKeyDown(inventoryKey))
        {
            if(UIEventBroker.TriggerOnCheckInventoryStatus()) //if inventory is open than close it
            {
                UIEventBroker.TriggerOnCloseInventory();
            }
            else 
            {
                UIEventBroker.TriggerOnOpenInventory(); //if inventory i closed than open it
            }
        }
    }
}
