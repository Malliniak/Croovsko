using System;

[Serializable]
public class FloatReference : Reference<float>
{
    public FloatVariable _variable;

    public float Value => UseConstant ? ConstantValue : _variable._value;
}