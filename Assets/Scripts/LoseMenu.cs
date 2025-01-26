using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoseMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameManager gameManager;

    [SerializeField] TextMeshProUGUI scoreRecord;
    [SerializeField] TextMeshProUGUI roundRecord;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        scoreRecord.text = "Score Record: " + gameManager.maxScore.ToString();
        roundRecord.text = "Round Record: " + gameManager.maxRound.ToString();
    }

    public void OnRestartPressed()
    {
        this.gameObject.SetActive(false);
        gameManager.StartGame();
    }

    public void OnBackPressed()
    {
        this.gameObject.SetActive(false);
        mainMenu.SetActive(true);
    }
}
