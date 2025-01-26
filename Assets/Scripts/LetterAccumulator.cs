using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.EventSystems;

public class LetterAccumulator : MonoBehaviour, IDropHandler
{
    [SerializeField] private GameObject letterSlotPrefab; // Prefab for the draggable letter object

    public event Action<string> OnWord;


    private int lettersCount = 0;

    private void Update()
    {
        // Loop through all children of this object
        foreach (Transform child in transform)
        {
            // Check if the child has a LetterData component
            LetterData letterData = child.GetComponentInChildren<LetterData>();
            if (letterData == null)
            {
                if (DragManager.Instance != null)
                {
                    if (DragManager.Instance.IsAnythingBeingDragged)
                    {
                        break;
                    }
                }
                        // If no LetterData component is found, destroy the child
                Debug.LogWarning($"Child {child.name} does not have a LetterData component. Deleting it.");
                Destroy(child.gameObject);
                ProcessWord();
            }
        }
    }
    public void ResetChildren()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        lettersCount++;
        if (transform.childCount < lettersCount)
        {
            GameObject newLetterSlot = Instantiate(letterSlotPrefab);
            newLetterSlot.transform.SetParent(transform, false);

            // Get the IDropHandler component from the newLetterSlot
            IDropHandler dropHandler = newLetterSlot.GetComponent<IDropHandler>();

            // Call OnDrop on the newLetterSlot if it has a valid IDropHandler
            if (dropHandler != null)
            {
                dropHandler.OnDrop(eventData);
                ProcessWord();
            }
            else
            {
                Debug.LogWarning("The instantiated letterSlotPrefab does not have a component implementing IDropHandler.");
            }

        }
    }

    private void ProcessWord()
    {
        string word = "";
        foreach (Transform child in transform)
        {
            LetterData letterData = child.GetComponentInChildren<LetterData>();
            if (letterData)
            {
                word += letterData.letter;
            }

        }
        OnWord?.Invoke(word);
    }
}
