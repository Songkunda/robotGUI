using Avalonia.Controls;
using Avalonia.Interactivity;
using wxRobotApp.Models;

namespace wxRobotApp.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // 设置DataContext为MainViewModel
            DataContext = new MainViewModel();
        }

        private void OnSettingsClick(object? sender, RoutedEventArgs e)
        {
            // 打开设置窗口
            var settingsWindow = new SettingsWindow();
            settingsWindow.ShowDialog(this);
        }
    }
}
