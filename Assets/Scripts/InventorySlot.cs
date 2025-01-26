using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            if (dropped != null)
            {
                DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
                if (draggableItem != null)
                {
                    draggableItem.parentAfterDrag = transform;
                }
            }

            if (eventData.pointerDrag != null)
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            }
        }
    }
}
