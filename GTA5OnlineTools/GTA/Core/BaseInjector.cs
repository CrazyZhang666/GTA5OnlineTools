namespace GTA5OnlineTools.GTA.Core;

public static class BaseInjector
{
    [Flags]
    public enum ProcessAccessFlags : uint
    {
        All = 0x001F0FFF,
        Terminate = 0x00000001,
        CreateThread = 0x00000002,
        VirtualMemoryOperation = 0x00000008,
        VirtualMemoryRead = 0x00000010,
        VirtualMemoryWrite = 0x00000020,
        DuplicateHandle = 0x00000040,
        CreateProcess = 0x000000080,
        SetQuota = 0x00000100,
        SetInformation = 0x00000200,
        QueryInformation = 0x00000400,
        QueryLimitedInformation = 0x00001000,
        Synchronize = 0x00100000
    }

    [Flags]
    public enum AllocationType
    {
        Commit = 0x1000,
        Reserve = 0x2000,
        Decommit = 0x4000,
        Release = 0x8000,
        Reset = 0x80000,
        Physical = 0x400000,
        TopDown = 0x100000,
        WriteWatch = 0x200000,
        LargePages = 0x20000000
    }

    [Flags]
    public enum MemoryProtection
    {
        Execute = 0x10,
        ExecuteRead = 0x20,
        ExecuteReadWrite = 0x40,
        ExecuteWriteCopy = 0x80,
        NoAccess = 0x01,
        ReadOnly = 0x02,
        ReadWrite = 0x04,
        WriteCopy = 0x08,
        GuardModifierflag = 0x100,
        NoCacheModifierflag = 0x200,
        WriteCombineModifierflag = 0x400
    }

    /////////////////////////////////////////////////////////////////////

    [DllImport("kernel32.dll")]
    public static extern IntPtr OpenProcess(ProcessAccessFlags processAccess, bool bInheritHandle, int processId);

    [DllImport("kernel32.dll")]
    public static extern bool CloseHandle(IntPtr hObject);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr GetModuleHandle(string lpModuleName);

    [DllImport("kernel32", CharSet = CharSet.Ansi)]
    public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

    [DllImport("kernel32.dll", ExactSpelling = true)]
    public static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, int dwSize, AllocationType dwFreeType);

    [DllImport("kernel32.dll", ExactSpelling = true)]
    public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, IntPtr dwSize, AllocationType flAllocationType, MemoryProtection flProtect);

    [DllImport("kernel32.dll")]
    public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] lpBuffer, int dwSize, out IntPtr lpNumberOfBytesWritten);

    [DllImport("kernel32.dll")]
    public static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

    [DllImport("user32.dll")]
    public static extern int SetForegroundWindow(IntPtr hwnd);

    /// <summary>
    /// 根据进程id注入dll
    /// </summary>
    /// <param name="procId">目标进程Id</param>
    /// <param name="dllPath">DLL路径</param>
    public static void DLLInjector(int procId, string dllPath)
    {
        IntPtr size = (IntPtr)dllPath.Length;

        IntPtr procHandle = OpenProcess(ProcessAccessFlags.All, false, procId);
        IntPtr dllSpace = VirtualAllocEx(procHandle, IntPtr.Zero, size, AllocationType.Reserve | AllocationType.Commit, MemoryProtection.ExecuteReadWrite);

        byte[] bytes = Encoding.ASCII.GetBytes(dllPath);
        WriteProcessMemory(procHandle, dllSpace, bytes, bytes.Length, out _);

        IntPtr kernel32Handle = GetModuleHandle("Kernel32.dll");
        IntPtr loadLibraryAAddress = GetProcAddress(kernel32Handle, "LoadLibraryA");

        IntPtr remoteThreadHandle = CreateRemoteThread(procHandle, IntPtr.Zero, 0, loadLibraryAAddress, dllSpace, 0, IntPtr.Zero);

        CloseHandle(remoteThreadHandle);
        CloseHandle(procHandle);
    }
}

public class ProcessList
{
    public int ProcID { get; set; }
    public string ProcName { get; set; }
    public string MainWindowTitle { get; set; }
    public IntPtr MainWindowHandle { get; set; }
}

public class InjectInfo
{
    public int ProcID { get; set; }
    public string DLLPath { get; set; }
    public string ProcName { get; set; }
    public IntPtr MainWindowHandle { get; set; }
}
