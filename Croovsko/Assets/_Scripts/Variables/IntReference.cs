using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class IntReference: Reference<int>
{
    public IntVariable _variable;
    public int Value
    {
        get { return UseConstant ? ConstantValue : _variable._value; }
    }
}