///Permission to distribute belongs to Jess_StPierre on the Unity Asset Store. If you bought this asset, you have permission to use it in your project.

using UnityEngine;

/// <summary>
/// Allows players to use the items they have selected in their hand + hotbar
/// </summary>
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

	/// <summary>
	/// Gets the selected item so we have a reference in this script
	/// </summary>
	private void SetOnSelectedInventoryItem(GameObject obj, InventoryItem inventoryItem)
	{
		selectedOBJ = obj;
		selectedInventoryItem = inventoryItem;
	}

	private void Update()
	{
		//If inventory is closed + pressed button
		if (UIEventBroker.TriggerOnCheckInventoryStatus() == false && Input.GetMouseButtonDown(mouseButton) && selectedOBJ != null && selectedInventoryItem != null)
		{
			selectedOBJ.GetComponent<Object>().currentDurability = selectedOBJ.GetComponent<Object>().currentDurability - 1;

			//use the selected item
			selectedInventoryItem.usage.Invoke();

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
