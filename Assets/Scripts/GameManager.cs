using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    [SerializeField] private int score;

    private void Awake(){
        Initialize();
    }

    private void Initialize(){
        ServiceLocator.Register<GameManager>(this);
        score = 0;
        AddScore(0);
    }

    public void AddScore(int amount){
        score += amount;
        scoreText.text = score.ToString();
    }
}
