using System;

namespace Variables
{
    public class Variable
    {
        public String Name { get; set; }
        public ArgymentType Type { get; set; }
        public Object Value { get; set; }
        public Variable(String name, ArgymentType type, object value=null)
        {
            Name = name;
            Type = type;
            Value = value;
        }

        public Variable()
        {

        }
    }
}
