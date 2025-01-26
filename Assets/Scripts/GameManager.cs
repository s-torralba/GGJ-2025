using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SocialPlatforms.Impl; 
using UnityEngine.UI;
using Unity.VisualScripting; 

public class GameManager : MonoBehaviour
{
    [Header("Game Components")]
    public WordDictionary dictionary;     
    public WordScorer puntuator;        
    public BubbleManager bubbleManager;

    public int numberOfWordsPerLevel = 4;
    public int randomLettersToAddPerLevel = 7;

    public int totalScore = 0;
    public int totalRound = 0;
    public int maxScore = 0;
    public int maxRound = 0;

    public LetterAccumulator letterAccumulator;        

    public LetterCollector letterCollector; 
    public GridLayoutGroup letterBoardGrid; 

    [SerializeField] private GameObject letterSlotPrefab; 
    [SerializeField] private GameObject letterPrefab; 

    [SerializeField] GameObject HUDUp;
    public TextMeshProUGUI targetOutputText;          
    public TextMeshProUGUI totalScoreOutputText;         
    public TextMeshProUGUI roundOutputText;         

    [SerializeField] GameObject HUDDown;
    [SerializeField] GameObject LoseScreen;

    private bool hasStarted = false;
    private int collectedCount = 0;

    private int scoreTarget = 3;
    
    void Start()
    {
        if (dictionary == null || puntuator == null)
        {
            Debug.LogError("GameManager is missing required components!");
            return;
        }

        dictionary.SubscribeToOnWord(letterAccumulator);
        puntuator.SubscribeToDictionary(dictionary);
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
            targetOutputText.text = " ";
            puntuator.UpdateTarget(scoreTarget);

        }
    }

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

        if (HUDDown.GetComponentInChildren<LetterAccumulator>())
        {
            HUDDown.GetComponentInChildren<LetterAccumulator>().ResetChildren();
        }
        puntuator.Reset();
        HUDDown.SetActive(false);

        letterCollector.collectedLetters = new List<string>();
        collectedCount = 0;

        targetOutputText.text = "TARGET : " + scoreTarget;
        totalScoreOutputText.text = "TOTAL SCORE : " + totalScore;
        roundOutputText.text = "ROUND: " + (totalRound+1);

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
