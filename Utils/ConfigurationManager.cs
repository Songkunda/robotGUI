using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace wxRobotApp.Utils
{
    public class ConfigurationManager
    {
        private static ConfigurationManager? _instance;
        private readonly string _configFilePath;
        public JObject Config { get; private set; }

        private ConfigurationManager()
        {
            var platform = OperatingSystem.IsWindows() ? "windows" :
                OperatingSystem.IsMacOS() ? "macos" :
                OperatingSystem.IsLinux() ? "linux" : "unknown";

            _configFilePath = Path.Combine("configs", $"config-{platform}.json");

            // // Ensure the directory exists
            // var directoryPath = Path.GetDirectoryName(_configFilePath);

            // if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
            // {
            //     Directory.CreateDirectory(directoryPath);
            // }

            if (File.Exists(_configFilePath))
            {
                // Read configuration from file
                var json = File.ReadAllText(_configFilePath);
                Config = JObject.Parse(json);
            }
            else
            {
                // Create default configuration
                Config = new JObject
                {
                    ["Setting1"] = "Default Value 1",
                    ["Setting2"] = "Default Value 2"
                };
                SaveConfig();
            }
        }

        public static ConfigurationManager Instance => _instance ??= new ConfigurationManager();
        public void UpdateConfig(JObject newConfig)
        {
            Config = newConfig;
            SaveConfig();
        }

        public void SaveConfig()
        {
            // Ensure the directory exists
            var directoryPath = Path.GetDirectoryName(_configFilePath);

            if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            File.WriteAllText(_configFilePath, Config.ToString());
        }
    }
}
