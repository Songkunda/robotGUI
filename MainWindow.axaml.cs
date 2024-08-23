using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Diagnostics;
using System;
namespace wxRobotApp;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public void ButtonClicked(object sender, RoutedEventArgs e)
    {
        Debug.WriteLine($"Clicked: {sender}");
    }

    private void OnClickMeButton(object? sender, RoutedEventArgs e)
    {
        OutputTextBox.Text += $"Button clicked at {DateTime.Now}\n";
    }

    private void OnSettingsClick(object? sender, RoutedEventArgs e)
    {
        // 打开设置窗口
        var settingsWindow = new SettingsWindow();
        settingsWindow.ShowDialog(this);
    }
}