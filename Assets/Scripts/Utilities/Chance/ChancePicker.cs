using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChancePicker<T>
{
    [SerializeField] private List<ChanceItem<T>> items;

    public T Roll(){
        T item = default(T);
        float c = Random.Range(1, 101);
        float chanceSum = 0;

        for(int i = 0; i < items.Count; i++){
            float chance = items[i].Chance;
            if(c > chanceSum && c <= chanceSum + chance){
                item = items[i].Item;
                break;
            }else{
                chanceSum += chance;
            }
        }

        return item;
    }
}