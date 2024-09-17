using System;
using WXRobotApp.Common;

namespace WXRobotApp.ViewModels;

public class DetailWindowViewModel : ViewModelBase
{
    public WeChatProcessInfo SelectedProcessInfo { get; set; }
    public DetailWindowViewModel(WeChatProcessInfo selectedProcessInfo)
    {
        SelectedProcessInfo = selectedProcessInfo;
    }
   
}

