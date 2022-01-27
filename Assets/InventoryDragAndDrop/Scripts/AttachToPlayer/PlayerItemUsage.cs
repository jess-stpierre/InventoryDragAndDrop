﻿///Permission to distribute belongs to Jess_StPierre on the Unity Asset Store. If you bought this asset, you have permission to use it in your project.


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemUsage : MonoBehaviour
{
	[Header("0 is left click, 1 is right click, 2 is middle click")]
	[SerializeField] private int mouseButton = 0;

	private GameObject selectedOBJ;
	private InventoryItem selectedInventoryItem;

	private void Awake()
	{
		PlayerEventBroker.OnSelectedInventoryItem += SetOnSelectedInventoryItem;
	}	

	private void OnDestroy()
	{
		PlayerEventBroker.OnSelectedInventoryItem -= SetOnSelectedInventoryItem;
	}

	private void SetOnSelectedInventoryItem(GameObject obj, InventoryItem inventoryItem)
	{
		selectedOBJ = obj;
		selectedInventoryItem = inventoryItem;
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(mouseButton) && selectedOBJ != null && selectedInventoryItem != null) // && UIEventBroker.TriggerOnCheckInventoryStatus() == false)
		{
			selectedOBJ.GetComponent<Object>().currentDurability = selectedOBJ.GetComponent<Object>().currentDurability - 1;

			selectedInventoryItem.usage.Invoke();

			//TODO: Put this in another action and call it only when the usage is done... TRIGGER EVENT??!!
			if(selectedOBJ.GetComponent<Object>().currentDurability <= 0)
			{
				//remove from equippables
				UIEventBroker.TriggerOnRemoveItem(selectedInventoryItem);

				//remove from hand 
				selectedOBJ.SetActive(false);

				//make these null or else well keep on using the item that we deleted
				selectedOBJ = null;
				selectedInventoryItem = null;
			}
		}
	}
}