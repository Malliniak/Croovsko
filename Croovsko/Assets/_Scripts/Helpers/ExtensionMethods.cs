using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class ExtensionMethods
{
    public static void LookAt2d(this Transform transform, Vector3 lookVec)
    {
        float _rot_z = Mathf.Atan2(lookVec.y, lookVec.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, _rot_z + 90f);
    }
    
    //THIS WORKS, DO NOT TOUCH!
    
    public static void LookAt2d(this Transform transform, Vector3 lookVec, float lerpTime)
    {
        float _rot_z = Mathf.Atan2(lookVec.y, lookVec.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.localRotation,
                   Quaternion.Euler(new Vector3(0, 0, _rot_z + 90f)), Time.deltaTime * lerpTime);
    }
}
