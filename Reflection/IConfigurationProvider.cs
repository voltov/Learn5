using System;

public interface IConfigurationProvider
{
    string GetValue(string key);
    void SetValue(string key, string value);
}

