using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] highscoreTextFields;
    [SerializeField] private GameObject rankingPanel;

    


    void Start()
    {
        // highScoreEntryList = new List<HighScoreEntry>(){
        //     new HighScoreEntry { score = 5000, name = "Unknown"},
        //     new HighScoreEntry { score = 2000, name = "Unknown"},
        //     new HighScoreEntry { score = 3000, name = "Unknown"},
        //     new HighScoreEntry { score = 1000, name = "Unknown"},
        //     new HighScoreEntry { score = 4000, name = "Unknown"},
        // };
        // SaveScores();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void QuitGame(){
        Application.Quit();
    }

    public void BackToMenu(){
        SceneManager.LoadScene("MainMenu");
    }

    public void Ranking(){
        displayHighScores();
        rankingPanel.SetActive(true);
    }

    

    public void displayHighScores(){
        
        List<HighScoreEntry> highScoreEntryList = highScoreEntryList = SaveLoadManager.LoadScores();

        for (int i= 0; i<5; i++)
        {
            highscoreTextFields[i].text = highScoreEntryList[i].name + "                              " + highScoreEntryList[i].score;
        }
    }
}

