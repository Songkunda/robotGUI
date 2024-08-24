using Avalonia.Controls;
using System;
using System.IO;
using Avalonia.Interactivity;
using Newtonsoft.Json.Linq;

namespace wxRobotApp
{
    public partial class SettingsWindow : Window
    {
        private string _configFilePath;

        public SettingsWindow()
        {
            InitializeComponent();

            // 根据操作系统决定使用哪个配置文件
            var platform = OperatingSystem.IsWindows() ? "windows" :
                OperatingSystem.IsMacOS() ? "macos" :
                OperatingSystem.IsLinux() ? "linux" : "unknown";

            _configFilePath = $"config-{platform}.json";

            if (File.Exists(_configFilePath))
            {
                // 读取配置文件内容并显示在文本框中
                ConfigTextBox.Text = File.ReadAllText(_configFilePath);
            }
            else
            {
                // 如果配置文件不存在，创建默认配置文件
                var defaultConfig = new JObject
                {
                    ["Setting1"] = "Default Value 1",
                    ["Setting2"] = "Default Value 2"
                };
                File.WriteAllText(_configFilePath, defaultConfig.ToString());
                ConfigTextBox.Text = defaultConfig.ToString();
            }
        }

        private void OnSaveClick(object? sender, RoutedEventArgs e)
        {
            // 保存配置内容到文件
            File.WriteAllText(_configFilePath, ConfigTextBox.Text);
            this.Close();
        }
    }
}