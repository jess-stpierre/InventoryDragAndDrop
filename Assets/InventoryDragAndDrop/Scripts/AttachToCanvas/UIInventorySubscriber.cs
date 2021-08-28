
///Permission to distribute belongs to Jess_StPierre on the Unity Asset Store. If you bought this asset, you have permission to use it in your project.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventorySubscriber : MonoBehaviour
{
    [SerializeField] private List<GameObject> inventorySlots = new List<GameObject>();
    [SerializeField] private List<GameObject> loadoutSlots = new List<GameObject>();
    [SerializeField] private GameObject itemSlotPrefab;
    [SerializeField] private GameObject inventoryParent;

    [Header("inventoryList is only for visual testing purposes. DO NOT ADD ANYTHING IN THAT INSPECTOR.")]
    [SerializeField] private List<GameObject> inventoryList = new List<GameObject>();

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

    private void SetOnAddToSlots(GameObject obj, InventoryItem inventoryItem, LoadoutItem loadoutItem)
    {
        if(inventoryItem != null)
        {
            for (int i = 0; i < inventorySlots.Count; i++)
            {
                if(inventorySlots[i].transform.childCount == 0)
                {
                    //no copy pasting, created a new function with above code to be re-used
                    //GameObject objectAdded = Instantiate(objectPrefab, slots[i].transform);
                    //objectAdded.GetComponent<Image>().sprite = inventoryItem.itemImage;
                    //objectAdded.transform.GetChild(0).gameObject.GetComponent<Text>().text = inventoryItem.itemName;
                    //inventoryList.Add(obj);

                    ModularizeAddingToSlots(inventorySlots[i], obj, inventoryItem, loadoutItem);
                    break;
                }
            }  
        }
        else if(loadoutItem != null)
        {
            for (int j = 0; j < loadoutSlots.Count; j++)
            {
                if(loadoutSlots[j].transform.childCount == 0)
                {
                    //no copy pasting, created a new function with above code to be re-used
                    //GameObject objectAdded = Instantiate(objectPrefab, loadoutSlots[j].transform);
                    //objectAdded.GetComponent<Image>().sprite = loadoutItem.itemImage;
                    //objectAdded.transform.GetChild(0).gameObject.GetComponent<Text>().text = loadoutItem.itemName;
                    //inventoryList.Add(obj);

                    ModularizeAddingToSlots(loadoutSlots[j], obj, inventoryItem, loadoutItem);
                    break;
                }
            }
        }
    }

    private void ModularizeAddingToSlots(GameObject slotOBJ, GameObject obj, InventoryItem inventoryItem, LoadoutItem loadoutItem)
	{
        var item = (dynamic)null;

        if (inventoryItem != null) item = inventoryItem;
        if (loadoutItem != null) item = loadoutItem;

        GameObject objectAdded = Instantiate(itemSlotPrefab, slotOBJ.transform);

		Sprite itemImage = item.itemImage;
        objectAdded.GetComponent<Image>().sprite = itemImage;
        objectAdded.transform.GetChild(0).gameObject.GetComponent<Text>().text = item.itemName;

        inventoryList.Add(obj);
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
