///Permission to distribute belongs to Jess_StPierre on the Unity Asset Store. If you bought this asset, you have permission to use it in your project.

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

	/// <summary>
	/// Gets the item selected in players hand so we have a reference in this script
	/// </summary>
	private void SetOnSelectedInventoryItem(GameObject obj, InventoryItem inventoryItem)
	{
		selectedOBJ = obj;
		selectedInventoryItem = inventoryItem;
	}

	private void Update()
	{
		//Only allow dropping of items when inventory closed + Pressed Button
		if (UIEventBroker.TriggerOnCheckInventoryStatus() == false && Input.GetMouseButtonDown(mouseButton) && selectedOBJ != null && selectedInventoryItem != null)
		{
			selectedOBJ.GetComponent<Object>().DropItem();
			selectedOBJ = null;
		}
	}
}
