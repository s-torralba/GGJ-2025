using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Perform your action here
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
