using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterCollector : MonoBehaviour
{
    public List<string> collectedLetters = new List<string>();

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void CollectLetter(string letter)
    {
        collectedLetters.Add(letter);
        Debug.Log($"Collected letter: {letter}");
    }
}
