using System.Globalization;
using Avalonia;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using wxRobotApp.Views;
using wxRobotApp.ViewModels;

namespace wxRobotApp;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);

        // 初始化配置文件
        // ConfigurationManager.Instance;
    }

    public override void OnFrameworkInitializationCompleted()
    {
        Assets.Resources.Culture = new CultureInfo("zh-CN");
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
          
            desktop.MainWindow = new MainWindow();
        }

        base.OnFrameworkInitializationCompleted();
    }
}