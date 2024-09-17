using System;
using Avalonia;
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
        void OnAutoInjectClick(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("OnAutoInjectClick");
        }
        void OnDetailsClick(object sender, RoutedEventArgs e)
        {
            Console.WriteLine($"OnDetailsClick sender: {sender}, e: {e}");
        }
        void OnInjectDllsClick(Object sender, RoutedEventArgs e)
        {
            var injectDllWindow = new InjectDllWindow();
            injectDllWindow.ShowDialog(this);

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
                    var detailWindow = new DetailWindow
                    {
                        DataContext = new DetailWindowViewModel(selectedItem)
                    };
                    detailWindow.Show();
                }
            }

        }
        private void OnLangsCHClick(object sender, RoutedEventArgs e)
        {

            ((App)Application.Current).SetCulture("zh-CN");
            Console.WriteLine("OnLangsCHClick");

        }
        // 双击事件处理方法
        private void OnRowDoubleTapped(object sender, RoutedEventArgs e)
        {
            var dataGrid = sender as DataGrid;

            // 获取当前选中的行数据
            var selectedItem = dataGrid?.SelectedItem as WeChatProcessInfo;

            if (selectedItem != null)
            {
                // 输出日志到控制台以检查是否正确捕获了选中行
                Console.WriteLine($"Double-clicked item: Pid = {selectedItem.Pid}, MessagePort = {selectedItem.MessagePort}, WeChatVersion = {selectedItem.WeChatVersion}");

                // 创建并打开详情窗口，传递选中的数据
                var detailWindow = new DetailWindow
                {
                    DataContext = new DetailWindowViewModel(selectedItem)
                };
                detailWindow.Show();
            }
        }
    }
}
