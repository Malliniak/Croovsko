using System;

namespace _Scripts.Variables
{
    [Serializable]
    public class StringReference: Reference<string>
    {
        public StringVariable Variable;
        public string Value
        {
            get { return UseConstant ? ConstantValue : Variable.Value; }
        }
    }
}