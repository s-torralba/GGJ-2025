using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterImageSwitcher : MonoBehaviour
{
    [SerializeField] private Image letterImage; // Assign the Image component in the Inspector
    private string letterName;

    private static string spritePath = "kenney_letter-tiles (1)/PNG/Wood/letter_";

    public void ChangeLetter(string uppercaseLetter)
    {
        if (letterImage != null)
        {
            // Load the new sprite from the Resources folder
            letterName = uppercaseLetter;
            string computedPath = spritePath + uppercaseLetter;
            Sprite newSprite = Resources.Load<Sprite>(computedPath);

            if (newSprite != null)
            {
                // Assign the new sprite to the Image
                letterImage.sprite = newSprite;

                // Adjust the Image's RectTransform to fit its container
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
        // Get the RectTransform of the Image's parent container
        RectTransform containerRect = letterImage.rectTransform.parent.GetComponent<RectTransform>();
        RectTransform imageRect = letterImage.rectTransform;

        if (containerRect != null)
        {
            // Match the Image's size to the container's size
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
