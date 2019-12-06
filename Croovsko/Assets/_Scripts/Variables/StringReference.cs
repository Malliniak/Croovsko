using System;

namespace _Scripts.Variables
{
    [Serializable]
    public class StringReference : Reference<string>
    {
        public StringVariable _variable;

        public string Value => UseConstant ? ConstantValue : _variable._value;
    }
}