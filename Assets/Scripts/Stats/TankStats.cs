using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class TankStats : Stats
{
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float specialCooldown;
    [SerializeField] private Levels levels;
    [SerializeField] private Level currentLevel;

    [SerializeField][ReadOnly] private int currentLevelIndex;
    [SerializeField][ReadOnly] private int exp;

    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Image expImage;

    public float ProjectileSpeed { get { return projectileSpeed; } }
    private Level CurrentLevel {
        set { 
            currentLevel = value;
            currentLevelIndex = levels.LevelsList.IndexOf(currentLevel);
            levelText.text = (currentLevelIndex + 1).ToString();
            Damage = currentLevel.NewDamage;
            SpecialCooldown = currentLevel.NewCooldown;
        }
    }
    public float SpecialCooldown {
        get { return specialCooldown; }
        set {
            specialCooldown = value;
            if(onCooldownChanged != null) onCooldownChanged.Invoke(specialCooldown);
        }
    }

    public delegate void OnCooldownChanged(float cooldown);
    public event OnCooldownChanged onCooldownChanged;

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

        HealFully();
    }
}
