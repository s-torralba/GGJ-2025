using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject aboutMenu;
    [SerializeField] GameManager gameManager;
    [SerializeField] TextMeshProUGUI scoreRecord;
    [SerializeField] TextMeshProUGUI roundRecord;

    [SerializeField] BubbleManager bubbleManager;
    [SerializeField] GameObject HUDUp;

    public bool hasStarted = false;

    private void OnEnable()
    {
        scoreRecord.text = "Score Record: " + gameManager.maxScore.ToString();
        roundRecord.text = "Round Record: " + gameManager.maxRound.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnStartPressed()
    {
        this.gameObject.SetActive(false);
        hasStarted = true;
        HUDUp.SetActive(true);
        gameManager.StartGame();
    }

    public void OnAboutPressed()
    {
        mainMenu.SetActive(false);
        aboutMenu.SetActive(true);
    }

    public void OnExitPressed()
    {
        Application.Quit();
    }

    public void OnBackPressed()
    {
        mainMenu.SetActive(true);
        aboutMenu.SetActive(false);
    }
}
