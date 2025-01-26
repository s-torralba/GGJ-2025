using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropCollector : MonoBehaviour, IDropHandler
{
    [SerializeField] private GameObject gridObject;

    public void ResetChildren()
    {
        foreach (Transform child in gridObject.transform)
        {
            Destroy(child.gameObject);
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (dropped != null)
        {
            if (gridObject.transform.childCount != 0)
            {
                foreach (Transform gridItem in gridObject.transform)
                {
                    LetterData droppedLetterData = dropped.GetComponent<LetterData>();
                    LetterData letterData = gridItem.GetComponentInChildren<LetterData>();
                    if (droppedLetterData != null && letterData == null)
                    {
                        dropped.transform.SetParent(gridItem.transform);
                        gridItem.GetComponent<InventorySlot>().UpdateParent(gridItem.transform);
                        break;
                        /*letterData.letter = droppedLetterData.letter;
                        LetterImageSwitcher letterImage = dropped.GetComponent<LetterImageSwitcher>();
                        if (letterImage != null)
                        {
                            letterImage.ChangeLetter(letterData.letter);
                        }*/
                    }
                }
            }
        }
    }
}
