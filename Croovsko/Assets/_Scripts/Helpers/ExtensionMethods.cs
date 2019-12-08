using UnityEngine;

namespace _Scripts.Helpers
{
    public static class ExtensionMethods
    {
        public static void LookAt2d(this Transform transform, Vector3 lookVec)
        {
            float rotZ = Mathf.Atan2(lookVec.y, lookVec.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + 90f);
        }

        //THIS WORKS, DO NOT TOUCH!

        public static void LookAt2d(this Transform transform, Vector3 lookVec, float lerpTime)
        {
            float rotZ = Mathf.Atan2(lookVec.y, lookVec.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(transform.localRotation,
                Quaternion.Euler(new Vector3(0, 0, rotZ + 90f)), Time.deltaTime * lerpTime);
        }
    }
}