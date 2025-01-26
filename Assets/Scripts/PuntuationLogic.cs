using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class WordScorer : MonoBehaviour
{
    public TextAsset letterWeightsFile;
    public TextMeshProUGUI scoreOutputText;
    public TextMeshProUGUI targetOutputText;
    public int lastScore = 0;

    private Dictionary<char, int> letterWeights;

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

    private void ParseLetterWeights(string fileContent)
    {
        string[] lines = fileContent.Split('\n');

        foreach (string line in lines)
        {
            if (!string.IsNullOrWhiteSpace(line))
            {
                string[] parts = line.Split(':');
                if (parts.Length == 2)
                {
                    char letter = parts[0].Trim()[0];
                    if (int.TryParse(parts[1].Trim(), out int weight))
                    {
                        letterWeights[letter] = weight;
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

    public int CalculateWordScore(string word)
    {
        int score = 0;

        foreach (char letter in word.ToUpper())
        {
            if (letterWeights.TryGetValue(letter, out int weight))
            {
                score += weight;
            }
            else
            {
                Debug.LogWarning($"Letter '{letter}' not found in weights dictionary. Ignoring.");
            }
        }

        return score;
    }

    public void SubscribeToDictionary(WordDictionary dictionary)
    {
        dictionary.OnValidWord += EvaluateWordScore;
        dictionary.OnWrongWord += PrintScoreError;
    }

    public void EvaluateWordScore(string inputWord)
    {
        if (inputWord != null)
        {
            int wordScore = CalculateWordScore(inputWord);

            if (scoreOutputText != null)
            {
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
