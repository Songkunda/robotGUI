using Avalonia;
using System;
// using Avalonia.ReactiveUI;

namespace WXRobotApp;

sealed class Program
{
    // 初始化代码。在 AppMain 被调用之前，不要使用任何 Avalonia、第三方 API 或依赖 SynchronizationContext 的代码：
    // 此时还未初始化，可能会导致问题。
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);

    // Avalonia 配置，不要删除；也被可视化设计器使用。
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
    // .UseReactiveUI();
}
