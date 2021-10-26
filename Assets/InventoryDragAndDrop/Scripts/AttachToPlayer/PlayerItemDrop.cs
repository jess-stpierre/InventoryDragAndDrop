///Permission to distribute belongs to Jess_StPierre on the Unity Asset Store. If you bought this asset, you have permission to use it in your project.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemDrop : MonoBehaviour
{
	[Header("0 is left click, 1 is right click, 2 is middle click")]
	[SerializeField] private int mouseButton = 1;

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
		if (UIEventBroker.TriggerOnCheckInventoryStatus() == false && Input.GetMouseButtonDown(mouseButton) && selectedOBJ != null && selectedInventoryItem != null)
		{
			//Code to drop item in Object.cs
			PlayerEventBroker.TriggerOnDropItem(selectedOBJ);

			Debug.Log(selectedOBJ.name);

			//code to remove from inventory
		}
	}
}
