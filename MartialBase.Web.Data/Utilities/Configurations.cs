// <copyright file="Configurations.cs" company="Martialtech®">
// Solution: MartialBase.Web
// Project: MartialBase.Web.Data
// Copyright © 2020 Martialtech®. All rights reserved.
// </copyright>

using System.IO;
using System.Reflection;

using Microsoft.Extensions.Configuration;

namespace MartialBase.Web.Data.Utilities
{
    public static class Configurations
    {
        public static IConfiguration GetConfigurationFromAssembly(string appSettingsFileName)
        {
            // Get the MartialBase.API.Data assembly containing the required JSON file
            Assembly dataAssembly = Assembly.LoadFrom(
                Path.Combine(
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    "MartialBase.Web.Data.dll"));

            Stream jsonStream =
                dataAssembly.GetManifestResourceStream($"MartialBase.Web.Data.{appSettingsFileName}");

            // Builds and returns a configuration using above app settings file
            return new ConfigurationBuilder()
                .AddJsonStream(jsonStream)
                .SetBasePath(Directory.GetCurrentDirectory())
                .Build();
        }

        public static IConfiguration GetConfigurationFromFile(string appSettingsFileName)
        {
            // Set the app settings file path within the current output folder
            string appSettingsPath = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                appSettingsFileName);

            // Builds and returns a configuration using above app settings file
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(appSettingsPath, optional: false, reloadOnChange: true)
                .Build();
        }
    }
}
