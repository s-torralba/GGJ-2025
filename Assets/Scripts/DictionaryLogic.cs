using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System;

public class WordDictionary : MonoBehaviour
{
    public TextAsset mDictionaryFile;
    private HashSet<string> mWordList;

    public TMP_InputField mWordToFind;

    public event Action<string> OnValidWord;
    public event Action OnWrongWord;

    void Start()
    {
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
        List<string> wordList = new List<string>(mWordList);

        if (numberOfWords > wordList.Count)
        {
            Debug.LogWarning("Requested more words than available. Returning all words.");
            return wordList;
        }

        List<string> chosenWords = new List<string>();
        System.Random random = new System.Random();

        while (chosenWords.Count < numberOfWords)
        {
            string randomWord = wordList[random.Next(0, wordList.Count)];

            if (!chosenWords.Contains(randomWord))
            {
                chosenWords.Add(randomWord);
            }
        }

        return chosenWords;
    }

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
