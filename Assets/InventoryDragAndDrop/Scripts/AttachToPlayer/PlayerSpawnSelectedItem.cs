
///Permission to distribute belongs to Jess_StPierre on the Unity Asset Store. If you bought this asset, you have permission to use it in your project.

using UnityEngine;

/// <summary>
/// Spawns the selected hotbar item in the players hand in 3D
/// </summary>
public class PlayerSpawnSelectedItem : MonoBehaviour
{
	[SerializeField] private GameObject spawnSpot;

	private void Awake()
	{
		PlayerEventBroker.OnSelectedEquippedItem += SetOnSelectedEquippedItem;
	}

	private void OnDestroy()
	{
		PlayerEventBroker.OnSelectedEquippedItem -= SetOnSelectedEquippedItem;
	}

	/// <summary>
	/// Use object pooling to recycle the items spawn in hand
	/// </summary>
	private void SetOnSelectedEquippedItem(GameObject obj)
	{
		GameObject activeChild = null;
		GameObject newChild = null;
		InventoryItem newInventoryItem = null;

		if(spawnSpot.transform.childCount > 0)
		{
			//find the activeChild
			for (int i = 0; i < spawnSpot.transform.childCount; i++)
			{
				//checks to see if we already have an item in the players hand
				if (spawnSpot.transform.GetChild(i).gameObject.activeInHierarchy) activeChild = spawnSpot.transform.GetChild(i).gameObject;
			}
		}

		//Handles the "spawning" of the newly selected inventory item
		if (activeChild == null || (obj != null && activeChild != null))
		{
			for (int j = 0; j < spawnSpot.transform.childCount; j++)
			{
				if (obj != null && obj.GetComponent<Object>().inventoryItem == spawnSpot.transform.GetChild(j).gameObject.GetComponent<Object>().inventoryItem)
				{
					spawnSpot.transform.GetChild(j).gameObject.SetActive(true);
					newChild = spawnSpot.transform.GetChild(j).gameObject;
					newInventoryItem = spawnSpot.transform.GetChild(j).gameObject.GetComponent<Object>().inventoryItem;
					break;
				}

			}
		}

		//de-activate the old inventory item if its not equal to the new or currently selected item
		if (activeChild != null && activeChild != newChild) activeChild.SetActive(false);

		if(newChild != null && newInventoryItem != null) PlayerEventBroker.TriggerOnSelectedInventoryItem(newChild, newInventoryItem);
	}


}
