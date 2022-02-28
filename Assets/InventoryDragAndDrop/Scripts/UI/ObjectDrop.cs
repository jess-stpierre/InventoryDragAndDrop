
///Permission to distribute belongs to Jess_StPierre on the Unity Asset Store. If you bought this asset, you have permission to use it in your project.

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Attached to every inventory slot to allow items to be dropped on them
/// </summary>
public class ObjectDrop : MonoBehaviour, IDropHandler
{ 
    [SerializeField] private List<GameObject> slots = new List<GameObject>(); //reference to all inventory slots

    /// <summary>
    /// When mouse pointer lets go of an item on a slot stuff inside this function happens...
    /// </summary>
    public void OnDrop(PointerEventData eventData)
    {
        // as long as pointer is holding a gameObject and this drop slot is available for an item
        if(eventData.pointerDrag != null && this.gameObject.transform.childCount == 0 && (this.gameObject.CompareTag("Slot") || this.gameObject.CompareTag("Equipped")))
        {
            SetParentAndPosition(eventData);
        }
        // if pointer is holding object and this drop slot already has an item in it...
        else if(eventData.pointerDrag != null && this.gameObject.transform.childCount != 0 && (this.gameObject.CompareTag("Slot") || this.gameObject.CompareTag("Equipped")))
        {
            if(slots.Count > 0)
            {
                int counter = 0;

                for (int i = 0; i < slots.Count; i++)
                {
                    if(slots[i].transform.childCount == 0 && counter == 0)
                    {
                        if(this.gameObject.transform.childCount > 0) //we will drop the item on the next available slot...
                        {
                            Transform childZero = this.gameObject.transform.GetChild(0);

                            childZero.gameObject.GetComponent<RectTransform>().anchoredPosition = slots[i].GetComponent<RectTransform>().anchoredPosition;
                            childZero.transform.position = slots[i].transform.position;
                            childZero.transform.SetParent(slots[i].transform, true);
                        }

                        SetParentAndPosition(eventData);
                        counter++;
                    }
                }
            }
        }
        else //if we let go the item NOT on top of another slot, the item will automatically be brought back to its original slot
        {
            if(slots.Count > 0)
            {
                foreach (GameObject slot in slots)
                {
                    if (eventData.pointerDrag.transform.parent.gameObject == slot)
					{
                        GameObject obj = eventData.pointerDrag;

                        obj.transform.SetParent(slot.transform, false);
                        obj.GetComponent<RectTransform>().anchoredPosition = slot.GetComponent<RectTransform>().anchoredPosition;
                        obj.transform.position = slot.transform.position;

                        EquippedSlot();
                    }
                }
            }
        } 
    }

    /// <summary>
    /// Helper function for usage above
    /// </summary>
    private void SetParentAndPosition(PointerEventData eventData)
    {
        GameObject obj = eventData.pointerDrag;

        obj.transform.SetParent(this.transform, false);
        obj.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        obj.transform.position = this.transform.position;

        EquippedSlot();
    }

    /// <summary>
    /// Check if this is a hotbar equipment slot
    /// </summary>
    private void EquippedSlot()
	{
		if (this.gameObject.CompareTag("Equipped"))
		{
            UIEventBroker.TriggerOnDraggedItemToSelectedSlot();
        }
	}

    /// <summary>
    /// Only used for the quick transfering to the hotbar - using Control + Left Mouse Drag
    /// </summary>
    public void AttemptHotbarDrop(GameObject obj)
	{
        obj.transform.SetParent(this.transform, false);
        obj.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        obj.transform.position = this.transform.position;

        EquippedSlot();
    }
}
