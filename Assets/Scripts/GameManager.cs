using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public Tank tank;
    [SerializeField] private int score;

    private void Awake(){
        Initialize();
    }

    private void Initialize(){
        ServiceLocator.Register<GameManager>(this);
        score = 0;
        RewardTank(0, 0);
    }

    public void RewardTank(int points, int exp){
        score += points;
        scoreText.text = score.ToString();

        tank.stats.ChangeExp(exp);
    }
}
