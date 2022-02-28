
///Permission to distribute belongs to Jess_StPierre on the Unity Asset Store. If you bought this asset, you have permission to use it in your project.

using UnityEngine;

/// <summary>
/// "Press E to interact" popup functionality
/// </summary>
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
			this.transform.LookAt(mainCamera.transform.position); //want this popup to always look at the camera, so we dont get anything weird
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
