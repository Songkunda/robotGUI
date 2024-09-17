using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

class MutexApp
{
    static void OpenMutexAppTask()
    {
#if WINDOWS
        // call 
        int pid = Process.GetProcessesByName("WeChat")[0].Id;
        string targetMutexName = "_WeChat_App_Instance_Identity_Mutex_Name";
        WindowsHandleChecker.FindHandles(pid, targetMutexName);
#elif MACOS




 IntPtr versionPtr = WechatInstanceVersion();
        string version = Marshal.PtrToStringAnsi(versionPtr);
        Console.WriteLine($"WechatInstance version: {version}");
        // call
        if (HasWechatInstance())
        {
            Console.WriteLine("WeChat instance found on macOS.");
        }
        else
        {
            Console.WriteLine("No WeChat instance found on macOS.");
        }
#endif
    }

#if MACOS
    
     [DllImport("Libs/darwin-x64/libWechatInstance.dylib")]
    private static extern bool HasWechatInstance();

    [DllImport("Libs/darwin-x64/libWechatInstance.dylib")]
    private static extern IntPtr WechatInstanceVersion();

   
#endif
}
