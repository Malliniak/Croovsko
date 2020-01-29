using UnityEngine;

namespace _Scripts.Helpers
{
    public class AssetLoader
    {
        public static void GetAssetFile<T>(out T asset, string filter = "DefaultAsset l:noLabel t:noType")
            where T : ScriptableObject
        {
            Object[] scriptabels = Resources.LoadAll("Data/");
            foreach (Object variable in scriptabels)
                if (variable.name.Equals(filter))
                {
                    asset = variable as T;
                    return;
                }

            asset = null;
        }
    }
}