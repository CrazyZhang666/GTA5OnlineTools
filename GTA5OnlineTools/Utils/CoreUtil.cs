using GTA5OnlineTools.Data;
using GTA5OnlineTools.GTA.Core;

namespace GTA5OnlineTools.Utils;

public static class CoreUtil
{
    /// <summary>
    /// 主窗口标题
    /// </summary>
    public const string MainAppWindowName = "GTA5线上小助手 支持1.66 完全免费 v";

    /// <summary>
    /// 目标进程，默认为GTA5
    /// </summary>
    public const string TargetAppName = "GTA5";

    /// <summary>
    /// 程序服务端版本号，如：1.2.3.4
    /// </summary>
    public static Version ServerVersion = Version.Parse("0.0.0.0");

    /// <summary>
    /// 程序客户端版本号，如：1.2.3.4
    /// </summary>
    public static Version ClientVersion = Application.ResourceAssembly.GetName().Version;

    /// <summary>
    /// 程序客户端最后编译时间
    /// </summary>
    public static DateTime BuildTime = File.GetLastWriteTime(Environment.ProcessPath);

    /// <summary>
    /// 检查更新相关信息
    /// </summary>
    public static UpdateInfo UpdateInfo { get; set; } = null;

    /// <summary>
    /// 正在更新时的文件名
    /// </summary>
    public const string HalfwayAppName = "未下载完的小助手更新文件.exe";

    /// <summary>
    /// 固定下载更新地址
    /// </summary>
    public static string UpdateAddress = "https://github.com/CrazyZhang666/GTA5OnlineTools/releases/download/update/GTA5OnlineTools.exe";

    /// <summary>
    /// 更新完成后的文件名
    /// </summary>
    /// <returns></returns>
    public static string FinalAppName()
    {
        return MainAppWindowName + ServerVersion + ".exe";
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
    /// 刷新DNS缓存
    /// </summary>
    public static void FlushDNSCache()
    {
        Win32.DnsFlushResolverCache();
    }
}
