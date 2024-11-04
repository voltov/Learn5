﻿using System;

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

            settings.AppTitle = "New App Title 2";
            settings.MaxUsers = 102;
            settings.Timeout = TimeSpan.FromMinutes(32);

            settings.SaveSettings();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}