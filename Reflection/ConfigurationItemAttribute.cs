using System;

namespace Reflection
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ConfigurationItemAttribute : Attribute
    {
        public string SettingName { get; }
        public Type ProviderType { get; }

        public ConfigurationItemAttribute(string settingName, Type providerType)
        {
            SettingName = settingName;
            ProviderType = providerType;
        }
    }
}