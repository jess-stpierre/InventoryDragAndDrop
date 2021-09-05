
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
		if(spawnSpot.transform.childCount > 0 && spawnSpot.transform.GetChild(spawnSpot.transform.childCount - 1).gameObject.activeInHierarchy) spawnSpot.transform.GetChild(spawnSpot.transform.childCount - 1).gameObject.SetActive(false);

		if (obj == null) return;

		if (spawnSpot.transform.childCount == 0 || (spawnSpot.transform.childCount > 0 && spawnSpot.transform.GetChild(spawnSpot.transform.childCount - 1).gameObject.activeInHierarchy == false) || (obj != null && spawnSpot.transform.GetChild(spawnSpot.transform.childCount - 1).GetComponent<Object>().inventoryItem != obj.GetComponent<Object>().inventoryItem))
		{
			GameObject spawnedOBJ = Instantiate(obj, spawnSpot.transform, false);
			spawnedOBJ.transform.GetChild(0).gameObject.SetActive(false);

			foreach(Collider coll in spawnedOBJ.GetComponents<Collider>())
			{
				if (coll.isTrigger == true) spawnedOBJ.GetComponent<Collider>().enabled = false;
			}
		}
	}


}
