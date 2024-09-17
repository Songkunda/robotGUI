using System.Globalization;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using WXRobotApp.ViewModels;
using WXRobotApp.Views;

namespace WXRobotApp;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            SetCulture("");
            // 下面这行代码用于移除 Avalonia 的数据验证。
            // 如果没有这行代码，你会同时收到来自 Avalonia 和 CT 的重复验证。

            BindingPlugins.DataValidators.RemoveAt(0);
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
    public void SetCulture(string cultureCode)
    {
        Assets.Resources.Culture = new CultureInfo(cultureCode);
    }
}