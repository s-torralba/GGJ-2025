using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameManager gameManager;

    [SerializeField] TextMeshProUGUI scoreRecord;
    [SerializeField] TextMeshProUGUI roundRecord;

    private void OnEnable()
    {
        scoreRecord.text = "Score: " + gameManager.maxScore.ToString();
        roundRecord.text = "Round: " + gameManager.maxRound.ToString();
    }

    public void OnRestartPressed()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void OnBackPressed()
    {
        this.gameObject.SetActive(false);
        mainMenu.SetActive(true);
    }
}
