using ConfigurationManagerConfigurationProviderLibrary;
using FileConfigurationProviderLibrary;
using System;

namespace Reflection
{
    public class MyAppSettings : ConfigurationComponentBase
    {
        [ConfigurationItem("AppTitle", typeof(ConfigurationManagerConfigurationProvider))]
        public string AppTitle { get; set; }

        [ConfigurationItem("MaxUsers", typeof(FileConfigurationProvider))]
        public int MaxUsers { get; set; }

        [ConfigurationItem("Timeout", typeof(FileConfigurationProvider))]
        public TimeSpan Timeout { get; set; }
    }
}