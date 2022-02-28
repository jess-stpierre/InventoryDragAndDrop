
///Permission to distribute belongs to Jess_StPierre on the Unity Asset Store. If you bought this asset, you have permission to use it in your project.

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the selection of objects on the hotbar
/// </summary>
public class ObjectHotbarSelection : MonoBehaviour
{
	[Header("The order of each element is important and associated with the itemKeys elements")]
	[SerializeField] private List<GameObject> usageHotbar = new List<GameObject>();
	[SerializeField] private GameObject cursor;
	[Header("The order of each element is important and associated with the usageHotBar elements")]
	[SerializeField] private List<KeyCode> itemKeys = new List<KeyCode>();
	[SerializeField] private GameObject deleteItems;

	private int j = 0;

	private void Awake()
	{
		UIEventBroker.OnDraggedItemToSelectedSlot += SetOnDraggedItemToSelectedSlot;
		UIEventBroker.OnRemoveItem += SetOnRemoveItem;
	}

	private void OnDestroy()
	{
		UIEventBroker.OnDraggedItemToSelectedSlot -= SetOnDraggedItemToSelectedSlot;
		UIEventBroker.OnRemoveItem -= SetOnRemoveItem;
	}

	private void Start()
	{
		cursor.transform.position = usageHotbar[0].transform.position;
	}

	private void Update()
	{
		for (int i = 0; i < usageHotbar.Count; i++)
		{
			//hot bar item selection using the quick input keys
			Selection(i, i);
		}
		SelectionUsageMouseScroll();
	}

	/// <summary>
	/// hot bar quick item selection using the input keycodes
	/// </summary>
	private void Selection(int j, int i)
	{
		if (Input.GetKeyDown(itemKeys[j]))
		{
			cursor.transform.position = usageHotbar[i].transform.position;
			SpawnItemOnPlayerSpot(usageHotbar[i]);
		}
	}

	/// <summary>
	/// hot bar item selection using the mouse scroll
	/// </summary>
	private void SelectionUsageMouseScroll()
	{
		if (cursor.transform.position == usageHotbar[j].transform.position)
		{
			if (Input.mouseScrollDelta.y > 0f)
			{
				if (j < (usageHotbar.Count - 1) && j >= 0)
				{
					j++;
					SpawnItemOnPlayerSpot(usageHotbar[j]);
				}
			}
			else if (Input.mouseScrollDelta.y < 0f)
			{
				if (j < (usageHotbar.Count) && j > 0)
				{
					j--;
					SpawnItemOnPlayerSpot(usageHotbar[j]);
				}
			}
			cursor.transform.position = usageHotbar[j].transform.position;
			
		}
	}


	/// <summary>
	/// Triggers event on player for the item to spawn
	/// </summary>
	private void SpawnItemOnPlayerSpot(GameObject equippedSlot)
	{
		GameObject sendThis = null;

		if (equippedSlot.transform.childCount > 0) sendThis = equippedSlot.transform.GetChild(0).gameObject.GetComponent<Object>().inventoryItem.itemPrefab;

		PlayerEventBroker.TriggerOnSelectedEquippedItem(sendThis);
	}

	/// <summary>
	/// If the selected slot is initially empty and we drag an item in it, than we want it to spawn on the player as well...
	/// </summary>
	private void SetOnDraggedItemToSelectedSlot()
	{
		for (int i = 0; i < usageHotbar.Count; i++)
		{
			if(cursor.transform.position == usageHotbar[i].transform.position)
			{
				SpawnItemOnPlayerSpot(usageHotbar[i]);
				break;
			}
		}
	}


	/// <summary>
	/// If item has ran out of durability than we want to remove it from the equippables bar and inventory....
	/// </summary>
	private void SetOnRemoveItem(InventoryItem item)
	{
		for (int i = 0; i < usageHotbar.Count; i++)
		{
			if(usageHotbar[i].transform.childCount > 0 && usageHotbar[i].transform.GetChild(0).gameObject.GetComponent<Object>().inventoryItem == item)
			{
				usageHotbar[i].transform.GetChild(0).gameObject.SetActive(false);
				usageHotbar[i].transform.GetChild(0).gameObject.transform.SetParent(deleteItems.transform);
				break;
			}
		}
	}

}
