namespace GTA5Shared.Helper;

public static class FileHelper
{
    public const string ResFiles = "GTA5Shared.Files";

    public const string Res_Kiddion_Kiddion = $"{ResFiles}.Kiddion.Kiddion.exe";
    public const string Res_Kiddion_KiddionChs = $"{ResFiles}.Kiddion.KiddionChs.dll";
    public const string Res_Kiddion_Config = $"{ResFiles}.Kiddion.config.json";
    public const string Res_Kiddion_Config87 = $"{ResFiles}.Kiddion.config87.json";
    public const string Res_Kiddion_Themes = $"{ResFiles}.Kiddion.themes.json";
    public const string Res_Kiddion_Teleports = $"{ResFiles}.Kiddion.teleports.json";
    public const string Res_Kiddion_Vehicles = $"{ResFiles}.Kiddion.vehicles.json";

    public const string Res_Kiddion_Scripts_Readme = $"{ResFiles}.Kiddion.scripts.Readme.api";

    public const string Res_Cache_BincoHax = $"{ResFiles}.Cache.BincoHax.exe";
    public const string Res_Cache_GTAHax = $"{ResFiles}.Cache.GTAHax.exe";
    public const string Res_Cache_LSCHax = $"{ResFiles}.Cache.LSCHax.exe";
    public const string Res_Cache_Stat = $"{ResFiles}.Cache.stat.txt";
    public const string Res_Cache_Notepad2 = $"{ResFiles}.Cache.Notepad2.exe";
    public const string Res_Cache_Xenos64Profile = $"{ResFiles}.Cache.XenosCurrentProfile.xpr";
    public const string Res_Cache_Xenos64 = $"{ResFiles}.Cache.Xenos64.exe";

    public const string Res_YimMenu_YimMenu = $"{ResFiles}.YimMenu.YimMenu.dll";
    public const string Res_YimMenu_Index = $"{ResFiles}.YimMenu.index.json";
    public const string Res_YimMenu_ZHCN = $"{ResFiles}.YimMenu.zh_CN.json";

    public const string Res_Other_SGTA50000 = $"{ResFiles}.Other.SGTA50000";

    //////////////////////////////////////////////////////////////////

    public static string Dir_MyDocuments { get; private set; }

    public static string Dir_Base { get; private set; }

    public static string Dir_Kiddion { get; private set; }
    public static string Dir_YimMenu { get; private set; }
    public static string Dir_Cache { get; private set; }
    public static string Dir_Config { get; private set; }
    public static string Dir_Logger { get; private set; }

    public static string Dir_Kiddion_Scripts { get; private set; }

    public static string Dir_Log_Crash { get; private set; }
    public static string Dir_Log_NLog { get; private set; }

    public static string File_Kiddion_Kiddion { get; private set; }
    public static string File_Kiddion_KiddionChs { get; private set; }
    public static string File_Kiddion_Config { get; private set; }
    public static string File_Kiddion_Themes { get; private set; }
    public static string File_Kiddion_Teleports { get; private set; }
    public static string File_Kiddion_Vehicles { get; private set; }

    public static string File_Kiddion_Scripts_Readme { get; private set; }

    public static string File_Cache_BincoHax { get; private set; }
    public static string File_Cache_GTAHax { get; private set; }
    public static string File_Cache_LSCHax { get; private set; }
    public static string File_Cache_Stat { get; private set; }
    public static string File_Cache_Notepad2 { get; private set; }

    public static string File_YimMenu_DLL { get; private set; }
    public static string File_YimMenu_Index { get; private set; }
    public static string File_YimMenu_ZHCN { get; private set; }

    //////////////////////////////////////////////////////////////////

    public static string Dir_AppData { get; private set; }

    public static string Dir_AppData_YimMenu { get; private set; }
    public static string Dir_AppData_YimMenu_Scripts { get; private set; }
    public static string Dir_AppData_YimMenu_Translations { get; private set; }

    public static string File_AppData_YimMenu_Settings { get; private set; }
    public static string File_AppData_YimMenu_Logger { get; private set; }

    //////////////////////////////////////////////////////////////////

    public static string File_Config_Vehicles { get; private set; }
    public static string File_Config_Teleports { get; private set; }
    public static string File_Config_Blips { get; private set; }

    //////////////////////////////////////////////////////////////////

    static FileHelper()
    {
        Dir_MyDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        Dir_AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        Initialization(Dir_MyDocuments);
    }

