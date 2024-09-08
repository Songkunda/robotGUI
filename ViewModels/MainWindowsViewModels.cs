using System.Collections.ObjectModel;
using DynamicData;
using wxRobotApp.Common;
using wxRobotApp.Utils;


namespace wxRobotApp.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public WeChatProcessInfo[] WeChatProcessInfos { get; set; } = [];
    static public string Hello { get; set; } = "Hello, World!";

    public MainWindowViewModel()
    {
        
        WeChatProcessInfos.AddRange(WeChatProcesses.ScanWeChatProcesses());
    }

}

