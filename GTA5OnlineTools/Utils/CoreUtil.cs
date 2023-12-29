using GTA5OnlineTools.Data;

namespace GTA5OnlineTools.Utils;

public static class CoreUtil
{
    /// <summary>
    /// 主窗口标题
    /// </summary>
    public const string MainAppWindowName = "GTA5线上小助手 支持1.68 完全免费 v";

    /// <summary>
    /// 程序服务端版本号，如：1.2.3.4
    /// </summary>
    public static Version ServerVersion = Version.Parse("0.0.0.0");

    /// <summary>
    /// 程序客户端版本号，如：1.2.3.4
    /// </summary>
    public static readonly Version ClientVersion = Application.ResourceAssembly.GetName().Version;

    /// <summary>
    /// 程序客户端最后编译时间
    /// </summary>
    public static readonly DateTime BuildDate = File.GetLastWriteTime(Environment.ProcessPath);

    /// <summary>
    /// 检查更新相关信息
    /// </summary>
    public static UpdateInfo UpdateInfo { get; set; }

    /// <summary>
    /// 正在更新时的文件名
    /// </summary>
    public const string HalfwayAppName = "未下载完的小助手更新文件.exe";

    /// <summary>
    /// 固定下载更新地址
    /// </summary>
    public static string UpdateAddress = "https://github.com/CrazyZhang666/GTA5/releases/download/update/GTA5OnlineTools.exe";

    /// <summary>
    /// 更新完成后的完整文件名
    /// </summary>
    /// <returns></returns>
    public static string FullAppName()
    {
        return $"{MainAppWindowName}{ServerVersion}.exe";
    }

    /// <summary>
    /// 计算时间差，即软件运行时间
    /// </summary>
    public static string ExecDateDiff(DateTime dateBegin, DateTime dateEnd)
    {
        var ts1 = new TimeSpan(dateBegin.Ticks);
        var ts2 = new TimeSpan(dateEnd.Ticks);

        return ts1.Subtract(ts2).Duration().ToString("c")[..8];
    }

    /// <summary>
    /// 获取管理员状态
    /// </summary>
    /// <returns></returns>
    public static string GetAdminState()
    {
        return IsAdministrator() ? "管理员" : "普通";
    }

    /// <summary>
    /// 判断是否管理员权限运行
    /// </summary>
    /// <returns></returns>
    public static bool IsAdministrator()
    {
        var current = WindowsIdentity.GetCurrent();
        var windowsPrincipal = new WindowsPrincipal(current);

        // WindowsBuiltInRole可以枚举出很多权限，例如系统用户、User、Guest等等
        return windowsPrincipal.IsInRole(WindowsBuiltInRole.Administrator);
    }

    /// <summary>
    /// 文件大小转换
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
    /// 获取未下载完临时文件路径
    /// </summary>
    /// <returns></returns>
    public static string GetHalfwayFilePath()
    {
        return FileUtil.GetCurrFullPath(HalfwayAppName);
    }

    /// <summary>
    /// 获取已下载完真实文件路径
    /// </summary>
    /// <returns></returns>
    public static string GetFullFilePath()
    {
        return FileUtil.GetCurrFullPath(FullAppName());
    }
}
