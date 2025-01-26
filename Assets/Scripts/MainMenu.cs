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
    [SerializeField] GameObject HUD;

    private void OnEnable()
    {
        scoreRecord.text = "Score Record: " + gameManager.maxScore.ToString();
        roundRecord.text = "Round Record: " + gameManager.maxRound.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStartPressed()
    {
        this.gameObject.SetActive(false);
        HUD.SetActive(true);
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
