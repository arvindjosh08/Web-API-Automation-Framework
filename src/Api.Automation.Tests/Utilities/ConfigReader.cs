using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Api.Automation.Tests.Utilities
{
    public static class ConfigReader
    {
        private static readonly IConfigurationRoot config;

        static ConfigReader()
        {
            config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("src/Api.Automation.Tests/Config/AppSetting.json", optional: false, reloadOnChange: true)
                .Build();
        }

        public static string Get(string key) => config[key];
        public static int GetInt(string key) => int.Parse(config[key]);
    }
}