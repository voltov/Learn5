using System.Configuration;

namespace ConfigurationManagerConfigurationProviderLibrary
{
    public class ConfigurationManagerConfigurationProvider
    {
        public string GetValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public void SetValue(string key, string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Remove(key);
            config.AppSettings.Settings.Add(key, value);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
