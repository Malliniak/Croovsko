using UnityEditor;
using UnityEngine;

namespace _Scripts.Helpers
{
    public class AssetLoader
    {
        public static void GetAssetFile<T>(out T asset, string filter = "DefaultAsset l:noLabel t:noType") where T : ScriptableObject
        {
            string[] guids = AssetDatabase.FindAssets(filter);
            if(guids.Length > 0)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guids[0]); // only one loaded
                asset = AssetDatabase.LoadAssetAtPath(assetPath, typeof(T)) as T;
            }
            else
            {
                asset = null;
                Debug.LogWarning("There is not Asset File With this filter matching");
            }
        }
    }
}