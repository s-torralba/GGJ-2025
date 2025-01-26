using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SocialPlatforms.Impl; // Required for TextMeshPro
using UnityEngine.UI;
using Unity.VisualScripting; // Required for TextMeshPro

public class GameManager : MonoBehaviour
{
    [Header("Game Components")]
    public WordDictionary dictionary;      // Reference to the Dictionary script
    public WordScorer puntuator;        // Reference to the Puntuator script
    public BubbleManager bubbleManager;

    public int numberOfWordsPerLevel = 4;
    public int randomLettersToAddPerLevel = 7;

    private int totalScore = 0;
    private int totalRound = 0;
    public int maxScore = 0;
    public int maxRound = 0;

    public LetterAccumulator letterAccumulator;        // Reference to the Puntuator script

    public LetterCollector letterCollector; // Reference to the Letter Collector script
    public GridLayoutGroup letterBoardGrid; // Reference to the letter board grid layout group

    [SerializeField] private GameObject letterSlotPrefab; // Prefab for the draggable letter object
    [SerializeField] private GameObject letterPrefab; // Prefab for the draggable letter object

    [SerializeField] GameObject HUDUp;
    [SerializeField] GameObject HUDDown;
    [SerializeField] GameObject LoseScreen;

    private bool hasStarted = false;
    private int collectedCount = 0;

    private int scoreTarget = 3;
    
    //[Header("UI Components")]
    //public TMP_InputField wordInputField; // InputField for player to type a word
    //public TextMeshProUGUI feedbackText;  // Text to display feedback (e.g., valid/invalid, score)

    // Start is called before the first frame update
    void Start()
    {
        // Ensure all components are assigned
        if (dictionary == null || puntuator == null /*|| wordInputField == null || feedbackText == null*/)
        {
            Debug.LogError("GameManager is missing required components!");
            return;
        }

        // Subscribe Puntuator to Dictionary's word validation event
        dictionary.SubscribeToOnWord(letterAccumulator);
        puntuator.SubscribeToDictionary(dictionary);

        // Clear feedback text on start
        //feedbackText.text = "Type a word and press Enter to validate!";

    }

    private void Update()
    {
        int currentlyCollected = letterCollector.collectedLetters.Count;
        if (currentlyCollected > 0 && currentlyCollected != collectedCount)
        {
            for (int i = collectedCount; i < letterCollector.collectedLetters.Count; i++)
            {
                GameObject newLetterSlot = Instantiate(letterSlotPrefab);
                newLetterSlot.transform.SetParent(letterBoardGrid.transform, false);

                GameObject newLetter = Instantiate(letterPrefab);
                newLetter.transform.SetParent(newLetterSlot.transform, false);
                
                newLetter.GetComponent<LetterImageSwitcher>().ChangeLetter(letterCollector.collectedLetters[i].ToString());
                newLetter.GetComponent<LetterData>().letter = letterCollector.collectedLetters[i];
            }
            collectedCount = currentlyCollected;
        }

        if (!HUDDown.activeSelf && bubbleManager.hasFinished && hasStarted)
        {
            HUDDown.SetActive(true);
            puntuator.UpdateTarget(scoreTarget);

        }
    }

    // Called when the player presses Enter or a button to validate a word
    /*
    public void ValidateInputWord()
    {
        // Get the word from the InputField
        string word = wordInputField.text.Trim();

        // Check if the InputField is empty
        if (string.IsNullOrEmpty(word))
        {
            feedbackText.text = "Please enter a word.";
            return;
        }

        // Ask the Dictionary to validate the word
        dictionary.ValidateWord(word);

        // Clear the InputField after validation
        wordInputField.text = "";
    }*/

    public void StartGame()
    {
        totalScore = 0;
        totalRound = 0;
        hasStarted = true;
        StartRound();
    }

    public void StartRound()
    {
        List<string> wordsChosen = dictionary.ChooseWords(numberOfWordsPerLevel);
        bubbleManager.ChooseLetters(wordsChosen, randomLettersToAddPerLevel);

        if (HUDUp.GetComponentInChildren<DropCollector>())
        {
            HUDUp.GetComponentInChildren<DropCollector>().ResetChildren();
        }
        //HUDUp.SetActive(false);

        if (HUDDown.GetComponentInChildren<LetterAccumulator>())
        {
            HUDDown.GetComponentInChildren<LetterAccumulator>().ResetChildren();
        }
        puntuator.Reset();
        HUDDown.SetActive(false);

        letterCollector.collectedLetters = new List<string>();
        collectedCount = 0;

    }

    public void EndRound(int scoreGained, bool hasLost)
    {
        totalScore += scoreGained;
        totalRound++;
        scoreTarget++;
        
        if (hasLost)
        {
            EndGame();
        }
        else 
        {
            StartRound();
        }
    }

    private void EndGame()
    {
        maxScore = Mathf.Max(totalScore, maxScore);
        maxRound = Mathf.Max(totalRound, maxRound);

        LoseScreen.SetActive(true);
    }

    public void OnSendPressed()
    {
        bool hasLost = scoreTarget > puntuator.lastScore;
        EndRound(puntuator.lastScore, hasLost);
    }
}
