using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    
    [SerializeField] private GameObject gameplayScreen;
    [SerializeField] private TextMeshProUGUI scoreText;
    [Space]
    [SerializeField] private GameObject resultsScreen;
    [SerializeField] private TextMeshProUGUI resultsScoreText;
    [SerializeField] private TextMeshProUGUI resultsRecordText;

    [Header("Settings")]
    [SerializeField] private Tank tank;
    [SerializeField] private bool isPlaying = true;

    [Header("Stats")]
    [SerializeField] private int score;

    public delegate void OnGameStart();
    public event OnGameStart onGameStart;
    public delegate void OnGameEnd();
    public event OnGameEnd onGameEnd;

    private void Awake(){
        Initialize();
    }

    private void Initialize(){
        ServiceLocator.Register<GameManager>(this);
        tank.onDeath += EndGame;
        StartGame();
    }
    
    public void StartGame(){
        isPlaying = true;
        if(onGameStart != null) onGameStart.Invoke();

        // Enable controlling the tank
        tank.GetComponent<TankController>().ToggleControls(true);
        tank.Stats.Initialize();
        HideResultsScreen();

        score = 0;
        RewardTank(0, 0);
    }

    public void DamageTank(int damage){
        tank.Stats.DealDamage(damage);
    }

    public void RewardTank(int points, int exp){
        if(!isPlaying) return;

        score += points;
        scoreText.text = score.ToString();

        tank.Stats.ChangeExp(exp);
    }

    private void EndGame(){
        isPlaying = false;
        if(onGameEnd != null) onGameEnd.Invoke();

        // Disable controlling the tank
        tank.GetComponent<TankController>().ToggleControls(false);

        if(score > PlayerPrefs.GetInt("Record", 0))
            PlayerPrefs.SetInt("Record", score);

        ShowResultsScreen();
    }

    private void HideResultsScreen(){
        resultsScreen.SetActive(false);
        gameplayScreen.SetActive(true);
    }

    private void ShowResultsScreen(){
        gameplayScreen.SetActive(false);
        resultsScreen.SetActive(true);

        resultsScoreText.text = score.ToString();
        resultsRecordText.text = PlayerPrefs.GetInt("Record", 0).ToString();
    }
}
