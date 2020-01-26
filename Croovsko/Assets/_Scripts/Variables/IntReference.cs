using System;

[Serializable]
public class IntReference : Reference<int>
{
    public IntVariable _variable;

    public int Value => UseConstant ? ConstantValue : _variable._value;
}