using UnityEngine;
using System.Collections.Generic;

public class WordDictionary : MonoBehaviour
{
    public TextAsset mDictionaryFile;
    private HashSet<string> mWordList; // Use HashSet for faster lookups

    public string mWordToFind;

    public bool FindEnable = false;
    public bool WordFound = false; // Renamed for better clarity

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
        if (FindEnable)
        {
            FindEnable = false;
            WordFound = FindWord(mWordToFind);
            Debug.Log($"Word '{mWordToFind}' found: {WordFound}");
        }
    }
}
