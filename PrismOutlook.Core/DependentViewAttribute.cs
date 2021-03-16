using System;
using System.Collections.Generic;
using System.Text;

namespace PrismOutlook.Core
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DependentViewAttribute : Attribute
    {
        public DependentViewAttribute(string region, Type type)
        {
            if (region == null)
                throw new ArgumentNullException(nameof(region));
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            Region = region;
            Type = type;
        }

        public string Region { get; set; }
        public Type Type { get; set; }
    }
}
