using System;

namespace Reflection
{
    internal class Program
    {
        static void Main()
        {
            var settings = new MyAppSettings();
            settings.LoadSettings();

            Console.WriteLine($"AppTitle: {settings.AppTitle}");
            Console.WriteLine($"MaxUsers: {settings.MaxUsers}");
            Console.WriteLine($"Timeout: {settings.Timeout}");

            settings.AppTitle = "New App Title 1";
            settings.MaxUsers = 101;
            settings.Timeout = TimeSpan.FromMinutes(31);

            settings.SaveSettings();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
