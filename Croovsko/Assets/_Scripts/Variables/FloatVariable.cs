using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FloatVariable : BaseVariable<float>
{
    public void AddToValue(float amount)
    {
        Value += amount;
    }

    public void AddToValue(FloatVariable amount)
    {
        Value += amount.Value;
    }
}