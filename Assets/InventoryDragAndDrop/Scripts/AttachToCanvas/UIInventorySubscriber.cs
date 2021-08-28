using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventorySubscriber : MonoBehaviour
{
    [SerializeField] private List<GameObject> slots = new List<GameObject>();
    [SerializeField] private List<GameObject> loadoutSlots = new List<GameObject>();
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private GameObject inventoryParent;

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
            for (int i = 0; i < slots.Count; i++)
            {
                if(slots[i].transform.childCount == 0)
                {
                    //no copy pasting, created a new function with above code to be re-used
                    GameObject objectAdded = Instantiate(objectPrefab, slots[i].transform);
                    objectAdded.GetComponent<Image>().sprite = inventoryItem.itemImage;
                    objectAdded.transform.GetChild(0).gameObject.GetComponent<Text>().text = inventoryItem.itemName;
                    inventoryList.Add(obj);
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
                    GameObject objectAdded = Instantiate(objectPrefab, loadoutSlots[j].transform);
                    objectAdded.GetComponent<Image>().sprite = loadoutItem.itemImage;
                    objectAdded.transform.GetChild(0).gameObject.GetComponent<Text>().text = loadoutItem.itemName;
                    inventoryList.Add(obj);
                    break;
                }
            }
        }
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
