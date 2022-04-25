using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class TankStats : Stats
{
    public float specialCooldown;
    [SerializeField] private Image expImage;
    [SerializeField] private TextMeshProUGUI levelText;
    private int exp;
    [SerializeField] private Levels levels;

    [ReadOnly]
    [SerializeField] private int currentLevelIndex;
    [SerializeField] private Level currentLevel;
    private Level CurrentLevel {
        set { 
            currentLevel = value;
            currentLevelIndex = levels.LevelsList.IndexOf(currentLevel);
            levelText.text = (currentLevelIndex + 1).ToString();
            damage = currentLevel.NewDamage;
            specialCooldown = currentLevel.NewCooldown;
        }
    }

    public float projectileSpeed;

    public override void Initialize()
    {
        base.Initialize();

        exp = 0;
        expImage.fillAmount = 0;
        CurrentLevel = levels.LevelsList[0];
    }

    public void ChangeExp(int amount){
        exp += amount;
        if(currentLevelIndex < levels.LevelsList.Count-1 && exp >= currentLevel.RequiredExp)
            LevelUp();

        UpdateImageFill(expImage, exp, currentLevel.RequiredExp);
    }

    private void LevelUp(){
        exp -= currentLevel.RequiredExp;
        CurrentLevel = levels.LevelsList[++currentLevelIndex];

        ChangeHealth(maxHealthPoints - currentHealthPoints);
    }
}
