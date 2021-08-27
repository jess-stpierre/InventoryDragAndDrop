using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
	[SerializeField] private GameObject popup;
	[SerializeField] private GameObject mainCamera;

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
