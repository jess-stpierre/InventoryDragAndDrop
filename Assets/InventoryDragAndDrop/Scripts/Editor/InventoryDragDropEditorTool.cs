
///Permission to distribute belongs to Jess_StPierre on the Unity Asset Store. If you bought this asset, you have permission to use it in your project.

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor.Events;
using UnityEditor;

/// <summary>
/// Tool to help buyers use this asset 
/// </summary>
[CustomEditor(typeof(InventoryItem))]
public class InventoryDragDropEditorTool : EditorWindow
{
	[MenuItem("Tools/InventoryDragDropEditorTool")]
	public static void ShowWindow()
	{
		InventoryDragDropEditorTool window = GetWindow<InventoryDragDropEditorTool>();
		window.minSize = new Vector2(583f, 531f);
	}

	private GameObject prefab;
	private int numberSubmittedNewOBJ;
	private string itemName;
	private string description;
	private Sprite sprite;
	//private UnityEvent function;
	//SerializedProperty usage;
	private int durability;
	//private GameObject popupHolder;
	private string prefabLocation;
	private string popUpHolderLocation;

	private InventoryItem.ItemType chosenItemType;

	private InventoryItem selectedItem;

	private void OnGUI()
	{
		GUILayout.Space(10);
		GUILayout.BeginVertical();

		GUIStyle bigBold = new GUIStyle();
		bigBold.fontSize = 16;
		bigBold.fontStyle = FontStyle.Bold;

		prefabLocation = "Assets/InventoryDragAndDrop/Scripts/ScriptableObject/InventoryObjects";

		GUILayout.Label("Required fields to create your pickup-able object", bigBold);

		GUILayout.Label("Chosen prefab must have Mesh Filter & Mesh Renderer");
		GUILayout.Label("Default location of prefab is: \n Assets/InventoryDragAndDrop/Prefabs/ItemPrefabs/*ItemName*.prefab");
		prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", prefab, typeof(GameObject), true);

		GUILayout.Label("Default location of scriptable object is: \n Assets/InventoryDragAndDrop/Scripts/ScriptableObject/InventoryObjects");
		prefabLocation = EditorGUILayout.TextField("Location to Store Scriptable Object", prefabLocation);

		itemName = EditorGUILayout.TextField("Item Name", itemName);
		description = EditorGUILayout.TextField("Item Description", description);
		sprite = (Sprite)EditorGUILayout.ObjectField("Item Sprite", sprite, typeof(Sprite), true);
		durability = EditorGUILayout.IntField("Durability", durability);

		popUpHolderLocation = "Assets/InventoryDragAndDrop/Prefabs/ItemChildren/PopUpHolder.prefab";
		GUILayout.Label("Default location of PopUpHolder is: \n Assets/InventoryDragAndDrop/Prefabs/ItemChildren/PopUpHolder.prefab");
		popUpHolderLocation = EditorGUILayout.TextField("Location of PopUpHolder", popUpHolderLocation);

		chosenItemType = (InventoryItem.ItemType)EditorGUILayout.EnumPopup("Item type:", chosenItemType);

		if (GUILayout.Button("Create pickup-able object") == true)
		{
			numberSubmittedNewOBJ++;

			prefab.AddComponent<SphereCollider>();
			prefab.GetComponent<SphereCollider>().isTrigger = true;
			prefab.GetComponent<SphereCollider>().radius = 3f;

			prefab.AddComponent<BoxCollider>();
			prefab.GetComponent<BoxCollider>().isTrigger = false;
			prefab.GetComponent<BoxCollider>().size = Vector3.one;

			prefab.AddComponent<Object>();

			

			InventoryItem newItem = ScriptableObject.CreateInstance<InventoryItem>();
			newItem.name = itemName;
			AssetDatabase.CreateAsset(newItem, $"{prefabLocation}/{itemName}.asset");
			newItem.itemName = itemName;
			newItem.itemDescription = description;
			newItem.itemImage = sprite;
			newItem.currentItemType = chosenItemType;

			
			

			InventoryItem target_0 = newItem;
			switch (chosenItemType)
            {
				case InventoryItem.ItemType.Hit:
					var Wanted_Method0 = target_0.GetType().GetMethod("Hit");
					var delegate_0 = Delegate.CreateDelegate(typeof(UnityAction), target_0, Wanted_Method0) as UnityAction;
					UnityEventTools.AddPersistentListener(newItem.usage, delegate_0);
					break;
				case InventoryItem.ItemType.Shoot:
					var Wanted_Method1 = target_0.GetType().GetMethod("Shoot");
					var delegate_1 = Delegate.CreateDelegate(typeof(UnityAction), target_0, Wanted_Method1) as UnityAction;
					UnityEventTools.AddPersistentListener(newItem.usage, delegate_1);
					break;
				case InventoryItem.ItemType.Light:
					var Wanted_Method2 = target_0.GetType().GetMethod("Light");
					var delegate_2 = Delegate.CreateDelegate(typeof(UnityAction), target_0, Wanted_Method2) as UnityAction;
					UnityEventTools.AddPersistentListener(newItem.usage, delegate_2);
					break;
				case InventoryItem.ItemType.Heal:
					var Wanted_Method3 = target_0.GetType().GetMethod("Heal");
					var delegate_3 = Delegate.CreateDelegate(typeof(UnityAction), target_0, Wanted_Method3) as UnityAction;
					UnityEventTools.AddPersistentListener(newItem.usage, delegate_3);
					break;
				case InventoryItem.ItemType.Mana:
					var Wanted_Method4 = target_0.GetType().GetMethod("UseMana");
					var delegate_4 = Delegate.CreateDelegate(typeof(UnityAction), target_0, Wanted_Method4) as UnityAction;
					UnityEventTools.AddPersistentListener(newItem.usage, delegate_4);
					break;
				case InventoryItem.ItemType.Ammo:
					var Wanted_Method5 = target_0.GetType().GetMethod("UseAmmo");
					var delegate_5 = Delegate.CreateDelegate(typeof(UnityAction), target_0, Wanted_Method5) as UnityAction;
					UnityEventTools.AddPersistentListener(newItem.usage, delegate_5);
					break;
				case InventoryItem.ItemType.Other:
					Debug.Log("Please enter a valid Usage function for : " + newItem.name);
					break;

            }

			newItem.totalDurability = durability;

			prefab.GetComponent<Object>().inventoryItem = newItem;

			UnityEngine.Object popupHolder = AssetDatabase.LoadAssetAtPath($"{popUpHolderLocation}", typeof(GameObject));
			PrefabUtility.InstantiatePrefab(popupHolder, prefab.transform);
			GameObject popupHolderOBJ = (GameObject)popupHolder;

			PopUp target = prefab.transform.GetChild(0).gameObject.GetComponent<PopUp>();

			var WantedMethod1 = target.GetType().GetMethod("ShowPopup");
			var delegate1 = Delegate.CreateDelegate(typeof(UnityAction), target, WantedMethod1) as UnityAction;
			UnityEventTools.AddPersistentListener(prefab.GetComponent<Object>().showPopup, delegate1);

			var WantedMethod2 = target.GetType().GetMethod("HidePopup");
			var delegate2 = Delegate.CreateDelegate(typeof(UnityAction), target, WantedMethod2) as UnityAction;
			UnityEventTools.AddPersistentListener(prefab.GetComponent<Object>().hidePopup, delegate2);

			GameObject obj = PrefabUtility.SaveAsPrefabAssetAndConnect(prefab, $"Assets/InventoryDragAndDrop/Prefabs/ItemPrefabs/{itemName}.prefab", InteractionMode.UserAction);
			//SerializedObject serializedObject = new UnityEditor.SerializedObject(newItem);
			newItem.itemPrefab = AssetDatabase.LoadAssetAtPath<GameObject>($"Assets/InventoryDragAndDrop/Prefabs/ItemPrefabs/{itemName}.prefab");
			//newItem.itemPrefab = obj;
		}


		GUILayout.Label(" \n \n Required fields to find an object created", bigBold);

		GUILayout.Label("Will open chosen Inventory Item in the inspector, for easy editing");

		selectedItem = (InventoryItem)EditorGUILayout.ObjectField("Inventory Item", selectedItem, typeof(InventoryItem), true);

		if (GUILayout.Button("Open Inventory Item Data") == true)
        {
			AssetDatabase.OpenAsset(selectedItem);
        }


			GUILayout.EndVertical();
	}
}
