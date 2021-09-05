
///Permission to distribute belongs to Jess_StPierre on the Unity Asset Store. If you bought this asset, you have permission to use it in your project.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	private void SetOnSelectedEquippedItem(GameObject obj)
	{
		//Instead of disabling the most recently added child obj prefab, disable the one and only active child

		GameObject activeChild = null;

		if(spawnSpot.transform.childCount > 0)
		{
			//find the activeChild
			for (int i = 0; i < spawnSpot.transform.childCount; i++)
			{
				if (spawnSpot.transform.GetChild(i).gameObject.activeInHierarchy) activeChild = spawnSpot.transform.GetChild(i).gameObject;
			}
		}

		if (activeChild == null || (obj != null &&  activeChild != null && obj.GetComponent<Object>().inventoryItem != activeChild.GetComponent<Object>().inventoryItem)) //if the activeChild SO is not the same as the obj SO
		{
			for (int j = 0; j < spawnSpot.transform.childCount; j++)
			{
				if (obj != null && obj.GetComponent<Object>().inventoryItem == spawnSpot.transform.GetChild(j).gameObject.GetComponent<Object>().inventoryItem)
				{
					spawnSpot.transform.GetChild(j).gameObject.SetActive(true);
					break;
				}

			}
			//DONT INSTANTIATE INSTEAD SPAWN THE CHILD ITEM WITH SAME SO as input obj

			//GameObject spawnedOBJ = Instantiate(obj, spawnSpot.transform, false);
			//spawnedOBJ.transform.GetChild(0).gameObject.SetActive(false);

			//foreach(Collider coll in spawnedOBJ.GetComponents<Collider>())
			//{
			//	if (coll.isTrigger == true) spawnedOBJ.GetComponent<Collider>().enabled = false;
			//}
		}
		if (activeChild != null) activeChild.SetActive(false);
		//if(spawnSpot.transform.childCount > 0 && spawnSpot.transform.GetChild(spawnSpot.transform.childCount - 1).gameObject.activeInHierarchy) spawnSpot.transform.GetChild(spawnSpot.transform.childCount - 1).gameObject.SetActive(false);


	}


}
