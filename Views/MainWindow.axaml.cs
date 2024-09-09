using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using wxRobotApp.Common;
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
        void OnAutoInjectClick(object sender, RoutedEventArgs e){
            Console.WriteLine("OnAutoInjectClick");
        }
        void OnDetailsClick(object sender, RoutedEventArgs e){
            Console.WriteLine($"OnDetailsClick sender: {sender}, e: {e}"); 
        }

        void OnDetailsClick(object sender, PointerPressedEventArgs e)
        {
            Console.WriteLine("OnDetailsClick");
            if (e.ClickCount == 2)
            {
                var dataGrid = sender as DataGrid;
                var selectedItem = dataGrid?.SelectedItem as WeChatProcessInfo;

                if (selectedItem != null)
                {
                    // 输出详细日志到控制台
                    Console.WriteLine($"Double-clicked item: Pid = {selectedItem.Pid}, MessagePort = {selectedItem.MessagePort}, WeChatVersion = {selectedItem.WeChatVersion}");

                    // 创建并打开详情窗口
                    var detailWindow = new DetailsWindow
                    {
                        DataContext = new DetailWindowViewModel(selectedItem)
                    };
                    detailWindow.Show();
                }
            }

        }
    }
}
