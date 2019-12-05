using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Vector2Reference: Reference<Vector2>
{
    public Vector2Variable _variable;
    public Vector2 Value
    {
        get { return UseConstant ? ConstantValue : _variable._value; }
    }
}    