using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TankStats : Stats
{
    public float projectileSpeed;
    private Image expImage;
    private int exp;

    public void ChangeExp(int amount){
        exp += amount;
    }
}
