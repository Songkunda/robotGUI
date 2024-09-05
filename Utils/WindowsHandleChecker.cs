using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

public class WindowsHandleChecker
{
    // 导入用于句柄枚举的Windows API
    [DllImport("ntdll.dll")]
    private static extern int NtQuerySystemInformation(int SystemInformationClass, IntPtr SystemInformation, int SystemInformationLength, ref int ReturnLength);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr OpenProcess(uint processAccess, bool bInheritHandle, int processId);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool CloseHandle(IntPtr hObject);

    [DllImport("ntdll.dll")]
    private static extern int NtQueryObject(IntPtr Handle, int ObjectInformationClass, IntPtr ObjectInformation, int ObjectInformationLength, ref int ReturnLength);

    private const int SystemHandleInformation = 16;
    private const int ObjectNameInformation = 1;
    private const uint PROCESS_DUP_HANDLE = 0x0040;

    public static void FindHandles(int pid, string targetMutexName)
    {
        int handleInfoSize = 0x10000;
        IntPtr handleInfoPtr = Marshal.AllocHGlobal(handleInfoSize);
        int returnLength = 0;

        while (NtQuerySystemInformation(SystemHandleInformation, handleInfoPtr, handleInfoSize, ref returnLength) == -1)
        {
            handleInfoSize *= 2;
            handleInfoPtr = Marshal.ReAllocHGlobal(handleInfoPtr, (IntPtr)handleInfoSize);
        }

        int handleCount = Marshal.ReadInt32(handleInfoPtr);
        IntPtr handleEntryPtr = handleInfoPtr + 4; // 指向第一个句柄条目

        for (int i = 0; i < handleCount; i++)
        {
            var handleInfo = Marshal.PtrToStructure<SystemHandleInformation>(handleEntryPtr);

            if (handleInfo.ProcessId == pid)
            {
                IntPtr processHandle = OpenProcess(PROCESS_DUP_HANDLE, false, pid);

                if (processHandle != IntPtr.Zero)
                {
                    IntPtr duplicatedHandle = IntPtr.Zero;

                    if (DuplicateHandle(processHandle, new IntPtr(handleInfo.Handle), IntPtr.Zero, out duplicatedHandle, 0, false, 2))
                    {
                        string name = GetObjectName(duplicatedHandle);

                        if (!string.IsNullOrEmpty(name) && name.Contains(targetMutexName))
                        {
                            Console.WriteLine($"找到Mutex: {name}");
                        }

                        CloseHandle(duplicatedHandle);
                    }

                    CloseHandle(processHandle);
                }
            }

            handleEntryPtr += Marshal.SizeOf(typeof(SystemHandleInformation)); // 移动到下一个句柄条目
        }

        Marshal.FreeHGlobal(handleInfoPtr);
    }

    private static string GetObjectName(IntPtr handle)
    {
        int nameInfoSize = 0x1000;
        IntPtr nameInfoPtr = Marshal.AllocHGlobal(nameInfoSize);
        int returnLength = 0;

        NtQueryObject(handle, ObjectNameInformation, nameInfoPtr, nameInfoSize, ref returnLength);

        string name = Marshal.PtrToStringUni(nameInfoPtr + 4); // 对象名称从4字节后开始
        Marshal.FreeHGlobal(nameInfoPtr);

        return name;
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool DuplicateHandle(IntPtr hSourceProcessHandle, IntPtr hSourceHandle, IntPtr hTargetProcess, out IntPtr lpTargetHandle, uint dwDesiredAccess, bool bInheritHandle, uint dwOptions);

    [StructLayout(LayoutKind.Sequential)]
    private struct SystemHandleInformation
    {
        public int ProcessId;
        public byte ObjectTypeNumber;
        public byte Flags;
        public ushort Handle;
        public IntPtr Object;
        public uint GrantedAccess;
    }
}
