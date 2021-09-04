
///Permission to distribute belongs to Jess_StPierre on the Unity Asset Store. If you bought this asset, you have permission to use it in your project.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// RENAME SCRIPT TO OBJECTSELECTION INSTEAD
/// </summary>
public class ObjectUsages : MonoBehaviour
{
	[Header("The order of each element is important and associated with the itemKeys elements")]
	[SerializeField] private List<GameObject> usageHotbar = new List<GameObject>();
	[SerializeField] private GameObject cursor;
	[Header("The order of each element is important and associated with the usageHotBar elements")]
	[SerializeField] private List<KeyCode> itemKeys = new List<KeyCode>();

	//1 - 5 hotbar or mouse scrolling to select next
	private void Start()
	{
		cursor.transform.position = usageHotbar[0].transform.position;
	}

	private void Update()
	{
		for (int i = 0; i < usageHotbar.Count; i++)
		{
			Selection(i, i);
		}
	}

	private void Selection(int j, int i)
	{
		if (Input.GetKeyDown(itemKeys[j])) cursor.transform.position = usageHotbar[i].transform.position;
	}


}
