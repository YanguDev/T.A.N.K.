using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TankStats : Stats
{
    public float specialCooldown;
    [SerializeField] private Image expImage;
    private int exp;
    [SerializeField] private Levels levels;

    [ReadOnly]
    [SerializeField] private int currentLevelIndex;
    [SerializeField] private Level currentLevel;

    public float projectileSpeed;

    public override void Initialize()
    {
        base.Initialize();

        exp = 0;
        expImage.fillAmount = 0;
        currentLevelIndex = 0;
        currentLevel = levels.LevelsList[0];
    }

    public void ChangeExp(int amount){
        exp += amount;
        if(currentLevelIndex < levels.LevelsList.Count-1 && exp >= currentLevel.RequiredExp)
            LevelUp();

        UpdateImageFill(expImage, exp, currentLevel.RequiredExp);
    }

    private void LevelUp(){
        exp -= currentLevel.RequiredExp;
        currentLevel = levels.LevelsList[++currentLevelIndex];

        damage = currentLevel.NewDamage;
        specialCooldown = currentLevel.NewCooldown;

        ChangeHealth(maxHealthPoints - currentHealthPoints);
    }
}
