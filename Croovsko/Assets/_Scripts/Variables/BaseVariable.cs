//
//  Square
//
//  Created by Krzysztof Maliński, Adam Kolinski, 
// Damian Klabuhn, Mikolaj Mikolajczak
//  
// Code inspired by Croovsko, project by Krzysztof Malinski
// 


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