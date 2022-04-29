using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling<T> where T : MonoBehaviour
{
    private int initialAmount;
    private T pooledObject;
    private List<T> pooledObjects;

    public Pooling(int initialAmount, T pooledObject){
        this.initialAmount = initialAmount;
        this.pooledObject = pooledObject;

        pooledObjects = new List<T>();

        CreateInitial();
    }

    public T GetObject(bool active){
        foreach(T obj in pooledObjects){
            if(!obj.gameObject.activeSelf){
                obj.gameObject.SetActive(active);
                return obj;
            }
        }
        
        CreateObject();
        return GetObject(active);
    }

    public void StoreObject(T obj){
        obj.gameObject.SetActive(false);
        pooledObjects.Add(obj);
    }

    private void CreateInitial(){
        for(int i = 0; i < initialAmount; i++){
            CreateObject();
        }
    }

    private void CreateObject(){
        T obj = Object.Instantiate(pooledObject, Vector3.zero, Quaternion.identity);
        obj.gameObject.SetActive(false);
        pooledObjects.Add(obj);
    }
}
