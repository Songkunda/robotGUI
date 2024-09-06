using System;
using System.Diagnostics;
using System.Linq;
using wxRobotApp.Common;

namespace wxRobotApp.Utils
{
    public class WeChatProcesses
    {
        void ScanWeChatProcesses()
        {
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

                // Dispatcher.UIThread.Invoke(() => WeChatProcesses.Add(wechatInfo));
            }

        }
        private string GetWeChatVersion(string? wechatPath)
        {
            if (wechatPath == null) return "Unknown";
            return FileVersionInfo.GetVersionInfo(wechatPath).FileVersion ?? "Unknown";
        }

        private string CheckHelperDll(Process process)
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
        private string[] GetProcessModules(Process process)
        {
            string[] modules = [];
            foreach (ProcessModule module in process.Modules)
            {
                modules.Append(module.ModuleName);
            }

            return modules;
        }

        private string GetWeChatId(Process process)
        {
            return "UnknownWeChatId";
        }

        private int GetMessagePort(Process process)
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