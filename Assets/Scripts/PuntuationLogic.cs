using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI; // Required for UI components
using TMPro;
using System.Collections;

public class WordScorer : MonoBehaviour
{
    public TextAsset letterWeightsFile;    // Assign your .txt file here in the Inspector
    public TextMeshProUGUI scoreOutputText;          // Text element to display the score (optional)
    public TextMeshProUGUI targetOutputText;          // Text element to display the score (optional)
    public int lastScore = 0;

    private Dictionary<char, int> letterWeights; // To store letter and weight pairs

    // Start is called before the first frame update
    void Start()
    {
        letterWeights = new Dictionary<char, int>();

        if (letterWeightsFile != null)
        {
            ParseLetterWeights(letterWeightsFile.text);
        }
        else
        {
            Debug.LogError("LetterWeightsFile is not assigned!");
        }
    }

    // Method to parse the letter weights file
    private void ParseLetterWeights(string fileContent)
    {
        string[] lines = fileContent.Split('\n'); // Split by lines

        foreach (string line in lines)
        {
            if (!string.IsNullOrWhiteSpace(line)) // Ignore empty lines
            {
                string[] parts = line.Split(':'); // Split each line into letter and weight
                if (parts.Length == 2)
                {
                    char letter = parts[0].Trim()[0]; // Get the letter
                    if (int.TryParse(parts[1].Trim(), out int weight))
                    {
                        letterWeights[letter] = weight; // Add to dictionary
                    }
                    else
                    {
                        Debug.LogError($"Invalid weight value on line: {line}");
                    }
                }
                else
                {
                    Debug.LogError($"Invalid line format: {line}");
                }
            }
        }

        Debug.Log("Letter weights successfully parsed.");
    }

    // Method to calculate the score of a word
    public int CalculateWordScore(string word)
    {
        int score = 0;

        foreach (char letter in word.ToUpper()) // Convert to uppercase for case-insensitivity
        {
            if (letterWeights.TryGetValue(letter, out int weight))
            {
                score += weight; // Add the letter's weight to the score
            }
            else
            {
                Debug.LogWarning($"Letter '{letter}' not found in weights dictionary. Ignoring.");
            }
        }

        return score;
    }

    // Subscribes to the Dictionary's OnValidWord event
    public void SubscribeToDictionary(WordDictionary dictionary)
    {
        dictionary.OnValidWord += EvaluateWordScore;
        dictionary.OnWrongWord += PrintScoreError;
    }

    // Method called by the UI to calculate and display the score of the entered word
    public void EvaluateWordScore(string inputWord)
    {
        if (inputWord != null)
        {
            int wordScore = CalculateWordScore(inputWord);

            // Display the score in the UI
            if (scoreOutputText != null)
            {
                //ScoreOutputText.text = $"The score for '{inputWord}' is: {wordScore}";
                scoreOutputText.text = $"SCORE: {wordScore}";
                lastScore = wordScore;
            }

            Debug.Log($"The score for '{inputWord}' is: {wordScore}");
        }
        else
        {
            Debug.LogError("WordInputField is not assigned!");
        }
    }

    public void UpdateTarget(int targetScore)
    {
        // Display the score in the UI
        if (targetOutputText != null)
        {
            targetOutputText.text = $"TARGET: {targetScore}";
        }
    }

    public void PrintScoreError()
    {
        scoreOutputText.text = $"SCORE: - ";
        lastScore = 0;

    }

    public void Reset()
    {
        scoreOutputText.text = $"SCORE: - ";
        lastScore = 0;
    }
}
