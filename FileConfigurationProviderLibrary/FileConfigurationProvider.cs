using System.IO;

namespace FileConfigurationProviderLibrary 
{
    public class FileConfigurationProvider
    {
        private const string _filePath = "config.txt";

        public FileConfigurationProvider()
        {
        }

        public string GetValue(string key)
        {
            if (!File.Exists(_filePath)) return null;
            var lines = File.ReadAllLines(_filePath);
            foreach (var line in lines)
            {
                var parts = line.Split('=');
                if (parts[0] == key) return parts[1];
            }
            return null;
        }

        public void SetValue(string key, string value)
        {
            var lines = File.Exists(_filePath) ? File.ReadAllLines(_filePath) : new string[0];
            using (var writer = new StreamWriter(_filePath))
            {
                bool found = false;
                foreach (var line in lines)
                {
                    var parts = line.Split('=');
                    if (parts[0] == key)
                    {
                        writer.WriteLine($"{key}={value}");
                        found = true;
                    }
                    else
                    {
                        writer.WriteLine(line);
                    }
                }
                if (!found)
                {
                    writer.WriteLine($"{key}={value}");
                }
            }
        }
    }
}
