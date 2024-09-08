using Avalonia.Controls;
using System;
using System.IO;
using Avalonia.Interactivity;
using Newtonsoft.Json.Linq;
using wxRobotApp.Utils;

namespace wxRobotApp.Views
{
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();

            // 读取配置文件
            var config = ConfigurationManager.Instance.Config;
            ConfigTextBox.Text = config.ToString();
        }

        private void OnSaveClick(object? sender, RoutedEventArgs e)
        {
            var config = JObject.Parse(ConfigTextBox.Text ?? "{}");
            ConfigurationManager.Instance.UpdateConfig(config);

            // this.Close();
        }
    }
}