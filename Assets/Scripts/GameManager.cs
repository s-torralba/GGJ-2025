using UnityEngine;
using TMPro; // Required for TextMeshPro

public class GameManager : MonoBehaviour
{
    [Header("Game Components")]
    public WordDictionary dictionary;      // Reference to the Dictionary script
    public WordScorer puntuator;        // Reference to the Puntuator script

    //[Header("UI Components")]
    //public TMP_InputField wordInputField; // InputField for player to type a word
    //public TextMeshProUGUI feedbackText;  // Text to display feedback (e.g., valid/invalid, score)

    // Start is called before the first frame update
    void Start()
    {
        // Ensure all components are assigned
        if (dictionary == null || puntuator == null /*|| wordInputField == null || feedbackText == null*/)
        {
            Debug.LogError("GameManager is missing required components!");
            return;
        }

        // Subscribe Puntuator to Dictionary's word validation event
        puntuator.SubscribeToDictionary(dictionary);

        // Clear feedback text on start
        //feedbackText.text = "Type a word and press Enter to validate!";
    }

    // Called when the player presses Enter or a button to validate a word
    /*
    public void ValidateInputWord()
    {
        // Get the word from the InputField
        string word = wordInputField.text.Trim();

        // Check if the InputField is empty
        if (string.IsNullOrEmpty(word))
        {
            feedbackText.text = "Please enter a word.";
            return;
        }

        // Ask the Dictionary to validate the word
        dictionary.ValidateWord(word);

        // Clear the InputField after validation
        wordInputField.text = "";
    }*/
}
