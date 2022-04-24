using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Levels : ScriptableObject
{
    [SerializeField] private List<Level> levelsList;
    public List<Level> LevelsList { get { return levelsList; } }
}

[System.Serializable]
public class Level{
    [SerializeField] private int requiredExp;
    [SerializeField] private int newDamage;
    [SerializeField] private float newCooldown;

    public int RequiredExp { get { return requiredExp; } }
    public int NewDamage { get { return newDamage; } }
    public float NewCooldown { get { return newCooldown; } }
}
