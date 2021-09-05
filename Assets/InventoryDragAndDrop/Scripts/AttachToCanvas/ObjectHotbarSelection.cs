﻿
///Permission to distribute belongs to Jess_StPierre on the Unity Asset Store. If you bought this asset, you have permission to use it in your project.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// RENAME SCRIPT TO OBJECTSELECTION INSTEAD
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
		//DraggedItemToSelectedSlot();
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

	private void SpawnItemOnPlayerSpot(GameObject equippedSlot)
	{
		GameObject sendThis = null;

		if (equippedSlot.transform.childCount > 0) sendThis = equippedSlot.transform.GetChild(0).gameObject.GetComponent<Object>().inventoryItem.itemPrefab;

		PlayerEventBroker.TriggerOnSelectedEquippedItem(sendThis);
	}

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
