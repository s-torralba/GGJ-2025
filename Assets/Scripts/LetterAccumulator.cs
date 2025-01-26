using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.EventSystems;

public class LetterAccumulator : MonoBehaviour, IDropHandler
{
    [SerializeField] private GameObject letterSlotPrefab;

    public event Action<string> OnWord;


    private int lettersCount = 0;

    private void Update()
    {
        foreach (Transform child in transform)
        {
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

            IDropHandler dropHandler = newLetterSlot.GetComponent<IDropHandler>();

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
