using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FloatReference: Reference<float>
{
    public FloatVariable Variable;
    public float Value
    {
        get { return UseConstant ? ConstantValue : Variable.Value; }
    }
}