using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Stats 
{
    [SerializeField] private Image healthImage;
    public int maxHealthPoints;
    public float moveSpeed;

    [ReadOnly]
    [SerializeField] private int currentHealthPoints;

    public delegate void OnHealthChanged(int health);
    public event OnHealthChanged onHealthChanged;

    public void Initialize(){
        currentHealthPoints = maxHealthPoints;
        if(healthImage != null)
            healthImage.fillAmount = 1;
    }

    public void ChangeHealth(int amount){
        currentHealthPoints += amount;

        if(healthImage != null){
            if(!healthImage.gameObject.activeSelf)
                healthImage.gameObject.SetActive(true);
            healthImage.fillAmount = (float)currentHealthPoints/(float)maxHealthPoints;
        }

        if(onHealthChanged != null) onHealthChanged.Invoke(currentHealthPoints);
    }
}
