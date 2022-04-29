using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{
    public static void StandOnFloor(this Transform t, Transform floor){
        Vector3 pos = t.position;
        pos.y = t.localScale.y/2 + floor.localScale.y/2;
        t.position = pos;
    }
}
