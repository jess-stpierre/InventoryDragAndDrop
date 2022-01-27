///Permission to distribute belongs to Jess_StPierre on the Unity Asset Store. If you bought this asset, you have permission to use it in your project.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

public class ObjectDrag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler , IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas actualCanvas;
    [Header("Default is left-control, changeable below")]
    [SerializeField] private KeyCode control = KeyCode.LeftControl;
    [SerializeField] private GameObject descriptionBox;

    private GameObject[] hotbar;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        actualCanvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();

        hotbar = GameObject.FindGameObjectsWithTag("Equipped");
        hotbar.Reverse();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
       rectTransform.anchoredPosition = rectTransform.anchoredPosition + (eventData.delta / actualCanvas.scaleFactor);

        if (Input.GetKey(control) == true)
        {
            for (int i = hotbar.Length-1; i > -1; i--)
            {
                if (hotbar[i].transform.childCount == 0) hotbar[i].GetComponent<ObjectDrop>().AttemptHotbarDrop(this.gameObject);
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

	public void OnPointerEnter(PointerEventData eventData)
	{
        descriptionBox.SetActive(true);
    }

	public void OnPointerExit(PointerEventData eventData)
	{
        descriptionBox.SetActive(false);
    }
}
