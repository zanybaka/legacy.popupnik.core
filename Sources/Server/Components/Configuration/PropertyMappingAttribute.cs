using System;

namespace Popupnik.Server.Components.Configuration
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class PropertyMappingAttribute : Attribute
    {
        public PropertyMappingAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}