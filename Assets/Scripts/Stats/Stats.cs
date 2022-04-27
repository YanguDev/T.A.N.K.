using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Stats 
{
    
    public float moveSpeed;
    public int damage;
    [SerializeField] private Image healthImage;
    public int maxHealthPoints;

    [ReadOnly]
    [SerializeField] protected int currentHealthPoints;
    public int CurrentHealthPoints { get { return currentHealthPoints; } }

    public delegate void OnHealthChanged(int health);
    public event OnHealthChanged onHealthChanged;

    public virtual void Initialize(){
        currentHealthPoints = maxHealthPoints;
        if(healthImage != null)
            healthImage.fillAmount = 1;
    }

    public void ChangeSpeed(float normalizedPercent){
        normalizedPercent = Mathf.Clamp(normalizedPercent, -1, 1);
        moveSpeed += moveSpeed * normalizedPercent;
    }

    protected void ChangeHealth(int amount){
        currentHealthPoints = Mathf.Clamp(currentHealthPoints + amount, 0, maxHealthPoints);
        UpdateImageFill(healthImage, currentHealthPoints, maxHealthPoints);

        if(onHealthChanged != null) onHealthChanged.Invoke(currentHealthPoints);
    }

    public void Damage(int amount){
        ChangeHealth(-amount);
    }

    public void Heal(int amount){
        ChangeHealth(amount);
    }

    public void HealPercent(float normalizedPercent){
        normalizedPercent = Mathf.Clamp(normalizedPercent, 0, 1);
        ChangeHealth((int)(maxHealthPoints * normalizedPercent));
    }

    public void HealFully(){
        ChangeHealth(maxHealthPoints - currentHealthPoints);
    }

    protected void UpdateImageFill(Image image, int currentAmount, int maxAmount){
        if(image != null){
            if(!image.gameObject.activeSelf)
                image.gameObject.SetActive(true);
            image.fillAmount = (float)currentAmount/(float)maxAmount;
        }
    }
}
