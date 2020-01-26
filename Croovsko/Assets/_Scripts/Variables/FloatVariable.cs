using UnityEngine;

[CreateAssetMenu]
public class FloatVariable : BaseVariable<float>
{
    public void AddToValue(float amount)
    {
        _value += amount;
    }

    public void AddToValue(FloatVariable amount)
    {
        _value += amount._value;
    }
}