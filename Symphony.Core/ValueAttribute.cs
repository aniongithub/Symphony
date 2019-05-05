using System;
namespace Symphony.Core
{
    public class ValueAttribute: Attribute
    {
        public ValueAttribute()
        {
        }

        public object Min { get; set; }
        public object Max { get; set; }
    }
}
