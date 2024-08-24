using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Timers;
using Avalonia.Threading;


namespace wxRobotApp;

public partial class MainWindow : Window
{

    private ObservableCollection<WeChatProcessInfo> _wechatProcesses;
    private Timer _scanTimer = new Timer(); // 5秒;
    public MainWindow()
    {
        InitializeComponent();
        _wechatProcesses = new ObservableCollection<WeChatProcessInfo>();
        WeChatListBox.ItemsSource = _wechatProcesses;
        // 初始化并启动定时器
        InitializeTimer();
    }

    private void InitializeTimer()
    {
        _scanTimer.Interval = 5000;
        _scanTimer.Elapsed += OnScanTimerElapsed;
        _scanTimer.Start();
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
    private void OnScanTimerElapsed(object? sender, ElapsedEventArgs e)
    {
        // 扫描微信进程
        ScanWeChatProcesses();
    }

    private void ScanWeChatProcesses()
    {
        // 清空之前的列表
        Dispatcher.UIThread.Invoke(() => _wechatProcesses.Clear());

        // 获取所有微信进程
        var processes = Process.GetProcessesByName("WeChat");

        foreach (var process in processes)
        {
            var wechatInfo = new WeChatProcessInfo
            {
                Pid = process.Id,
                WeChatVersion = GetWeChatVersion(process.MainModule?.FileName),
                DllVersion = CheckHelperDll(process),
                WeChatId = GetWeChatId(process),
                MessagePort = GetMessagePort(process)
            };

            Dispatcher.UIThread.Invoke(() => _wechatProcesses.Add(wechatInfo));
        }
    }

    private string GetWeChatVersion(string? wechatPath)
    {
        if (wechatPath == null) return "Unknown";
        return FileVersionInfo.GetVersionInfo(wechatPath).FileVersion ?? "Unknown";
    }

    private string CheckHelperDll(Process process)
    {
        // 检查是否加载了helper.dll
        foreach (ProcessModule module in process.Modules)
        {
            if (module.ModuleName.Equals("helper.dll", StringComparison.OrdinalIgnoreCase))
            {
                return FileVersionInfo.GetVersionInfo(module.FileName).FileVersion ?? "Unknown";
            }
        }
        return "Not Injected";
    }

    private string GetWeChatId(Process process)
    {
        // 此处实现获取当前登录微信号的逻辑，可能需要内存读取或API调用
        return "UnknownWeChatId";
    }

    private int GetMessagePort(Process process)
    {
        // 此处实现获取消息发送端口号的逻辑，可能需要内存读取或API调用
        return 0;
    }

    private void OnWeChatListBoxItemDoubleClicked(object sender, RoutedEventArgs e)
    {
        if (WeChatListBox.SelectedItem is WeChatProcessInfo selectedProcess)
        {
            var detailsWindow = new DetailsWindow(selectedProcess);
            detailsWindow.ShowDialog(this);
        }
    }

}