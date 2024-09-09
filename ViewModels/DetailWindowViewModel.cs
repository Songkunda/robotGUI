using System;
using wxRobotApp.Common;

namespace wxRobotApp.ViewModels;

public class DetailWindowViewModel : ViewModelBase
{
    public WeChatProcessInfo SelectedProcessInfo { get; set; }
    public DetailWindowViewModel(WeChatProcessInfo selectedProcessInfo)
    {
        SelectedProcessInfo = selectedProcessInfo;
    }
   
}

