using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterImageSwitcher : MonoBehaviour
{
    [SerializeField] private Image letterImage;
    private string letterName;

    private static string spritePath = "kenney_letter-tiles (1)/PNG/Wood/letter_";

    public void ChangeLetter(string uppercaseLetter)
    {
        if (letterImage != null)
        {
            letterName = uppercaseLetter;
            string computedPath = spritePath + uppercaseLetter;
            Sprite newSprite = Resources.Load<Sprite>(computedPath);

            if (newSprite != null)
            {
                letterImage.sprite = newSprite;

                ResizeImageToFitContainer();

                Debug.Log($"Sprite successfully changed to: {computedPath}");
            }
            else
            {
                Debug.LogError($"Sprite not found at path: {computedPath}. Make sure the sprite is in a 'Resources' folder.");
            }
        }
        else
        {
            Debug.LogError("Letter Image component is not assigned.");
        }
    }

    private void ResizeImageToFitContainer()
    {
        RectTransform containerRect = letterImage.rectTransform.parent.GetComponent<RectTransform>();
        RectTransform imageRect = letterImage.rectTransform;

        if (containerRect != null)
        {
            imageRect.anchorMin = Vector2.zero;
            imageRect.anchorMax = Vector2.one;
            imageRect.offsetMin = Vector2.zero;
            imageRect.offsetMax = Vector2.zero;
        }
        else
        {
            Debug.LogError("No RectTransform found for the parent container.");
        }
    }
}
