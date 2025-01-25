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
    }

    public bool FindWord(string word)
    {
        return mWordList.Contains(word.ToLower());
    }

    // Update is called once per frame
    void Update()
    {
        string word = mWordToFind.text.Trim();
        bool WordFound = FindWord(word);
        if (WordFound)
        {
            OnValidWord?.Invoke(word);
        }
        Debug.Log($"Word '{mWordToFind}' found: {WordFound}");
    }
}
