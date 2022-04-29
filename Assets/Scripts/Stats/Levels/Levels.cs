using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Levels : ScriptableObject
{
    [SerializeField] private List<Level> levelsList;
    public List<Level> LevelsList { get { return levelsList; } }
}