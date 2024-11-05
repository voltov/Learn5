using System;

namespace Reflection
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ConfigurationItemAttribute : Attribute
    {
        public string SettingName { get; }
        public Type ValueType { get; }

        public ConfigurationItemAttribute(string settingName, Type valueType)
        {
            SettingName = settingName;
            ValueType = valueType;
        }
    }
}