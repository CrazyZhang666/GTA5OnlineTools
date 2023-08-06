using GTA5OnlineTools.Data;

namespace GTA5OnlineTools.Utils;

public static class CoreUtil
{
    /// <summary>
    /// 主視窗標題
    /// </summary>
    public const string MainAppWindowName = "GTA5線上小助手 支援1.67 完全免費 v";

    /// <summary>
    /// 程式服務端版本號，如：1.2.3.4
    /// </summary>
    public static Version ServerVersion = Version.Parse("0.0.0.0");

    /// <summary>
    /// 程式客戶端版本號，如：1.2.3.4
    /// </summary>
    public static readonly Version ClientVersion = Application.ResourceAssembly.GetName().Version;

    /// <summary>
    /// 程式客戶端最後編譯時間
    /// </summary>
    public static readonly DateTime BuildDate = File.GetLastWriteTime(Environment.ProcessPath);

    /// <summary>
    /// 檢查更新相關資訊
    /// </summary>
    public static UpdateInfo UpdateInfo { get; set; }

    /// <summary>
    /// 正在更新時的檔名
    /// </summary>
    public const string HalfwayAppName = "未下載完的小助手更新檔案.exe";

    /// <summary>
    /// 固定下載更新地址
    /// </summary>
    public static string UpdateAddress = "https://github.com/CrazyZhang666/GTA5/releases/download/update/GTA5OnlineTools.exe";

    /// <summary>
    /// 更新完成後的完整檔名
    /// </summary>
    /// <returns></returns>
    public static string FullAppName()
    {
        return $"{MainAppWindowName}{ServerVersion}.exe";
    }

    /// <summary>
    /// 計算時間差，即軟體執行時間
    /// </summary>
    public static string ExecDateDiff(DateTime dateBegin, DateTime dateEnd)
    {
        var ts1 = new TimeSpan(dateBegin.Ticks);
        var ts2 = new TimeSpan(dateEnd.Ticks);

        return ts1.Subtract(ts2).Duration().ToString("c")[..8];
    }

    /// <summary>
    /// 獲取管理員狀態
    /// </summary>
    /// <returns></returns>
    public static string GetAdminState()
    {
        return IsAdministrator() ? "管理員" : "普通";
    }

    /// <summary>
    /// 判斷是否管理員許可權執行
    /// </summary>
    /// <returns></returns>
    public static bool IsAdministrator()
    {
        var current = WindowsIdentity.GetCurrent();
        var windowsPrincipal = new WindowsPrincipal(current);

        // WindowsBuiltInRole可以列舉出很多許可權，例如系統使用者、User、Guest等等
        return windowsPrincipal.IsInRole(WindowsBuiltInRole.Administrator);
    }

    /// <summary>
    /// 檔案大小轉換
    /// </summary>
    /// <param name="size"></param>
    /// <returns></returns>
    public static string GetFileForamtSize(long size)
    {
        var kb = size / 1024.0f;

        if (kb > 1024.0f)
            return $"{kb / 1024.0f:0.00}MB";
        else
            return $"{kb:0.00}KB";
    }

    /// <summary>
    /// 獲取未下載完臨時檔案路徑
    /// </summary>
    /// <returns></returns>
    public static string GetHalfwayFilePath()
    {
        return FileUtil.GetCurrFullPath(HalfwayAppName);
    }

    /// <summary>
    /// 獲取已下載完真實檔案路徑
    /// </summary>
    /// <returns></returns>
    public static string GetFullFilePath()
    {
        return FileUtil.GetCurrFullPath(FullAppName());
    }
}
