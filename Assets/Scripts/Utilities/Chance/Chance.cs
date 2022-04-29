using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChanceItem<T>{
    [SerializeField] private T item;
    [SerializeField] private float chance;

    public T Item { get { return item; } }
    public float Chance { get { return chance; } }
}
