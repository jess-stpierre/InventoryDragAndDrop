
///Permission to distribute belongs to Jess_StPierre on the Unity Asset Store. If you bought this asset, you have permission to use it in your project.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventorySubscriber : MonoBehaviour
{
    [SerializeField] private List<GameObject> inventorySlots = new List<GameObject>();
    [SerializeField] private GameObject itemSlotPrefab;
    [SerializeField] private GameObject inventoryParent;
    [SerializeField] private GameObject deletedItemsParent;

    //list all items??

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

    private void SetOnAddToSlots(GameObject obj, InventoryItem inventoryItem)
    {
        if(inventoryItem != null)
        {
            for (int i = 0; i < inventorySlots.Count; i++)
            {
                if(inventorySlots[i].transform.childCount == 0)
                {
                    ModularizeAddingToSlots(inventorySlots[i], obj, inventoryItem);
                    break;
                }
            }  
        }
    }

    private void ModularizeAddingToSlots(GameObject slotOBJ, GameObject obj, InventoryItem inventoryItem)
	{
        GameObject objectAdded = null;

        int matchedObjects = 0;

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
        
        if(matchedObjects == 0) objectAdded = Instantiate(itemSlotPrefab, slotOBJ.transform);

        objectAdded.GetComponent<Object>().inventoryItem = inventoryItem;
		Sprite itemImage = inventoryItem.itemImage;
        objectAdded.GetComponent<Image>().sprite = itemImage;
        objectAdded.transform.GetChild(0).gameObject.GetComponent<Text>().text = inventoryItem.itemName;
    }

    private void SetOnOpenInventory()
    {
        inventoryParent.SetActive(true);
    }

    private void SetOnCloseInventory()
    {
        inventoryParent.SetActive(false);
    }

    private bool SetOnCheckInventoryStatus()
    {
        if(inventoryParent.activeInHierarchy) return true;
        else return false;
    }

}
