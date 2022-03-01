
///Permission to distribute belongs to Jess_StPierre on the Unity Asset Store. If you bought this asset, you have permission to use it in your project.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Tool to help buyers use this asset 
/// </summary>
public class InventoryDragDropEditorTool : EditorWindow
{
	[MenuItem("Tools/InventoryDragDropEditorTool")]
	public static void ShowWindow()
	{
		GetWindow<InventoryDragDropEditorTool>();
	}

	private GameObject prefab;

	private void OnGUI()
	{
		GUILayout.Space(10);
		GUILayout.BeginVertical();

		GUIStyle bigBold = new GUIStyle();
		bigBold.fontSize = 16;
		bigBold.fontStyle = FontStyle.Bold;

		GUILayout.Label("Enter the required fields than press submit to create your pickup-able object", bigBold);

		prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", prefab, typeof(Object), true);

		//Add sphere collider that is a trigger to the prefab + adjust the size of the trigger collider
		//Add Object.cs script
		//Add Inventory Item to the slot in Object.prefab...we actually have to build it with all the stuffs

		//Add PopUpHolder as a child with PopupText, put those proper functions in the Object.cs slots
		//Use prefab we made for the popupHolder??!!!

		//SUBMIT!!!

		GUILayout.EndVertical();
	}
}
