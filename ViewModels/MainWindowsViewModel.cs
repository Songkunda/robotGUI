using System.Collections.ObjectModel;
using wxRobotApp.Common;
using wxRobotApp.Utils;


namespace wxRobotApp.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<WeChatProcessInfo> WeChatProcessInfos { get; set; } = [];
    static public string Hello { get; set; } = "Hello, World!";

    public MainWindowViewModel()
    {

        foreach (var process in WeChatProcesses.ScanWeChatProcesses())
        {
            WeChatProcessInfos.Add(process);  // This works with ObservableCollection
        }
    }

}

