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
    [SerializeField] private int currentHealthPoints;

    public delegate void OnHealthChanged(int health);
    public event OnHealthChanged onHealthChanged;

    public virtual void Initialize(){
        currentHealthPoints = maxHealthPoints;
        if(healthImage != null)
            healthImage.fillAmount = 1;
    }

    public void ChangeHealth(int amount){
        currentHealthPoints += amount;
        UpdateImageFill(healthImage, currentHealthPoints, maxHealthPoints);

        if(onHealthChanged != null) onHealthChanged.Invoke(currentHealthPoints);
    }

    protected void UpdateImageFill(Image image, int currentAmount, int maxAmount){
        if(image != null){
            if(!image.gameObject.activeSelf)
                image.gameObject.SetActive(true);
            image.fillAmount = (float)currentAmount/(float)maxAmount;
        }
    }
}
