using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using wxRobotApp.ViewModels;


namespace wxRobotApp.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
             DataContext = new MainWindowViewModel(); 
        }

        private void OnSettingsClick(object? sender, RoutedEventArgs e)
        {
            // 打开设置窗口
            var settingsWindow = new SettingsWindow();
            settingsWindow.ShowDialog(this);
        }

        void OnDetailsClick(object? sender, RoutedEventArgs e)
        {
            new DetailsWindow().ShowDialog(this);
        }
    }
}
