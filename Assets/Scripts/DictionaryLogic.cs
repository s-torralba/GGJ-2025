using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System;

public class WordDictionary : MonoBehaviour
{
    public TextAsset mDictionaryFile;
    private HashSet<string> mWordList; // Use HashSet for faster lookups

    public TMP_InputField mWordToFind;

    public event Action<string> OnValidWord;
    public event Action OnWrongWord;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the HashSet and load words
        mWordList = new HashSet<string>();

        if (mDictionaryFile != null)
        {
            string[] words = mDictionaryFile.text.Split('\n');
            foreach (string word in words)
            {
                mWordList.Add(word.Trim().ToLower());
            }
        }
        else
        {
            Debug.LogError("DictionaryFile is not assigned!");
        }

        List<string> wordsChosen = ChooseWords(3);
        int a = 3;

    }

    public bool FindWord(string word)
    {
        return mWordList.Contains(word.ToLower());
    }

    public List<string> ChooseWords(int numberOfWords)
    {
        // Convert HashSet to a List for indexed access
        List<string> wordList = new List<string>(mWordList);

        // Handle cases where the requested number of words exceeds the available words
        if (numberOfWords > wordList.Count)
        {
            Debug.LogWarning("Requested more words than available. Returning all words.");
            return wordList;
        }

        // Create a list to store the chosen words
        List<string> chosenWords = new List<string>();
        System.Random random = new System.Random();

        // Randomly pick words
        while (chosenWords.Count < numberOfWords)
        {
            // Get a random word from the list
            string randomWord = wordList[random.Next(0, wordList.Count)];

            // Avoid duplicates in the chosen words list
            if (!chosenWords.Contains(randomWord))
            {
                chosenWords.Add(randomWord);
            }
        }

        return chosenWords;
    }

    // Update is called once per frame
    void ProcessWord(string word)
    {
        bool WordFound = FindWord(word);
        if (WordFound)
        {
            OnValidWord?.Invoke(word);
            Debug.Log($"Word '{mWordToFind}' found: {WordFound}");
        }
        else
        {
            OnWrongWord?.Invoke();
        }
    }
    public void SubscribeToOnWord(LetterAccumulator accumulator)
    {
        accumulator.OnWord += ProcessWord;
    }
}
