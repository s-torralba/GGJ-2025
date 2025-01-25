using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    public GameObject bubblePrefab;

    public Vector2 spawnArea = new Vector2(8f, 5f);
    public float spawnInterval = 2f;
    public Vector2 scaleRange;

    private float _timer;
    private string letterPath;
    [SerializeField] private List<char> lettersToGenerate = new List<char>();
    public List<string> wordsToGenerate = new List<string>();
    public int randomLetter;
    [SerializeField] private LetterCollector letterCollector;

    // Start is called before the first frame update
    void Start()
    {
        List<string> list = new List<string>();
        ChooseLetters(list, randomLetter);
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= spawnInterval)
        {
            SpawnBubble();
            _timer = 0f;
        }
    }

    public void ChooseLetters(List<string> words, int randomLettersAdded)
    {
        words = wordsToGenerate;
        // Dictionary to track max occurrences of each letter across all words
        Dictionary<char, int> resultLetterMap = new Dictionary<char, int>();
        foreach (string word in words)
        {
            // Temporary dictionary for counting letters in the current word
            Dictionary<char, int> currentWordMap = new Dictionary<char, int>();
            string uppercaseWord = word.ToUpper();

            foreach (char c in uppercaseWord)
            {
                if (currentWordMap.ContainsKey(c))
                {
                    currentWordMap[c]++;
                }
                else
                {
                    currentWordMap[c] = 1;
                }
            }

            // Update the global letterCounts with the maximum occurrences from the current word
            foreach (KeyValuePair<char, int> letterOnCurrentWord in currentWordMap)
            {
                if (resultLetterMap.ContainsKey(letterOnCurrentWord.Key))
                {
                    resultLetterMap[letterOnCurrentWord.Key] = Mathf.Max(resultLetterMap[letterOnCurrentWord.Key], letterOnCurrentWord.Value);
                }
                else
                {
                    resultLetterMap[letterOnCurrentWord.Key] = letterOnCurrentWord.Value;
                }
            }
        }

        // Create the final list of letters based on max counts
        List<char> result = new List<char>();
        foreach (KeyValuePair<char, int> kvp in resultLetterMap)
        {
            for (int i = 0; i < kvp.Value; i++)
            {
                result.Add(kvp.Key);
            }
        }

        // Add random letters
        for (int i = 0; i < randomLettersAdded; i++)
        {
            char randomLetter = (char)Random.Range('A', 'Z' + 1);
            result.Add(randomLetter);
        }

        // Shuffle the result
        result = result.OrderBy(_ => Random.value).ToList();

        lettersToGenerate = result;
    }

    void SpawnBubble()
    {
        // Spawn at random position within the area
        float x = Random.Range(-spawnArea.x / 2, spawnArea.x / 2);
        float y = Random.Range(-spawnArea.y / 2, spawnArea.y / 2);
        Vector3 spawnPosition = new Vector3(x + transform.position.x, y + transform.position.y, 0f);

        char letter = lettersToGenerate.Last();
        lettersToGenerate.Remove(letter);

        // Instantiate the bubble prefab
        GameObject bubble = Instantiate(bubblePrefab, spawnPosition, Quaternion.identity, transform);

        Debug.Log("Bubble spawned");
        
        // Scale it
        float randomScale = Random.Range(scaleRange.x, scaleRange.y);
        Vector3 randomScaleVector = new Vector3(randomScale, randomScale, randomScale);
        bubble.transform.localScale = randomScaleVector;

        bubble.GetComponent<Bubble>().ChangeLetter(letter.ToString());
        bubble.GetComponent<Bubble>().AssignCollector(letterCollector);
    }


}
