using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FloatReference: Reference<float>
{
    public FloatVariable _variable;
    public float Value
    {
        get { return UseConstant ? ConstantValue : _variable._value; }
    }
}