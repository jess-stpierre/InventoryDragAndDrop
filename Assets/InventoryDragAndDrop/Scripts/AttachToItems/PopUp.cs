using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
	[SerializeField] private GameObject popup;

	private void Update()
	{
		if(popup.activeInHierarchy == true)
		{

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
