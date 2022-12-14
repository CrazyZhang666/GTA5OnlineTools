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

                if (item.MainModule.FileVersionInfo.LegalCopyright == "Rockstar Games Inc. (C) 2005-2022 Take Two Interactive. All rights reserved.")
                    return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 打开指定路径或链接（带异常提示）
    /// </summary>
    /// <param name="path">本地文件夹路径</param>
    public static void OpenPath(string path)
    {
        try
        {
            Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
        }
        catch (Exception ex)
        {
            NotifierHelper.ShowException(ex);
        }
    }

    /// <summary>
    /// 使用Notepad2编辑文本文件
    /// </summary>
    /// <param name="path"></param>
    public static void Notepad2EditTextFile(string path)
    {
        try
        {
            Process.Start(new ProcessStartInfo(FileUtil.D_Cache_Path + "Notepad2.exe", path) { UseShellExecute = true });
        }
        catch (Exception ex)
        {
            NotifierHelper.ShowException(ex);
        }
    }

    /// <summary>
    /// 以管理员权限打开指定程序，不需要后缀.exe
    /// </summary>
    /// <param name="processName">程序名字，要带后缀名</param>
    /// <param name="isKiddion">是否在Kiddion目录下</param>
    public static void OpenProcess(string processName, bool isKiddion)
    {
        try
        {
            if (IsAppRun(processName))
            {
                NotifierHelper.Show(NotifierType.Warning, $"请不要重复打开，{processName} 已经在运行了");
            }
            else
            {
                string path = string.Empty;
                if (isKiddion)
                    path = FileUtil.D_Kiddion_Path;
                else
                    path = FileUtil.D_Cache_Path;

                Directory.SetCurrentDirectory(path);
                path = Path.Combine(path, processName + ".exe");
                Process.Start(new ProcessStartInfo(path)
                {
                    UseShellExecute = true,
                    Verb = "runas"
                });
            }
        }
        catch (Exception ex)
        {
            NotifierHelper.ShowException(ex);
        }
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
