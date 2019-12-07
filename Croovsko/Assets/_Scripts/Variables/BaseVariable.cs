using UnityEngine;

public abstract class BaseVariable<T> : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline] public string _developerDescription = "";
#endif
    public T _value;

    public void SetValue(T value)
    {
        _value = value;
    }

    public void SetValue(BaseVariable<T> value)
    {
        _value = value._value;
    }
}