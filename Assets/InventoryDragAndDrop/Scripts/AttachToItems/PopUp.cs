
///Permission to distribute belongs to Jess_StPierre on the Unity Asset Store. If you bought this asset, you have permission to use it in your project.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
	[SerializeField] private GameObject popup;
	[SerializeField] private string cameraTag = "Camera";

	private GameObject mainCamera;

	private void Start()
	{
		mainCamera = GameObject.FindGameObjectWithTag(cameraTag);
	}

	private void Update()
	{
		if(popup.activeInHierarchy == true)
		{
			this.transform.LookAt(mainCamera.transform.position);
		}
	}

	public void ShowPopup()
	{
		popup.SetActive(true);
	}

	public void HidePopup()
	{
		popup.SetActive(false);
	}
}
