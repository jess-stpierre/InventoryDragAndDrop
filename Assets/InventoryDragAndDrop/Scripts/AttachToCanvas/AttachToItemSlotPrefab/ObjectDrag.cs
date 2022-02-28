///Permission to distribute belongs to Jess_StPierre on the Unity Asset Store. If you bought this asset, you have permission to use it in your project.

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

/// <summary>
/// Gives the ability for a inventory UI item to be drag-able throughout the inventory
/// </summary>
public class ObjectDrag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler , IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas actualCanvas;
    [Header("Default is left-control, changeable below")]
    [SerializeField] private KeyCode control = KeyCode.LeftControl;
    [SerializeField] private GameObject descriptionBox;
    [SerializeField] private Text textBox;

    private GameObject[] hotbar;

    private string text;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        actualCanvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();

        hotbar = GameObject.FindGameObjectsWithTag("Equipped");
        hotbar.Reverse();
    }

    /// <summary>
    /// Called when you initially start the dragging process
    /// </summary>
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f; // can increase this value if you want the item to be more visible when dragging

        canvasGroup.blocksRaycasts = false;
    }

    /// <summary>
    /// Called every frame during the drag
    /// </summary>
    public void OnDrag(PointerEventData eventData)
    {
        //we want the rect Transform of this inventory object to follow the transform of the pointer mouse...
       rectTransform.anchoredPosition = rectTransform.anchoredPosition + (eventData.delta / actualCanvas.scaleFactor);

        // When dragging and hit left control key (or which ever input you want... the inventory item will try to automically move to the hotbar quickslots
        if (Input.GetKey(control) == true)
        {
            for (int i = hotbar.Length-1; i > -1; i--)
            {
                if (hotbar[i].transform.childCount == 0) hotbar[i].GetComponent<ObjectDrop>().AttemptHotbarDrop(this.gameObject);
            }
        }
    }


    /// <summary>
    /// Called when you stop dragging the item
    /// </summary>
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f; //bring back the full alpha value when we stop dragging the item
        canvasGroup.blocksRaycasts = true;
    }

    /// <summary>
    /// Called when you hover over the item
    /// </summary>
	public void OnPointerEnter(PointerEventData eventData)
	{
        descriptionBox.SetActive(true);

        //want to setup the description box info... (can adjust the info as you like!)
        if(GetComponent<Object>().inventoryItem != null)
		{
            text ="Description: " + GetComponent<Object>().inventoryItem.itemDescription + " \n";
            text += "Durability left: " + GetComponent<Object>().inventoryItem.totalDurability.ToString() + " \n";
            text += "Type: " + GetComponent<Object>().inventoryItem.currentItemType.ToString() + " \n";

            textBox.text = text;
        }
    }

    /// <summary>
    /// Called when we stop hovering over the item
    /// </summary>
	public void OnPointerExit(PointerEventData eventData)
	{
        descriptionBox.SetActive(false);
    }
}
