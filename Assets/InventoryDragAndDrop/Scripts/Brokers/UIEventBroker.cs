
///Permission to distribute belongs to Jess_StPierre on the Unity Asset Store. If you bought this asset, you have permission to use it in your project.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Do not attach this script to a gameObject, leave it in your project folders, it'll do it's thing
/// It is a Broker that connects any triggered functions (that are coded below), to script that'll recieve those functions
/// </summary>
public class UIEventBroker
{
	public delegate bool BoolReturnEvent();
	public static event BoolReturnEvent OnCheckPopupStatus, OnCheckInventoryStatus;
	public delegate void GameObjectEvent(GameObject obj, InventoryItem inventoryItem);
	public static event GameObjectEvent OnAddToSlots;
	public delegate void VoidEvent();
	public static event VoidEvent OnOpenInventory, OnCloseInventory, OnDraggedItemToSelectedSlot;

	public static bool TriggerOnCheckPopupStatus()
	{
		return OnCheckPopupStatus.Invoke();
	}

	public static void TriggerOnOpenInventory()
	{
		OnOpenInventory?.Invoke();
	}

	public static void TriggerOnCloseInventory()
	{
		OnCloseInventory?.Invoke();
	}

	public static void TriggerOnAddToSlots(GameObject obj, InventoryItem inventoryItem)
	{
		OnAddToSlots?.Invoke(obj, inventoryItem);
	}

	public static bool TriggerOnCheckInventoryStatus()
	{
		return OnCheckInventoryStatus.Invoke();
	}

	public static void TriggerOnDraggedItemToSelectedSlot()
	{
		OnDraggedItemToSelectedSlot?.Invoke();
	}
}
