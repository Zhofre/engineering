using System;

namespace Engineering.Units.Attributes
{
    public class PrefixInformationAttribute : Attribute
    {
        public string Symbol { get; set; }
        public double Factor { get; set; }
    }
}