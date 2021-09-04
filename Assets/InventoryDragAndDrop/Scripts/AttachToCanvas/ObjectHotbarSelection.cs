
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

	private int j = 0;

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

	private void SpawnItemOnPlayerSpot(GameObject equippedSlot)
	{
		if(equippedSlot.transform.childCount > 0) //&& if the item in the player slot is either empty or different from equipped item selected
		{
			PlayerEventBroker.TriggerOnSelectedEquippedItem(equippedSlot.transform.GetChild(0).gameObject.GetComponent<Object>().inventoryItem.itemPrefab);
			//Pass the item above to a PlayerEventBroker function to trigger an instantiate of the item passed in a script attached to the player
		}
	}

}
