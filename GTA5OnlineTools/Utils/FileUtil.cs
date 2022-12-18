namespace GTA5OnlineTools.Utils;

public static class FileUtil
{
    /// <summary>
    /// 默认路径
    /// </summary>
    public const string Default_Path = @"C:\ProgramData\GTA5OnlineTools\";

    public const string Resource_Path = "GTA5OnlineTools.GTA.Files.";
    public const string Resource_Kiddion_Path = "GTA5OnlineTools.GTA.Files.Kiddion.";
    public const string Resource_Inject_Path = "GTA5OnlineTools.GTA.Files.Inject.";

    public static string D_Kiddion_Path = Default_Path + @"Kiddion\";
    public static string D_Cache_Path = Default_Path + @"Cache\";
    public static string D_Config_Path = Default_Path + @"Config\";
    public static string D_Inject_Path = Default_Path + @"Inject\";
    public static string D_Log_Path = Default_Path + @"Log\";

    public static string D_KiddionScripts_Path = D_Kiddion_Path + @"scripts\";

    public static string F_GTAHaxStat_Path = D_Cache_Path + "stat.txt";

    public static string F_OptionConfig_Path = D_Config_Path + "OptionConfig.json";
    public static string F_SelfStateConfig_Path = D_Config_Path + "SelfStateConfig.json";

    public static string F_CustomTPList_Path = D_Config_Path + "CustomTPList.json";

    /// <summary>
    /// 获取当前运行文件完整路径
    /// </summary>
    public static string Current_Path = Process.GetCurrentProcess().MainModule.FileName;

    /// <summary>
    /// 获取当前文件目录，不加文件名及后缀
    /// </summary>
    public static string CurrentDirectory_Path = AppDomain.CurrentDomain.BaseDirectory;

    /// <summary>
    /// 我的文档完整路径
    /// </summary>
    public static string MyDocuments_Path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

    /// <summary>
    /// AppData完整路径
    /// </summary>
    public static string AppData_Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

    /// <summary>
    /// YimMenu路径
    /// </summary>
    public static string YimMenu_Path = Path.Combine(AppData_Path, "BigBaseV2");

    /// <summary>
    /// 文件重命名
    /// </summary>
    public static void FileReName(string OldPath, string NewPath)
    {
        var ReName = new FileInfo(OldPath);
        ReName.MoveTo(NewPath);
    }

    /// <summary>
    /// 给文件名，得出当前目录完整路径，AppName带文件名后缀
    /// </summary>
    public static string GetCurrFullPath(string AppName)
    {
        return Path.Combine(CurrentDirectory_Path, AppName);
    }

    /// <summary>
    /// 保存崩溃日志
    /// </summary>
    /// <param name="log">日志内容</param>
    public static void SaveCrashLog(string log)
    {
        var path = D_Log_Path + @"\Crash";
        Directory.CreateDirectory(path);
        path += $@"\#Crash#{DateTime.Now:yyyyMMdd_HH-mm-ss_ffff}.log";
        File.WriteAllText(path, log);
    }

    /// <summary>
    /// 从资源文件中抽取资源文件
    /// </summary>
    /// <param name="resFileName">资源文件名称（资源文件名称必须包含目录，目录间用“.”隔开,最外层是项目默认命名空间）</param>
    /// <param name="outputFile">输出文件</param>
    public static void ExtractResFile(string resFileName, string outputFile)
    {
        BufferedStream inStream = null;
        FileStream outStream = null;
        try
        {
            var asm = Assembly.GetExecutingAssembly();
            inStream = new BufferedStream(asm.GetManifestResourceStream(resFileName));
            outStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write);

            var buffer = new byte[1024];
            int length;

            while ((length = inStream.Read(buffer, 0, buffer.Length)) > 0)
                outStream.Write(buffer, 0, length);

            outStream.Flush();
        }
        finally
        {
            outStream?.Close();
            inStream?.Close();
        }
    }

    /// <summary>
    /// 判断文件是否被占用
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static bool IsOccupied(string filePath)
    {
        FileStream stream = null;
        try
        {
            stream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            return false;
        }
        catch
        {
            return true;
        }
        finally
        {
            stream?.Close();
        }
    }

    /// <summary>
    /// 清空指定文件夹下的文件及文件夹
    /// </summary>
    /// <param name="srcPath">文件夹路径</param>
    public static void DelectDir(string srcPath)
    {
        try
        {
            var dir = new DirectoryInfo(srcPath);
            var fileinfo = dir.GetFileSystemInfos();
            foreach (var file in fileinfo)
            {
                if (file is DirectoryInfo)
                {
                    var subdir = new DirectoryInfo(file.FullName);
                    subdir.Delete(true);
                }
                else
                {
                    File.Delete(file.FullName);
                }
            }
        }
        catch { }
    }
}
