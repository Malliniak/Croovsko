using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseVariable<T> : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif
    public T Value;

    public void SetValue(T value)
    {
        Value = value;
    }

    public void SetValue(BaseVariable<T> value)
    {
        Value = value.Value;
    }
}