    /// <summary>
    /// 初始化配置文件夹
    /// </summary>
    /// <param name="baseDir"></param>
    private static void Initialization(string baseDir)
    {
        // 文件夹路径拼接，结尾加上 \\ ，以免出现同名文件导致不会正确打开文件夹

        Dir_Base = Path.Combine(baseDir, "GTA5OnlineTools\\");
        Dir_AppData_YimMenu = Path.Combine(Dir_AppData, "YimMenu\\");

        Dir_Kiddion = Path.Combine(Dir_Base, "Kiddion\\");
        Dir_YimMenu = Path.Combine(Dir_Base, "YimMenu\\");
        Dir_Cache = Path.Combine(Dir_Base, "Cache\\");
        Dir_Config = Path.Combine(Dir_Base, "Config\\");
        Dir_Logger = Path.Combine(Dir_Base, "Logger\\");

        Dir_Kiddion_Scripts = Path.Combine(Dir_Kiddion, "scripts\\");

        Dir_Log_Crash = Path.Combine(Dir_Logger, "Crash\\");
        Dir_Log_NLog = Path.Combine(Dir_Logger, "NLog\\");

        Dir_AppData_YimMenu_Scripts = Path.Combine(Dir_AppData_YimMenu, "scripts\\");
        Dir_AppData_YimMenu_Translations = Path.Combine(Dir_AppData_YimMenu, "translations\\");

        ////////////////////////////////////////////////////////////

        File_AppData_YimMenu_Settings = Path.Combine(Dir_AppData_YimMenu, "settings.json");
        File_AppData_YimMenu_Logger = Path.Combine(Dir_AppData_YimMenu, "cout.log");

        File_Kiddion_Kiddion = Path.Combine(Dir_Kiddion, "Kiddion.exe");
        File_Kiddion_KiddionChs = Path.Combine(Dir_Kiddion, "KiddionChs.dll");
        File_Kiddion_Config = Path.Combine(Dir_Kiddion, "config.json");
        File_Kiddion_Themes = Path.Combine(Dir_Kiddion, "themes.json");
        File_Kiddion_Teleports = Path.Combine(Dir_Kiddion, "teleports.json");
        File_Kiddion_Vehicles = Path.Combine(Dir_Kiddion, "vehicles.json");

        File_Kiddion_Scripts_Readme = Path.Combine(Dir_Kiddion_Scripts, "Readme.api");

        File_Cache_BincoHax = Path.Combine(Dir_Cache, "BincoHax.exe");
        File_Cache_GTAHax = Path.Combine(Dir_Cache, "GTAHax.exe");
        File_Cache_LSCHax = Path.Combine(Dir_Cache, "LSCHax.exe");
        File_Cache_Stat = Path.Combine(Dir_Cache, "stat.txt");
        File_Cache_Notepad2 = Path.Combine(Dir_Cache, "Notepad2.exe");

        File_YimMenu_DLL = Path.Combine(Dir_YimMenu, "YimMenu.dll");

        File_YimMenu_Index = Path.Combine(Dir_AppData_YimMenu_Translations, "index.json");
        File_YimMenu_ZHCN = Path.Combine(Dir_AppData_YimMenu_Translations, "zh_CN.json");

        File_Config_Vehicles = Path.Combine(Dir_Config, "Vehicles.json");
        File_Config_Teleports = Path.Combine(Dir_Config, "Teleports.json");
        File_Config_Blips = Path.Combine(Dir_Config, "Blips.json");
    }

    /// <summary>
    /// 重新设置初始化文件夹（一般用于启动参数）
    /// </summary>
    /// <param name="baseDir"></param>
    public static void ResetBaseDir(string baseDir)
    {
        Initialization(baseDir);
    }

    /// <summary>
    /// 创建文件夹，如果文件夹存在则不创建
    /// </summary>
    /// <param name="dirPath"></param>
    public static void CreateDirectory(string dirPath)
    {
        if (!Directory.Exists(dirPath))
            Directory.CreateDirectory(dirPath);
    }

    /// <summary>
    /// 保存崩溃日志
    /// </summary>
    /// <param name="log">日志内容</param>
    public static void SaveCrashLog(string log)
    {
        var path = Path.Combine(Dir_Log_Crash, $"#Crash#{DateTime.Now:yyyyMMdd_HH-mm-ss_ffff}.log");
        File.WriteAllText(path, log);
    }

    /// <summary>
    /// 从资源文件中抽取资源文件
    /// </summary>
    /// <param name="resFileName">资源文件路径</param>
    /// <param name="outputFile">输出文件</param>
    public static void ExtractResFile(string resFileName, string outputFile)
    {
        BufferedStream inStream = null;
        FileStream outStream = null;

        try
        {
            var assembly = Assembly.GetExecutingAssembly();
            inStream = new BufferedStream(assembly.GetManifestResourceStream(resFileName));
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
        if (!File.Exists(filePath))
            return false;

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
    public static void ClearDirectory(string srcPath)
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
