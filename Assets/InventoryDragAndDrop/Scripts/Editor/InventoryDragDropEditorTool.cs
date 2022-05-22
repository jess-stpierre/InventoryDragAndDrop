
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
	Color headerColor = new Color(255f / 255f, 101f / 255f, 0 / 255f, 1f);
	Texture2D headerTexture;
	Rect headerSize;

	Color sectionUnoColor = new Color(0f / 255f, 50f / 255f, 126 / 255f, 1f);
	Texture2D sectionUnoTexture;
	Rect sectionUnoSize;

	Color sectionDosColor = new Color(53f / 255f, 0f / 255f, 126 / 255f, 1f);
	Texture2D sectionDosTexture;
	Rect sectionDosSize;

	[MenuItem("Tools/InventoryDragDropEditorTool")]
	public static void ShowWindow()
	{
		InventoryDragDropEditorTool window = GetWindow<InventoryDragDropEditorTool>();
		window.minSize = new Vector2(583f, 900f);
	}

	private GameObject prefab;
	private string itemName;
	private string description;
	private Sprite sprite;
	private int durability;
	private string prefabLocation;
	private string popUpHolderLocation;

	private InventoryItem.ItemType chosenItemType;

	private InventoryItem selectedItem;

	private GUIStyle bigBoldWhite;
	private GUIStyle bigBoldBlack;
	
	private GUIStyle smallWhite;
	private GUIStyle smallBlack;


	private Vector3 positionOfPrefab;
	private Vector3 rotationOfPrefab;

	private void OnEnable()
    {
		bigBoldWhite = new GUIStyle();
		bigBoldWhite.fontSize = 20;
		bigBoldWhite.fontStyle = FontStyle.Bold;
		bigBoldWhite.normal.textColor = Color.white;

		bigBoldBlack = new GUIStyle();
		bigBoldBlack.fontSize = 20;
		bigBoldBlack.fontStyle = FontStyle.Bold;
		bigBoldBlack.normal.textColor = Color.black;

		smallWhite = new GUIStyle();
		smallWhite.fontStyle = FontStyle.Bold;
		smallWhite.normal.textColor = Color.white;

		smallBlack = new GUIStyle();
		smallBlack.fontStyle = FontStyle.Bold;
		smallBlack.normal.textColor = Color.black;

		InitializeTextures();
	}

	private void InitializeTextures()
    {
		headerTexture = new Texture2D(1, 1);
		headerTexture.SetPixel(0, 0, headerColor);
		headerTexture.Apply();

		sectionUnoTexture = new Texture2D(1, 1);
		sectionUnoTexture.SetPixel(0, 0, sectionUnoColor);
		sectionUnoTexture.Apply();

		sectionDosTexture = new Texture2D(1, 1);
		sectionDosTexture.SetPixel(0, 0, sectionDosColor);
		sectionDosTexture.Apply();
	}

	private void DrawAllLayouts()
    {
		headerSize.x = 50;
		headerSize.y = 0;
		headerSize.width = Screen.width-100;
		headerSize.height = 50;

		sectionUnoSize.x = 0;
		sectionUnoSize.y = 50;
		sectionUnoSize.width = Screen.width;
		sectionUnoSize.height = 700;

		sectionDosSize.x = 0;
		sectionDosSize.y = 700;
		sectionDosSize.width = Screen.width;
		sectionDosSize.height = 170;

		GUI.DrawTexture(headerSize, headerTexture);
		GUI.DrawTexture(sectionUnoSize, sectionUnoTexture);
		GUI.DrawTexture(sectionDosSize, sectionDosTexture);
	}

	private void DrawHeader()
    {
		GUILayout.BeginArea(headerSize);
		GUILayout.Space(10);
		GUILayout.Label("        INVENTORY DRAG DROP EDITOR TOOL", bigBoldBlack);
		GUILayout.EndArea();
    }

	private void DrawSectionUno()
    {
		GUILayout.BeginArea(sectionUnoSize);
		GUILayout.Space(10);
		GUILayout.Label("SECTION 1: Create pickup-able object", bigBoldWhite);

		PickupAbleObjectCreation();

		GUILayout.EndArea();
	}

	private void DrawSectionDos()
	{
		GUILayout.BeginArea(sectionDosSize);
		GUILayout.Space(10);
		GUILayout.Label("SECTION 2: Find a pickup-able object", bigBoldWhite);

		PickupAbleObjectSearch();

		GUILayout.EndArea();
	}


	private void PickupAbleObjectCreation()
	{
		prefabLocation = "Assets/InventoryDragAndDrop/Scripts/ScriptableObject/InventoryObjects";

		GUILayout.Space(10);

		GUILayout.Label("Required fields to create your pickup-able object", bigBoldBlack);

		GUILayout.Space(10);

		GUILayout.Label("Chosen prefab must have Mesh Filter & Mesh Renderer", smallWhite);
		GUILayout.Space(10);
		GUILayout.Label(" Default location of prefab is: \n Assets/InventoryDragAndDrop/Prefabs/ItemPrefabs/*ItemName*.prefab", smallBlack);
		GUILayout.Space(10);
		prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", prefab, typeof(GameObject), false);

		GUILayout.Space(10);
		positionOfPrefab = EditorGUILayout.Vector3Field("Position of Prefab", positionOfPrefab);
		
		GUILayout.Space(10);
		rotationOfPrefab = EditorGUILayout.Vector3Field("Rotation of Prefab", rotationOfPrefab);

		GUILayout.Space(10);

		GUILayout.Label(" Default location of scriptable object is: \n Assets/InventoryDragAndDrop/Scripts/ScriptableObject/InventoryObjects", smallBlack);
		GUILayout.Space(10);
		prefabLocation = EditorGUILayout.TextField("Location to Store Scriptable Object", prefabLocation);

		GUILayout.Space(10);

		itemName = EditorGUILayout.TextField("Item Name", itemName);
		GUILayout.Space(10);
		description = EditorGUILayout.TextField("Item Description", description);
		GUILayout.Space(10);
		sprite = (Sprite)EditorGUILayout.ObjectField("Item Sprite", sprite, typeof(Sprite), true);
		GUILayout.Space(10);
		durability = EditorGUILayout.IntField("Durability", durability);
		GUILayout.Space(10);
		popUpHolderLocation = "Assets/InventoryDragAndDrop/Prefabs/ItemChildren/PopUpHolder.prefab";
		GUILayout.Label(" Default location of PopUpHolder is: \n Assets/InventoryDragAndDrop/Prefabs/ItemChildren/PopUpHolder.prefab", smallBlack);
		GUILayout.Space(10);
		popUpHolderLocation = EditorGUILayout.TextField("Location of PopUpHolder", popUpHolderLocation);

		GUILayout.Space(10);

		chosenItemType = (InventoryItem.ItemType)EditorGUILayout.EnumPopup("Item type:", chosenItemType);

		GUILayout.Space(10);

		if (GUILayout.Button("Create pickup-able object") == true)
		{
			if (prefab.GetComponent<SphereCollider>() == null) prefab.AddComponent<SphereCollider>();
			prefab.GetComponent<SphereCollider>().isTrigger = true;
			prefab.GetComponent<SphereCollider>().radius = 3f;

			if (prefab.GetComponent<BoxCollider>() == null) prefab.AddComponent<BoxCollider>();
			prefab.GetComponent<BoxCollider>().isTrigger = false;
			prefab.GetComponent<BoxCollider>().size = Vector3.one;

			if (prefab.GetComponent<Object>() == null) prefab.AddComponent<Object>();

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


			GameObject spawnedPrefab = (GameObject)PrefabUtility.InstantiatePrefab(prefab);

			spawnedPrefab.transform.position = positionOfPrefab;
			spawnedPrefab.transform.rotation = Quaternion.Euler(rotationOfPrefab.x, rotationOfPrefab.y, rotationOfPrefab.z);

			newItem.totalDurability = durability;

			if(spawnedPrefab.GetComponent<Object>().inventoryItem == null) spawnedPrefab.GetComponent<Object>().inventoryItem = newItem;

			UnityEngine.Object popupHolder = AssetDatabase.LoadAssetAtPath($"{popUpHolderLocation}", typeof(GameObject));

			if (spawnedPrefab.transform.childCount == 0) PrefabUtility.InstantiatePrefab(popupHolder, spawnedPrefab.transform);
			else if(spawnedPrefab.transform.GetChild(0).gameObject.GetComponent<PopUp>() == null) PrefabUtility.InstantiatePrefab(popupHolder, spawnedPrefab.transform);

			PopUp target = spawnedPrefab.transform.GetChild(0).gameObject.GetComponent<PopUp>();

			var WantedMethod1 = target.GetType().GetMethod("ShowPopup");
			var delegate1 = Delegate.CreateDelegate(typeof(UnityAction), target, WantedMethod1) as UnityAction;
			if(spawnedPrefab.GetComponent<Object>().showPopup.GetPersistentEventCount() == 0) UnityEventTools.AddPersistentListener(spawnedPrefab.GetComponent<Object>().showPopup, delegate1);

			var WantedMethod2 = target.GetType().GetMethod("HidePopup");
			var delegate2 = Delegate.CreateDelegate(typeof(UnityAction), target, WantedMethod2) as UnityAction;
			if (spawnedPrefab.GetComponent<Object>().hidePopup.GetPersistentEventCount() == 0) UnityEventTools.AddPersistentListener(spawnedPrefab.GetComponent<Object>().hidePopup, delegate2);

			PrefabUtility.SaveAsPrefabAssetAndConnect(spawnedPrefab, $"Assets/InventoryDragAndDrop/Prefabs/ItemPrefabs/{itemName}.prefab", InteractionMode.UserAction);
			newItem.itemPrefab = AssetDatabase.LoadAssetAtPath<GameObject>($"Assets/InventoryDragAndDrop/Prefabs/ItemPrefabs/{itemName}.prefab");
		}
	}

	private void PickupAbleObjectSearch()
    {
		GUILayout.Space(10);
		GUILayout.Label("Required fields to find an object created", bigBoldBlack);

		GUILayout.Space(10);

		GUILayout.Label("Will open chosen Inventory Item in the inspector, for easy editing", smallWhite);

		GUILayout.Space(10);

		selectedItem = (InventoryItem)EditorGUILayout.ObjectField("Inventory Item", selectedItem, typeof(InventoryItem), true);

		GUILayout.Space(10);

		if (GUILayout.Button("Open Inventory Item Data") == true)
		{
			AssetDatabase.OpenAsset(selectedItem);
		}
	}

	private void OnGUI()
	{
		DrawAllLayouts();
		DrawHeader();
		DrawSectionUno();
		DrawSectionDos();
	}
}
