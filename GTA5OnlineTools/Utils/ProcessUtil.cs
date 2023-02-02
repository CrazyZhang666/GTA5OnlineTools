using GTA5OnlineTools.Helper;

namespace GTA5OnlineTools.Utils;

public static class ProcessUtil
{
    /// <summary>
    /// 判断程序是否运行
    /// </summary>
    /// <param name="appName">程序名称</param>
    /// <returns>正在运行返回true，未运行返回false</returns>
    public static bool IsAppRun(string appName)
    {
        return Process.GetProcessesByName(appName).Length > 0;
    }

    /// <summary>
    /// 判断GTA5程序是否运行
    /// </summary>
    /// <returns>正在运行返回true，未运行返回false</returns>
    public static bool IsGTA5Run()
    {
        var pArray = Process.GetProcessesByName(CoreUtil.TargetAppName);
        if (pArray.Length > 0)
        {
            foreach (var item in pArray)
            {
                if (item.MainWindowHandle == IntPtr.Zero)
                    continue;

                if (item.MainModule.FileVersionInfo.LegalCopyright == "Rockstar Games Inc. (C) 2005-2023 Take Two Interactive. All rights reserved.")
                    return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 打开进程基类
    /// </summary>
    /// <param name="path"></param>
    /// <param name="args"></param>
    public static void OpenBase(string path, string args = "")
    {
        Process.Start(path, args);
    }

    /// <summary>
    /// 打开链接或者文件夹路径
    /// </summary>
    /// <param name="url"></param>
    public static void OpenLink(string url)
    {
        Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
    }

    /// <summary>
    /// 打开指定路径或链接
    /// </summary>
    /// <param name="path">本地文件夹路径</param>
    public static void OpenPath(string path, string args = "")
    {
        try
        {
            OpenBase(path, args);
        }
        catch (Exception ex)
        {
            NotifierHelper.ShowException(ex);
        }
    }

    /// <summary>
    /// 使用Notepad2编辑文本文件
    /// </summary>
    /// <param name="args"></param>
    public static void Notepad2EditTextFile(string args)
    {
        OpenPath(FileUtil.File_Cache_Notepad2, args);
    }

    /// <summary>
    /// 以管理员权限打开指定程序
    /// </summary>
    /// <param name="processPath">程序路径</param>
    public static void OpenProcess(string processPath)
    {
        Directory.SetCurrentDirectory(Path.GetDirectoryName(processPath));
        OpenPath(processPath);
    }

    /// <summary>
    /// 根据进程名字关闭指定程序
    /// </summary>
    /// <param name="processName">程序名字，不需要加.exe</param>
    public static void CloseProcess(string processName)
    {
        var appProcess = Process.GetProcessesByName(processName);
        foreach (var targetPro in appProcess)
        {
            targetPro.Kill();
        }
    }

    /// <summary>
    /// 关闭全部第三方exe进程
    /// </summary> 
    public static void CloseThirdProcess()
    {
        CloseProcess("Kiddion");
        CloseProcess("GTAHax");
        CloseProcess("BincoHax");
        CloseProcess("LSCHax");
        CloseProcess("Notepad2");
    }
}
