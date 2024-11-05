using System;

namespace Reflection
{
    public class MyAppSettings : ConfigurationComponentBase
    {
        [ConfigurationItem("AppTitle", typeof(string))]
        public string AppTitle { get; set; }

        [ConfigurationItem("MaxUsers", typeof(int))]
        public int MaxUsers { get; set; }

        [ConfigurationItem("Timeout", typeof(TimeSpan))]
        public TimeSpan Timeout { get; set; }
    }
}