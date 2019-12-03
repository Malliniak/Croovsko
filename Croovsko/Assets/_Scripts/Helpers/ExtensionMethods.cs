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
                   Quaternion.Euler(0, 0, 45 * Mathf.Sign(lookVec.x)), Time.deltaTime * lerpTime);
    }
    
    public static bool GetAssetFile<T>(out T asset, string filter = "DefaultAsset l:noLabel t:noType") where T : ScriptableObject
    {
        bool found = false;
        string[] guids = AssetDatabase.FindAssets(filter);
        if(guids.Length > 0)
        {
            found = true;
            string assetPath = AssetDatabase.GUIDToAssetPath(guids[0]); // only one loaded
            asset = AssetDatabase.LoadAssetAtPath(assetPath, typeof(T)) as T;
        }
        else
        {
            asset = null;
            Debug.LogWarning("There is not a Level State File for this scene");
        }


        return found;
    }
}
