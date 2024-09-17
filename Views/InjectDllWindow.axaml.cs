// InjectDllWindow.axaml.cs
using System;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace wxRobotApp.Views
{
    public partial class InjectDllWindow : Window
    {
        public InjectDllWindow()
        {
            InitializeComponent();
        }

        private void OnInjectClick(object? sender, RoutedEventArgs e)
        {
            Console.WriteLine("onInjectDllBtnClick");
        }

        private void OnUnjectClick(object? sender, RoutedEventArgs e)
        {
            Console.WriteLine("onUnjectDllBtnClick");
        }
    }
}
