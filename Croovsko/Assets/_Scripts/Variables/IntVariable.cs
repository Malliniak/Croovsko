using UnityEngine;

[CreateAssetMenu]
public class IntVariable : BaseVariable<int>
{
    public void AddToValue(int amount)
    {
        _value += amount;
    }

    public void AddToValue(IntVariable amount)
    {
        _value += amount._value;
    }
}