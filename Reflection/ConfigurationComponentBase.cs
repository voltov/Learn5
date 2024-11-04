using ConfigurationManagerConfigurationProviderLibrary;
using System;
using System.Linq;
using System.Reflection;

namespace Reflection
{
    public abstract class ConfigurationComponentBase
    {
        public void LoadSettings()
        {
            var objType = GetType();
            var properties = objType.GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(ConfigurationItemAttribute)));

            foreach (var property in properties)
            {
                var attribute = (ConfigurationItemAttribute)property.GetCustomAttribute(typeof(ConfigurationItemAttribute));
                object provider;
                if (attribute.ProviderType == typeof(ConfigurationManagerConfigurationProvider))
                {
                    provider = Activator.CreateInstance(attribute.ProviderType);
                }
                else
                {
                    provider = Activator.CreateInstance(attribute.ProviderType, "config.txt");
                }
                var getValueMethod = provider.GetType().GetMethod("GetValue");
                if (getValueMethod != null)
                {
                    var value = getValueMethod.Invoke(provider, new object[] { attribute.SettingName }) as string;

                    if (value != null)
                    {
                        if (property.PropertyType == typeof(int))
                        {
                            property.SetValue(this, int.Parse(value));
                        }
                        else if (property.PropertyType == typeof(float))
                        {
                            property.SetValue(this, float.Parse(value));
                        }
                        else if (property.PropertyType == typeof(string))
                        {
                            property.SetValue(this, value);
                        }
                        else if (property.PropertyType == typeof(TimeSpan))
                        {
                            property.SetValue(this, TimeSpan.Parse(value));
                        }
                    }
                }
            }
        }

        public void SaveSettings()
        {
            var properties = GetType().GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(ConfigurationItemAttribute)));

            foreach (var property in properties)
            {
                var attribute = (ConfigurationItemAttribute)property.GetCustomAttribute(typeof(ConfigurationItemAttribute));
                object provider;
                if (attribute.ProviderType == typeof(ConfigurationManagerConfigurationProvider))
                {
                    provider = Activator.CreateInstance(attribute.ProviderType);
                }
                else
                {
                    provider = Activator.CreateInstance(attribute.ProviderType, "config.txt");
                }

                var setValueMethod = provider.GetType().GetMethod("SetValue");
                if (setValueMethod != null)
                {
                    var value = property.GetValue(this)?.ToString();
                    setValueMethod.Invoke(provider, new object[] { attribute.SettingName, value });
                }
            }
        }
    }
}