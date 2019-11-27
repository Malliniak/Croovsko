using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class IntReference: Reference<int>
{
    public IntVariable Variable;
    public int Value
    {
        get { return UseConstant ? ConstantValue : Variable.Value; }
    }
}