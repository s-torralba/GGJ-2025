using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    AudioSource audioSource;

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
                    PlayAudio();
                    draggableItem.parentAfterDrag = transform;
                    draggableItem.UpdateParent();
                }
            }

            if (eventData.pointerDrag != null)
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            }
        }
    }

    private void PlayAudio()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            AudioClip clip = Resources.Load<AudioClip>("LetterPlaced");
            audioSource.clip = clip;
        }
        audioSource.volume = 0.8f;
        audioSource.Play();
    }
    
    public void UpdateParent (Transform parentTransform)
    {
        foreach (Transform gridItem in transform)
        {
            DraggableItem draggableItem = gridItem.GetComponent<DraggableItem>();
            if (draggableItem != null)
            {
                draggableItem.parentAfterDrag = transform;
            }
        }
    }
}


