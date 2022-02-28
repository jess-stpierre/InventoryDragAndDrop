
///Permission to distribute belongs to Jess_StPierre on the Unity Asset Store. If you bought this asset, you have permission to use it in your project.

using UnityEngine;

/// <summary>
/// Do not attach this script to a gameObject, leave it in your project folders, it'll do it's thing
/// It is a Broker that connects any triggered functions (that are coded below), to script that'll recieve those functions
/// </summary>
public class PlayerEventBroker
{
    public delegate void GameObjectEvent(GameObject obj);
    public static event GameObjectEvent OnSelectedEquippedItem;
    public delegate void GameObjectInventoryEvent(GameObject obj, InventoryItem inventoryItem);
    public static event GameObjectInventoryEvent OnSelectedInventoryItem;


    public static void TriggerOnSelectedEquippedItem(GameObject obj)
    {
        OnSelectedEquippedItem?.Invoke(obj);
    }

    public static void TriggerOnSelectedInventoryItem(GameObject obj, InventoryItem inventoryItem)
    {
        OnSelectedInventoryItem?.Invoke(obj, inventoryItem);
    }
}
