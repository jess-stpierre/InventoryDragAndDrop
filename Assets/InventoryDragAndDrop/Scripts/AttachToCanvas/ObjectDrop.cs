using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ObjectDrop : MonoBehaviour, IDropHandler
{ 
    [SerializeField] private List<GameObject> slots = new List<GameObject>();

    public void OnDrop(PointerEventData eventData)
    {
       // RectTransform eventRect = eventData.pointerDrag.GetComponent<RectTransform>();

        if(eventData.pointerDrag != null && this.gameObject.transform.childCount == 0 && this.gameObject.CompareTag("Slot"))
        {
            SetParentAndPosition(eventData);
        }
        else if(eventData.pointerDrag != null && this.gameObject.transform.childCount != 0 && this.gameObject.CompareTag("Slot"))
        {
            if(slots.Count > 0)
            {
                int counter = 0;

                for (int i = 0; i < slots.Count; i++)
                {
                    if(slots[i].transform.childCount == 0 && counter == 0)
                    {
                        if(this.gameObject.transform.childCount > 0) 
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
        else
        {
            if(slots.Count > 0)
            {
                int counter = 0;

                foreach (GameObject slot in slots)
                {
                    if(slot.transform.childCount == 0 && counter == 0)
                    {
                        GameObject obj = eventData.pointerDrag;

                        obj.transform.SetParent(slot.transform, false);
                        obj.GetComponent<RectTransform>().anchoredPosition = slot.GetComponent<RectTransform>().anchoredPosition;
                        obj.transform.position = slot.transform.position;
                        counter++;
                    }
                }
            }
        } 
    }

    private void SetParentAndPosition(PointerEventData eventData)
    {
        GameObject obj = eventData.pointerDrag;

        obj.transform.SetParent(this.transform, false);
        obj.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        obj.transform.position = this.transform.position;
    }
}
