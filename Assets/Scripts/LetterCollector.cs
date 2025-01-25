using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterCollector : MonoBehaviour
{
    public List<string> collectedLetters = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CollectLetter(string letter)
    {
        collectedLetters.Add(letter);
        Debug.Log($"Collected letter: {letter}");
    }
}
