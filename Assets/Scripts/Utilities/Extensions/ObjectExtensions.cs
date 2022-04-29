using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectExtensions
{
    public static void SetRandomMaterialColor(this MonoBehaviour obj){
        obj.GetComponent<MeshRenderer>().material.color = ColorUtility.GetRandomColor();
    }

    
}
