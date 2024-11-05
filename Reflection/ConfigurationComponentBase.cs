using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Reflection
{
    public abstract class ConfigurationComponentBase
    {
        public void LoadSettings()
        {
            var ConfigurationManagerConfigurationProviderLibrary = Assembly.LoadFrom($"{Directory.GetCurrentDirectory()}\\ConfigurationManagerConfigurationProviderLibrary.dll");
            var FileConfigurationProviderLibrary = Assembly.LoadFrom($"{Directory.GetCurrentDirectory()}\\FileConfigurationProviderLibrary.dll");

            var objType = GetType();
            var properties = objType.GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(ConfigurationItemAttribute)));

            foreach (var property in properties)
            {
                var attribute = (ConfigurationItemAttribute)property.GetCustomAttribute(typeof(ConfigurationItemAttribute));
                object instance;
                dynamic method;
                if (attribute.ValueType == typeof(string))
                {
                    var vova = ConfigurationManagerConfigurationProviderLibrary.GetTypes();
                    var type = ConfigurationManagerConfigurationProviderLibrary.GetType("ConfigurationManagerConfigurationProviderLibrary.ConfigurationManagerConfigurationProvider");
                    instance = Activator.CreateInstance(type);
                    method = type.GetMethod("GetValue");
                } else
                {
                    var type = FileConfigurationProviderLibrary.GetType("FileConfigurationProviderLibrary.FileConfigurationProvider");
                    instance = Activator.CreateInstance(type);
                    method = type.GetMethod("GetValue");
                }
                

                if (method != null)
                {
                    var value = method.Invoke(instance, new object[] { attribute.SettingName }) as string;
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
            var ConfigurationManagerConfigurationProviderLibrary = Assembly.LoadFrom("ConfigurationManagerConfigurationProviderLibrary.dll");
            var FileConfigurationProviderLibrary = Assembly.LoadFrom("FileConfigurationProviderLibrary.dll");

            var properties = GetType().GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(ConfigurationItemAttribute)));

            foreach (var property in properties)
            {
                var attribute = (ConfigurationItemAttribute)property.GetCustomAttribute(typeof(ConfigurationItemAttribute));
                object instance;
                dynamic method;
                if (attribute.ValueType == typeof(string))
                {
                    var type = ConfigurationManagerConfigurationProviderLibrary.GetType("ConfigurationManagerConfigurationProviderLibrary.ConfigurationManagerConfigurationProvider");
                    instance = Activator.CreateInstance(type);
                    method = type.GetMethod("SetValue");
                }
                else
                {
                    var type = FileConfigurationProviderLibrary.GetType("FileConfigurationProviderLibrary.FileConfigurationProvider");
                    instance = Activator.CreateInstance(type);
                    method = type.GetMethod("SetValue");
                }

                if (method != null)
                {
                    var value = property.GetValue(this)?.ToString();
                    method.Invoke(instance, new object[] { attribute.SettingName, value });
                }
            }
        }
    }
}