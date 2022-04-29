using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Stats 
{
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected int damage;
    [SerializeField] protected int maxHealthPoints;
    [SerializeField][ReadOnly] protected int currentHealthPoints;
    [SerializeField] private Image healthImage;

    public float MoveSpeed { get { return moveSpeed; } }
    public int Damage { 
        get { return damage; }
        set {
            damage = value;
            if(onDamageChanged != null) onDamageChanged.Invoke(damage);
        }
    }
    public int MaxHealthPoints { get { return maxHealthPoints; } }
    public int CurrentHealthPoints { 
        get { return currentHealthPoints; }
        set {
            currentHealthPoints = Mathf.Clamp(value, 0, maxHealthPoints);
            UpdateImageFill(healthImage, currentHealthPoints, maxHealthPoints);

            if(onHealthChanged != null) onHealthChanged.Invoke(currentHealthPoints);
        }
    }

    public delegate void OnHealthChanged(int health);
    public event OnHealthChanged onHealthChanged;
    public delegate void OnDamageChanged(int damage);
    public event OnDamageChanged onDamageChanged;

    public virtual void Initialize(){
        currentHealthPoints = maxHealthPoints;
        if(healthImage != null)
            healthImage.fillAmount = 1;
    }

    public void ChangeSpeed(float normalizedPercent){
        normalizedPercent = Mathf.Clamp(normalizedPercent, -1, 1);
        moveSpeed += moveSpeed * normalizedPercent;
    }

    public void DealDamage(int amount){
        CurrentHealthPoints -= amount;
    }

    public void Heal(int amount){
        CurrentHealthPoints += amount;
    }

    public void HealPercent(float normalizedPercent){
        normalizedPercent = Mathf.Clamp(normalizedPercent, 0, 1);
        CurrentHealthPoints += (int)(maxHealthPoints * normalizedPercent);
    }

    public void HealFully(){
        CurrentHealthPoints = maxHealthPoints;
    }

    protected void UpdateImageFill(Image image, int currentAmount, int maxAmount){
        if(image != null){
            if(!image.gameObject.activeSelf)
                image.gameObject.SetActive(true);
            image.fillAmount = (float)currentAmount/(float)maxAmount;
        }
    }
}
