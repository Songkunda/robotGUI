using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WXRobotApp.Common;

namespace WXRobotApp.Utils
{
    public class WeChatProcesses
    {
        static public List<WeChatProcessInfo> ScanWeChatProcesses()
        {
            var reWeChatProcessInfos = new List<WeChatProcessInfo>();
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

                reWeChatProcessInfos.Add(wechatInfo);
            }
            return reWeChatProcessInfos;

        }
        static private string GetWeChatVersion(string? wechatPath)
        {
            if (wechatPath == null) return "Unknown";
            return FileVersionInfo.GetVersionInfo(wechatPath).FileVersion ?? "Unknown";
        }

        static private string CheckHelperDll(Process process)
        {
            foreach (ProcessModule module in process.Modules)
            {
                if (module.ModuleName.Equals("helper.dll", StringComparison.OrdinalIgnoreCase))
                {
                    return FileVersionInfo.GetVersionInfo(module.FileName).FileVersion ?? "Unknown";
                }
            }
            return "Not Injected";
        }
        static private string[] GetProcessModules(Process process)
        {
            string[] modules = [];
            foreach (ProcessModule module in process.Modules)
            {
                modules.Append(module.ModuleName);
            }

            return modules;
        }

        static private string GetWeChatId(Process process)
        {
            return "UnknownWeChatId";
        }

        static private int GetMessagePort(Process process)
        {
            return 0;
        }
        //   private void InitializeTimer()
        // {
        //     _scanTimer.Interval = 5000;
        //     _scanTimer.Elapsed += (sender, e) => ScanWeChatProcesses();
        //     _scanTimer.Start();
        // }
    }
}