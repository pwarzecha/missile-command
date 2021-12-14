using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    EnemyMissileSpawner myEnemyMissileSpawner;

    [SerializeField] private GameObject endOfRoundPanel;
    public int score = 0;
    public int level = 1;
    public int playerMissilesLeft = 30;
    private int enemyMissilesLeftInRound = 0;
    private int enemyMissilesThisRound = 20;
    private int missileDestroyedPoints = 15;
    private bool isRoundOver = false;
    public float enemyMissileSpeed = 5f;
    public int currentMissilesLoadedInLauncher = 0;
    public int houseNumber = 0;
    [SerializeField] private float enemyMissleSpeedMultiplier = 0.2f;
    [SerializeField] private int missileEndOfRoundPoints = 3;
    
    [SerializeField] private int housesEndOfRoundPoints = 30;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI missileLeftText;
    [SerializeField] private TextMeshProUGUI missileLeftInLauncherText;
    [SerializeField] private TextMeshProUGUI missileBonusText;
    [SerializeField] private TextMeshProUGUI housesBonusText;
    [SerializeField] private TextMeshProUGUI totalBonusText;
    [SerializeField] private TextMeshProUGUI endPanelTimerText;


    void Start()
    {
        currentMissilesLoadedInLauncher = 10;
        playerMissilesLeft -= 10;

        myEnemyMissileSpawner = GameObject.FindObjectOfType<EnemyMissileSpawner>();
        houseNumber = GameObject.FindGameObjectsWithTag("Houses").Length - 1;
        
        UpdateMissilesLeft();
        UpdateScore();
        UpdateLevel();
        UpdateMissilesInLauncher();

        StartRound();
    }

    void Update()
    {
        if(enemyMissilesLeftInRound < 0 && !isRoundOver)
        {
            isRoundOver = true;
            StartCoroutine(EndOfRound());
        }
        if(houseNumber <=0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void UpdateMissilesLeft(){
        missileLeftText.text ="" + playerMissilesLeft;
        UpdateMissilesInLauncher();
    }

    public void UpdateMissilesInLauncher(){
        missileLeftInLauncherText.text ="" + currentMissilesLoadedInLauncher;
    }

    public void UpdateScore(){
        scoreText.text ="Score: " + score;
    }

    public void UpdateLevel(){
        levelText.text ="Level: " + level;
    }

    public void getScore(){
        score += missileDestroyedPoints;
        EnemyMissileDestroyed();
        UpdateScore();
    }

    public void EnemyMissileDestroyed(){
        enemyMissilesLeftInRound--;
    }

    public void PlayerFiredMissle(){
        if (currentMissilesLoadedInLauncher>0) {
            currentMissilesLoadedInLauncher--;
        }
        if (currentMissilesLoadedInLauncher == 0)
        {
            if (playerMissilesLeft >=10){
            currentMissilesLoadedInLauncher = 10;
            playerMissilesLeft -=10;
        }
        else {
            currentMissilesLoadedInLauncher = playerMissilesLeft;
            playerMissilesLeft = 0;
        }
        }
        UpdateMissilesLeft();
    }
    public void PlayerHit(){
        //playerMissilesLeft -= currentMissilesLoadedInLauncher;
        if (playerMissilesLeft >=10){
            currentMissilesLoadedInLauncher = 10;
            playerMissilesLeft -=10;
        }
        else {
            currentMissilesLoadedInLauncher = playerMissilesLeft;
            playerMissilesLeft = 0;
        }
        UpdateMissilesLeft();
        UpdateMissilesInLauncher();
    }

    private void StartRound(){
        myEnemyMissileSpawner.missilesCountToSpawn = enemyMissilesThisRound;
        enemyMissilesLeftInRound = enemyMissilesThisRound;
        myEnemyMissileSpawner.StartRound();
    }

    public IEnumerator EndOfRound() {
        yield return new WaitForSeconds(.5f);
        endOfRoundPanel.SetActive(true);
        int missileBonus = (playerMissilesLeft + currentMissilesLoadedInLauncher) * missileEndOfRoundPoints;
        GameObject[] houses = GameObject.FindGameObjectsWithTag("Houses");
        int houseBonus = houses.Length * housesEndOfRoundPoints;
        int totalBonus = missileBonus + houseBonus;

        if (level >= 2 && level < 4)
        {
            totalBonus *= 2;
        }
        else if (level >= 4 && level < 6){
            totalBonus *= 3;    
        }
        else if (level >= 6 && level < 8){
            totalBonus *= 4;    
        }
        else if (level >= 8){
            totalBonus *= 5;    
        }

        missileBonusText.text = "MISSILES BONUS : " + missileBonus;
        housesBonusText.text = ": " + houseBonus;
        totalBonusText.text = "TOTAL BONUS      : " + totalBonus;

        score += totalBonus;
        UpdateScore();

        endPanelTimerText.text ="3";
        yield return new WaitForSeconds(1f);
        endPanelTimerText.text ="2";
        yield return new WaitForSeconds(1f);
        endPanelTimerText.text ="1";
        yield return new WaitForSeconds(1f);

        endOfRoundPanel.SetActive(false);

        isRoundOver = false;
        //Next round -> new settings, eg. increase enemy missile speed
        playerMissilesLeft = 30; 
        enemyMissileSpeed += enemyMissleSpeedMultiplier;

        currentMissilesLoadedInLauncher = 10;
        playerMissilesLeft -= 10;

        StartRound();
        UpdateLevel();
        UpdateMissilesLeft();
    }
}
