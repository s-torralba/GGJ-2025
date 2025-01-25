using UnityEngine;
using System.Collections.Generic;

public class WordDictionary : MonoBehaviour
{
    public TextAsset DictionaryFile;

    public string WordToFind;

    private HashSet<string> dictionaryWords; // Use HashSet for faster lookups

    public bool FindEnable = false;
    public bool WordFound = false; // Renamed for better clarity

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the HashSet and load words
        dictionaryWords = new HashSet<string>();

        if (DictionaryFile != null)
        {
            string[] words = DictionaryFile.text.Split('\n');
            foreach (string word in words)
            {
                dictionaryWords.Add(word.Trim().ToLower());
            }
        }
        else
        {
            Debug.LogError("DictionaryFile is not assigned!");
        }
    }

    public bool FindWord(string word)
    {
        return dictionaryWords.Contains(word.ToLower());
    }

    // Update is called once per frame
    void Update()
    {
        if (FindEnable)
        {
            FindEnable = false;
            WordFound = FindWord(WordToFind);
            Debug.Log($"Word '{WordToFind}' found: {WordFound}");
        }
    }
}
