
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
		GetWindow<InventoryDragDropEditorTool>();
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

	private void OnGUI()
	{
		GUILayout.Space(10);
		GUILayout.BeginVertical();

		GUIStyle bigBold = new GUIStyle();
		bigBold.fontSize = 16;
		bigBold.fontStyle = FontStyle.Bold;

		GUILayout.Label("Required fields to create your pickup-able object", bigBold);

		GUILayout.Label("Chosen prefab must have Mesh Filter & Mesh Renderer");
		prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", prefab, typeof(GameObject), true);

		itemName = EditorGUILayout.TextField("Item Name", itemName);
		description = EditorGUILayout.TextField("Item Description", description);
		sprite = (Sprite)EditorGUILayout.ObjectField("Item Sprite", sprite, typeof(Sprite), true);
		durability = EditorGUILayout.IntField("Durability", durability);



		// Draw the Inspector widget for this property.


		// Commit changes to the property back to the component we're editing.

		//build scriptable object at run-time

		//Add Object.cs script
		//Add Inventory Item to the slot in Object.prefab...we actually have to build it with all the stuffs

		//Add PopUpHolder as a child with PopupText, put those proper functions in the Object.cs slots
		//Use prefab we made for the popupHolder??!!!

		//SUBMIT!!!
		if (GUILayout.Button("Create pickup-able object") == true)
		{
			numberSubmittedNewOBJ++;

			prefab.AddComponent<SphereCollider>();
			prefab.GetComponent<SphereCollider>().isTrigger = true;
			prefab.GetComponent<SphereCollider>().radius = 3f;

			//MISSING SMALL NON TRIGGER COLLIDER

			prefab.AddComponent<Object>();

			InventoryItem newItem = ScriptableObject.CreateInstance<InventoryItem>();
			newItem.name = itemName;
			AssetDatabase.CreateAsset(newItem, $"Assets/InventoryDragAndDrop/Scripts/ScriptableObject/InventoryObjects/{itemName}.asset");
			newItem.itemName = itemName;
			newItem.itemDescription = description;
			newItem.itemImage = sprite;
			
			newItem.totalDurability = durability;

			prefab.GetComponent<Object>().inventoryItem = newItem;

			UnityEngine.Object popupHolder = AssetDatabase.LoadAssetAtPath("Assets/InventoryDragAndDrop/Prefabs/ItemChildren/PopUpHolder.prefab", typeof(GameObject));
			PrefabUtility.InstantiatePrefab(popupHolder, prefab.transform);
			GameObject popupHolderOBJ = (GameObject)popupHolder;

			PopUp target = prefab.transform.GetChild(0).gameObject.GetComponent<PopUp>();

			var WantedMethod1 = target.GetType().GetMethod("ShowPopup");
			var delegate1 = Delegate.CreateDelegate(typeof(UnityAction), target, WantedMethod1) as UnityAction;
			UnityEventTools.AddPersistentListener(prefab.GetComponent<Object>().showPopup, delegate1);

			var WantedMethod2 = target.GetType().GetMethod("HidePopup");
			var delegate2 = Delegate.CreateDelegate(typeof(UnityAction), target, WantedMethod2) as UnityAction;
			UnityEventTools.AddPersistentListener(prefab.GetComponent<Object>().hidePopup, delegate2);

			SerializedObject serializedObject = new UnityEditor.SerializedObject(newItem);
			newItem.itemPrefab = prefab;
			//usage = serializedObject.FindProperty("usage");
			//EditorGUILayout.PropertyField(usage, true);
		}
		

		GUILayout.EndVertical();
	}
}
