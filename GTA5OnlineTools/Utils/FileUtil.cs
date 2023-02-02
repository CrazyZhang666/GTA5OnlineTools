namespace GTA5OnlineTools.Utils;

public static class FileUtil
{
    /// <summary>
    /// 默认路径
    /// </summary>
    public const string AppData = "C:\\ProgramData\\GTA5OnlineTools";
    /// <summary>
    /// 资源路径
    /// </summary>
    public const string ResFiles = "GTA5OnlineTools.GTA.Files";

    public const string Dir_Kiddion = $"{AppData}\\Kiddion";
    public const string Dir_Cache = $"{AppData}\\Cache";
    public const string Dir_Config = $"{AppData}\\Config";
    public const string Dir_Inject = $"{AppData}\\Inject";
    public const string Dir_Log = $"{AppData}\\Log";

    public const string Dir_Kiddion_Scripts = $"{Dir_Kiddion}\\scripts";

    public const string Res_Other_SGTA50000 = $"{ResFiles}.Other.SGTA50000";

    public const string Res_Kiddion_Kiddion = $"{ResFiles}.Kiddion.Kiddion.exe";
    public const string File_Kiddion_Kiddion = $"{Dir_Kiddion}\\Kiddion.exe";

    public const string Res_Kiddion_KiddionChs = $"{ResFiles}.Kiddion.KiddionChs.dll";
    public const string File_Kiddion_KiddionChs = $"{Dir_Kiddion}\\KiddionChs.dll";

    public const string Res_Kiddion_Config = $"{ResFiles}.Kiddion.config.json";
    public const string File_Kiddion_Config = $"{Dir_Kiddion}\\config.json";

    public const string Res_Kiddion_Key87_Config = $"{ResFiles}.Kiddion.key87.config.json";

    public const string Res_Kiddion_Themes = $"{ResFiles}.Kiddion.themes.json";
    public const string File_Kiddion_Themes = $"{Dir_Kiddion}\\themes.json";

    public const string Res_Kiddion_Teleports = $"{ResFiles}.Kiddion.teleports.json";
    public const string File_Kiddion_Teleports = $"{Dir_Kiddion}\\teleports.json";

    public const string Res_Kiddion_Vehicles = $"{ResFiles}.Kiddion.vehicles.json";
    public const string File_Kiddion_Vehicles = $"{Dir_Kiddion}\\vehicles.json";

    public const string Res_Kiddion_Scripts_Readme = $"{ResFiles}.Kiddion.scripts.Readme.api";
    public const string File_Kiddion_Scripts_Readme = $"{Dir_Kiddion_Scripts}\\Readme.api";

    public const string Res_Cache_BincoHax = $"{ResFiles}.Cache.BincoHax.exe";
    public const string File_Cache_BincoHax = $"{Dir_Cache}\\BincoHax.exe";

    public const string Res_Cache_GTAHax = $"{ResFiles}.Cache.GTAHax.exe";
    public const string File_Cache_GTAHax = $"{Dir_Cache}\\GTAHax.exe";

    public const string Res_Cache_LSCHax = $"{ResFiles}.Cache.LSCHax.exe";
    public const string File_Cache_LSCHax = $"{Dir_Cache}\\LSCHax.exe";

    public const string Res_Cache_Notepad2 = $"{ResFiles}.Cache.Notepad2.exe";
    public const string File_Cache_Notepad2 = $"{Dir_Cache}\\Notepad2.exe";

    public const string Res_Cache_Stat = $"{ResFiles}.Cache.stat.txt";
    public const string File_Cache_Stat = $"{Dir_Cache}\\stat.txt";

    public const string Res_Inject_YimMenu = $"{ResFiles}.Inject.YimMenu.dll";
    public const string File_Inject_YimMenu = $"{Dir_Inject}\\YimMenu.dll";

    public const string File_Config_Option = $"{Dir_Config}\\OptionConfig.json";
    public const string File_Config_SelfState = $"{Dir_Config}\\SelfStateConfig.json";
    public const string File_Config_CustomTPList = $"{Dir_Config}\\CustomTPList.json";

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
        var path = Dir_Log + @"\Crash";
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
