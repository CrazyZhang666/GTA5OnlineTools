using GTA5Core.Native;
using GTA5Core.Offsets;

namespace GTA5Core;

/// <summary>
/// GTA5InitWindow.xaml 的交互逻辑
/// </summary>
public partial class GTA5InitWindow
{
    public bool IsAutoClose { get; }

    public GTA5InitWindow(bool isAutoClose = true)
    {
        InitializeComponent();

        IsAutoClose = isAutoClose;
    }

    private void Window_GTA5Init_Loaded(object sender, RoutedEventArgs e)
    {
        Task.Run(() =>
        {
            Memory.IsInitialized = false;

            if (GTA5Init())
            {
                if (PatternInit())
                {
                    Logger("《GTA5》相关模块初始化完毕");
                    Logger("请关闭此初始化窗口");
                    Memory.IsInitialized = true;

                    if (IsAutoClose)
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            this.Close();
                        });
                    }

                    return;
                }
            }

            Memory.CloseHandle();
        });
    }

    private void Window_GTA5Init_Closing(object sender, CancelEventArgs e)
    {
        this.DialogResult = Memory.IsInitialized;
    }

    private void Logger(string log)
    {
        this.Dispatcher.Invoke(() =>
        {
            TextBox_Logger.AppendText($"{log}\n");
            TextBox_Logger.ScrollToEnd();
        });
    }

    private bool GTA5Init()
    {
        try
        {
            Logger("开始初始化《GTA5》内存模块");
            var pArray = Process.GetProcessesByName("GTA5");
            Logger($"《GTA5》进程数量 {pArray.Length}");

            if (pArray.Length == 0)
            {
                Logger("发生错误，未发现《GTA5》进程");
                return false;
            }

            Memory.GTA5Process = pArray.First();
            Logger($"《GTA5》取第一个进程实例成功");

            if (Memory.GTA5Process.MainWindowHandle == IntPtr.Zero)
            {
                Logger("发生错误，《GTA5》窗口句柄为空");
                return false;
            }

            Memory.GTA5WinHandle = Memory.GTA5Process.MainWindowHandle;
            Logger($"《GTA5》程序窗口句柄 {Memory.GTA5WinHandle}");

            Logger($"《GTA5》程序窗口标题 {Memory.GTA5Process.MainWindowTitle}");

            if (Memory.GTA5Process.Id == 0)
            {
                Logger("发生错误，《GTA5》程序进程ID为空");
                return false;
            }

            Memory.GTA5ProId = Memory.GTA5Process.Id;
            Logger($"《GTA5》程序进程ID {Memory.GTA5ProId}");

            if (Memory.GTA5Process.MainModule is null)
            {
                Logger("发生错误，《GTA5》程序主模块基址为空");
                return false;
            }

            Memory.GTA5ProBaseAddress = Memory.GTA5Process.MainModule.BaseAddress.ToInt64();
            Logger($"《GTA5》程序主模块基址 0x{Memory.GTA5ProBaseAddress:X}");

            Memory.GTA5ProHandle = Win32.OpenProcess(ProcessAccessFlags.All, false, Memory.GTA5ProId);
            if (Memory.GTA5ProHandle == IntPtr.Zero)
            {
                Logger($"发生错误，《GTA5》程序进程句柄为空");
                return false;
            }

            Logger($"《GTA5》程序进程句柄 {Memory.GTA5ProHandle}");
            return true;
        }
        catch (Exception ex)
        {
            Logger($"《GTA5》内存模块初始化异常 {ex.Message}");
            return false;
        }
    }

    private bool PatternInit()
    {
        Logger("开始初始化《GTA5》特征码模块");

        Pointers.WorldPTR = Memory.FindPattern(Mask.World);
        Pointers.WorldPTR = Memory.Rip_37(Pointers.WorldPTR);
        Logger($"《GTA5》WorldPTR 0x{Pointers.WorldPTR:X}");
        if (!Memory.IsValid(Pointers.WorldPTR))
            return false;

        Pointers.BlipPTR = Memory.FindPattern(Mask.Blip);
        Pointers.BlipPTR = Memory.Rip_37(Pointers.BlipPTR);
        Logger($"《GTA5》BlipPTR 0x{Pointers.BlipPTR:X}");
        if (!Memory.IsValid(Pointers.BlipPTR))
            return false;

        Pointers.GlobalPTR = Memory.FindPattern(Mask.Global);
        Pointers.GlobalPTR = Memory.Rip_37(Pointers.GlobalPTR);
        Logger($"《GTA5》GlobalPTR 0x{Pointers.GlobalPTR:X}");
        if (!Memory.IsValid(Pointers.GlobalPTR))
            return false;

        Pointers.ReplayInterfacePTR = Memory.FindPattern(Mask.ReplayInterface);
        Pointers.ReplayInterfacePTR = Memory.Rip_37(Pointers.ReplayInterfacePTR);
        Logger($"《GTA5》ReplayInterfacePTR 0x{Pointers.ReplayInterfacePTR:X}");
        if (!Memory.IsValid(Pointers.ReplayInterfacePTR))
            return false;

        Pointers.NetworkPlayerMgrPTR = Memory.FindPattern(Mask.NetworkPlayerMgr);
        Pointers.NetworkPlayerMgrPTR = Memory.Rip_37(Pointers.NetworkPlayerMgrPTR);
        Logger($"《GTA5》NetworkPlayerMgrPTR 0x{Pointers.NetworkPlayerMgrPTR:X}");
        if (!Memory.IsValid(Pointers.NetworkPlayerMgrPTR))
            return false;

        Pointers.NetworkInfoPTR = Memory.FindPattern(Mask.NetworkInfo);
        Pointers.NetworkInfoPTR = Memory.Rip_37(Pointers.NetworkInfoPTR);
        Logger($"《GTA5》NetworkInfoPTR 0x{Pointers.NetworkInfoPTR:X}");
        if (!Memory.IsValid(Pointers.NetworkInfoPTR))
            return false;

        Pointers.ViewPortPTR = Memory.FindPattern(Mask.ViewPort);
        Pointers.ViewPortPTR = Memory.Rip_37(Pointers.ViewPortPTR);
        Logger($"《GTA5》ViewPortPTR 0x{Pointers.ViewPortPTR:X}");
        if (!Memory.IsValid(Pointers.ViewPortPTR))
            return false;

        Pointers.CCameraPTR = Memory.FindPattern(Mask.CCamera);
        Pointers.CCameraPTR = Memory.Rip_37(Pointers.CCameraPTR);
        Logger($"《GTA5》CCameraPTR 0x{Pointers.CCameraPTR:X}");
        if (!Memory.IsValid(Pointers.CCameraPTR))
            return false;

        Pointers.AimingPedPTR = Memory.FindPattern(Mask.AimingPed);
        Pointers.AimingPedPTR = Memory.Rip_37(Pointers.AimingPedPTR);
        Logger($"《GTA5》AimingPedPTR 0x{Pointers.AimingPedPTR:X}");
        if (!Memory.IsValid(Pointers.AimingPedPTR))
            return false;

        Pointers.WeatherPTR = Memory.FindPattern(Mask.Weather);
        Pointers.WeatherPTR = Memory.Rip_6A(Pointers.WeatherPTR);
        Logger($"《GTA5》WeatherPTR 0x{Pointers.WeatherPTR:X}");
        if (!Memory.IsValid(Pointers.WeatherPTR))
            return false;

        //Pointers.TimePTR = Memory.FindPattern(Mask.Time);
        //Pointers.TimePTR = Memory.Rip_6A(Pointers.TimePTR);
        //Logger($"《GTA5》TimePTR 0x{Pointers.TimePTR:X}");
        //if (!Memory.IsValid(Pointers.TimePTR))
        //    return false;

        Pointers.PickupDataPTR = Memory.FindPattern(Mask.PickupData);
        Pointers.PickupDataPTR = Memory.Rip_37(Pointers.PickupDataPTR);
        Logger($"《GTA5》PickupDataPTR 0x{Pointers.PickupDataPTR:X}");
        if (!Memory.IsValid(Pointers.PickupDataPTR))
            return false;

        //Pointers.UnkModelPTR = Memory.FindPattern(Mask.UnkModel);
        //Pointers.UnkModelPTR = Memory.Rip_37(Pointers.UnkModelPTR);
        //Logger($"《GTA5》UnkModelPTR 0x{Pointers.UnkModelPTR:X}");
        //if (!Memory.IsValid(Pointers.UnkModelPTR))
        //    return false;

        Pointers.LocalScriptsPTR = Memory.FindPattern(Mask.LocalScripts);
        Pointers.LocalScriptsPTR = Memory.Rip_37(Pointers.LocalScriptsPTR);
        Logger($"《GTA5》LocalScriptsPTR 0x{Pointers.LocalScriptsPTR:X}");
        if (!Memory.IsValid(Pointers.LocalScriptsPTR))
            return false;

        Pointers.UnkPTR = Memory.FindPattern(Mask.Unk);
        Pointers.UnkPTR = Memory.Rip_37(Pointers.UnkPTR);
        Logger($"《GTA5》UnkPTR 0x{Pointers.UnkPTR:X}");
        if (!Memory.IsValid(Pointers.UnkPTR))
            return false;

#if DEBUG
        Test();
#endif

        return true;
    }

    private void Test()
    {
        //var myV3 = Teleport.GetPlayerPosition();

        //for (var i = 1; i <= 2000; i++)
        //{
        //    var pBlip = Memory.Read<long>(Pointers.BlipPTR + i * 0x08);
        //    if (!Memory.IsValid(pBlip))
        //        continue;

        //    var dwIcon = Memory.Read<int>(pBlip + 0x40);
        //    var dwColor = Memory.Read<byte>(pBlip + 0x48);
        //    var vector3 = Memory.Read<Vector3>(pBlip + 0x10);

        //    if (dwIcon < 0 || dwIcon > 1000)
        //        continue;

        //    if (vector3 == Vector3.Zero)
        //        continue;

        //    var distance = (float)Math.Sqrt(
        //        Math.Pow(myV3.X - vector3.X, 2) +
        //        Math.Pow(myV3.Y - vector3.Y, 2) +
        //        Math.Pow(myV3.Z - vector3.Z, 2));

        //    if (distance < 100)
        //        Debug.WriteLine($"{dwIcon} {dwColor} {vector3}");
        //}
    }
}
