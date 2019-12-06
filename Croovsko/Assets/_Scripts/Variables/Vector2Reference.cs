using System;
using UnityEngine;

[Serializable]
public class Vector2Reference : Reference<Vector2>
{
    public Vector2Variable _variable;

    public Vector2 Value => UseConstant ? ConstantValue : _variable._value;
}