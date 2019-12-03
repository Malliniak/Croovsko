using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class IntVariable : BaseVariable<int>
{
    public void AddToValue(int amount)
    {
        Value += amount;
    }

    public void AddToValue(IntVariable amount)
    {
        Value += amount.Value;
    }
}