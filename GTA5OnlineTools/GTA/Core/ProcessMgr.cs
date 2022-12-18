namespace GTA5OnlineTools.GTA.Core;

public static class ProcessMgr
{
    [Flags]
    public enum ProcessAccess : uint
    {
        Terminate = 0x1,
        CreateThread = 0x2,
        SetSessionId = 0x4,
        VmOperation = 0x8,
        VmRead = 0x10,
        VmWrite = 0x20,
        DupHandle = 0x40,
        CreateProcess = 0x80,
        SetQuota = 0x100,
        SetInformation = 0x200,
        QueryInformation = 0x400,
        SetPort = 0x800,
        SuspendResume = 0x800,
        QueryLimitedInformation = 0x1000,
        Synchronize = 0x100000
    }

    [DllImport("ntdll.dll")]
    private static extern uint NtResumeProcess([In] IntPtr processHandle);

    [DllImport("ntdll.dll")]
    private static extern uint NtSuspendProcess([In] IntPtr processHandle);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr OpenProcess(ProcessAccess desiredAccess, bool inheritHandle, int processId);

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool CloseHandle([In] IntPtr handle);

    /// <summary>
    /// 暂停进程
    /// </summary>
    public static void SuspendProcess(int processId)
    {
        IntPtr hProc = IntPtr.Zero;
        try
        {
            hProc = OpenProcess(ProcessAccess.SuspendResume, false, processId);
            if (hProc != IntPtr.Zero)
                NtSuspendProcess(hProc);
        }
        finally
        {
            if (hProc != IntPtr.Zero)
                CloseHandle(hProc);
        }
    }

    /// <summary>
    /// 恢复进程
    /// </summary>
    public static void ResumeProcess(int processId)
    {
        IntPtr hProc = IntPtr.Zero;
        try
        {
            hProc = OpenProcess(ProcessAccess.SuspendResume, false, processId);
            if (hProc != IntPtr.Zero)
                NtResumeProcess(hProc);
        }
        finally
        {
            if (hProc != IntPtr.Zero)
                CloseHandle(hProc);
        }
    }
}
