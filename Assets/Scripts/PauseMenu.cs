using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameManager gameManager;

    [SerializeField] TextMeshProUGUI scoreRecord;
    [SerializeField] TextMeshProUGUI roundRecord;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key pressed!");
            if (canvas.activeSelf)
            {
                ResumeGame();
            }
            else
            {
                StopGame();
            }
        }
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
        canvas.SetActive(false);
    }

    private void StopGame()
    {
        Time.timeScale = 0f;
        canvas.SetActive(true);
        scoreRecord.text = "Score: " + gameManager.totalScore.ToString();
        roundRecord.text = "Round: " + gameManager.totalRound.ToString();

    }

    public void OnContinuePressed()
    {
        ResumeGame();
    }

    public void OnBackPressed()
    {
        Time.timeScale = 1f;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
