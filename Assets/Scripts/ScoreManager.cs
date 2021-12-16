using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    private List<HighScoreEntry> highScoreEntryList;

    private void Awake() {
    if (instance == null){
        instance = this;
        DontDestroyOnLoad(gameObject);
    } else {
        Destroy(gameObject);
    }
}
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveScores(){
        //Sorting scores
        for (int i = 0; i < highScoreEntryList.Count; i++)
        {
            for (int j = i+1; j < highScoreEntryList.Count; j++)
            {
                if (highScoreEntryList[j].score > highScoreEntryList[i].score)
                {
                    HighScoreEntry tmpEntry = highScoreEntryList[i];
                    highScoreEntryList[i] = highScoreEntryList[j];
                    highScoreEntryList[j] = tmpEntry;
                }
            }
        }
        SaveLoadManager.SaveScores(highScoreEntryList);
    }

    public bool isThisANewHighScore(int score){
        highScoreEntryList = SaveLoadManager.LoadScores();

        highScoreEntryList.RemoveRange(5, highScoreEntryList.Count - 5);
        for (int i = 0; i < highScoreEntryList.Count; i++)
        {
            if(score > highScoreEntryList[i].score){
                return true;
            }
        }
        
        return false;
    }
   
   public void AddNewHighscore(HighScoreEntry newScore){
       highScoreEntryList.Add(newScore);
       SaveScores();
       highScoreEntryList.RemoveAt(5);
   }
}

[Serializable]
public class HighScoreEntry{
        public int score;
        public string name;
    }