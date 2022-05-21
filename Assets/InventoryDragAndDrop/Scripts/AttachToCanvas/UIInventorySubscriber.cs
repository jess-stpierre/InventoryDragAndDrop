
///Permission to distribute belongs to Jess_StPierre on the Unity Asset Store. If you bought this asset, you have permission to use it in your project.

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles inventory related call backs and adding items to the inventory
/// </summary>
public class UIInventorySubscriber : MonoBehaviour
{
    [SerializeField] private List<GameObject> inventorySlots = new List<GameObject>();
    [SerializeField] private GameObject itemSlotPrefab;
    [SerializeField] private GameObject inventoryParent;
    [SerializeField] private GameObject deletedItemsParent;

    void Awake()
    {
        UIEventBroker.OnAddToSlots += SetOnAddToSlots;
        UIEventBroker.OnOpenInventory += SetOnOpenInventory;
        UIEventBroker.OnCloseInventory += SetOnCloseInventory;
        UIEventBroker.OnCheckInventoryStatus += SetOnCheckInventoryStatus;
    }

    private void OnDestroy() 
    {
        UIEventBroker.OnAddToSlots -= SetOnAddToSlots;
        UIEventBroker.OnOpenInventory -= SetOnOpenInventory;
        UIEventBroker.OnCloseInventory -= SetOnCloseInventory;
        UIEventBroker.OnCheckInventoryStatus -= SetOnCheckInventoryStatus;
    }

    /// <summary>
    /// Adds item to inventory by checking which spots are available
    /// </summary>
    private void SetOnAddToSlots(InventoryItem inventoryItem)
    {
        if(inventoryItem != null)
        {
            for (int i = 0; i < inventorySlots.Count; i++)
            {
                if(inventorySlots[i].transform.childCount == 0) //if this slot is available
                {
                    ModularizeAddingToSlots(inventorySlots[i], inventoryItem);
                    break;
                }
            }  
        }
    }

    /// <summary>
    /// More adding to inventory functionality + uses object pooling to save performance
    /// </summary>
    private void ModularizeAddingToSlots(GameObject slotOBJ, InventoryItem inventoryItem)
	{
        GameObject objectAdded = null;

        int matchedObjects = 0;

        // Object pooling
        if(deletedItemsParent.transform.childCount > 0) //check to see if we have this exact item in the deletedItems parent object, if so, than re-se it
        {
			for (int i = 0; i < deletedItemsParent.transform.childCount; i++)
			{
                if(deletedItemsParent.transform.GetChild(i).gameObject.GetComponent<Object>().inventoryItem == inventoryItem)
				{
                    objectAdded = deletedItemsParent.transform.GetChild(i).gameObject;
                    objectAdded.SetActive(true);
                    objectAdded.transform.position = slotOBJ.transform.position;
                    deletedItemsParent.transform.GetChild(i).gameObject.transform.SetParent(slotOBJ.transform);
                    matchedObjects++;
                }
			}
		}
        
        //if we don't have the item in in deletedItems than we spawn it
        if(matchedObjects == 0) objectAdded = Instantiate(itemSlotPrefab, slotOBJ.transform);
        
        //set up item info for proper visual representation in the inventory
        objectAdded.GetComponent<Object>().inventoryItem = inventoryItem;
		Sprite itemImage = inventoryItem.itemImage;
        objectAdded.GetComponent<Image>().sprite = itemImage;
        objectAdded.transform.GetChild(0).gameObject.GetComponent<Text>().text = inventoryItem.itemName;
    }

    private void SetOnOpenInventory()
    {
        inventoryParent.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void SetOnCloseInventory()
    {
        inventoryParent.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private bool SetOnCheckInventoryStatus()
    {
        if(inventoryParent.activeInHierarchy) return true;
        else return false;
    }

}
