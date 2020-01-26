using UnityEditor;
using UnityEngine;

namespace _Scripts.Helpers
{
    public class AssetLoader
    {
        public static void GetAssetFile<T>(out T asset, string filter = "DefaultAsset l:noLabel t:noType")
            where T : ScriptableObject
        {
            var _scriptabels = Resources.LoadAll("Data/");
            foreach (var VARIABLE in _scriptabels)
            {
                //Debug.LogWarning($"Lookinig for: {filter}, current: {VARIABLE.name}");
                if (VARIABLE.name.Equals(filter))
                {
                    asset = VARIABLE as T;
                    return;
                }
            }
            asset = null;
            //Debug.LogWarning("NO ASSET :( ");
        }
    }
}