using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using WXRobotApp.Common;
using WXRobotApp.ViewModels;


namespace WXRobotApp.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    void OpenAutoInjectWorker(object sender, RoutedEventArgs e)
    {
        Console.WriteLine("OpenAutoInjectWorker");
    }



    // MARK: 设置
    // MARK: -- 设置

    // 打开全局设置窗口
    private void OpenSettingsWithGlobalSettingsWindow(object? sender, RoutedEventArgs e)
    {
        // 打开设置窗口
        var settingsWindow = new SettingsWindow();
        settingsWindow.ShowDialog(this);
    }
    // MARK: -- 工具
    // 打开工具: 注入工具窗口
    void OpenToolsWithInjectToolWindow(Object sender, RoutedEventArgs e)
    {
        var injectDllWindow = new InjectDllWindow();
        injectDllWindow.ShowDialog(this);

    }
    // MARK: -- 语言
    // 设置语言为 简体中文
    private void ChangedLanguagesWithZHCN(object sender, RoutedEventArgs e)
    {

        ((App)Application.Current).SetCulture("zh-CN");
        Console.WriteLine("OnLangsCHClick");

    }
    // MARK: 微信进程列表
    // 双击单个微信ROW 进入微信详情
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
}