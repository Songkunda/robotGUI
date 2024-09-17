using System.Collections.ObjectModel;
using WXRobotApp.Common;
using WXRobotApp.Utils;


namespace WXRobotApp.ViewModels;


public partial class MainWindowViewModel : ViewModelBase
{
#pragma warning disable CA1822 // Mark members as static
    public ObservableCollection<WeChatProcessInfo> WeChatProcessInfos { get; set; } = [];

#pragma warning restore CA1822 // Mark members as static

    public MainWindowViewModel()
    {

        foreach (var process in WeChatProcesses.ScanWeChatProcesses())
        {
            WeChatProcessInfos.Add(process);  // This works with ObservableCollection
        }
    }
}
