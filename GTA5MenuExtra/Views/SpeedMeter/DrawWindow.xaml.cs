using GTA5Core.Native;
using GTA5Core.Offsets;
using GTA5Core.Features;

using CommunityToolkit.Mvvm.ComponentModel;

namespace GTA5MenuExtra.Views.SpeedMeter;

/// <summary>
/// DrawWindow.xaml 的交互逻辑
/// </summary>
public partial class DrawWindow : Window
{
    private const float MPH = 2.23694f;
    private const float KPH = 3.6f;

    private float SpeedUnit = MPH;

    /////////////////////////////////////////////////

    /// <summary>
    /// 用于关闭窗口的时候销毁线程
    /// </summary>
    private bool isRunning = true;

    public DrawWindowModel DrawWindowModel { get; set; } = new();

    [StructLayout(LayoutKind.Sequential)]
    private struct STYLESTRUCT
    {
        public int styleOld;
        public int styleNew;
    }

    public DrawWindow()
    {
        InitializeComponent();

        this.SourceInitialized += delegate
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            var extendedStyle = Win32.GetWindowLong(hwnd, Win32.GWL_EXSTYLE);
            Win32.SetWindowLong(hwnd, Win32.GWL_EXSTYLE, extendedStyle | Win32.WS_EX_TRANSPARENT);
        };
    }

    private void Window_Draw_Loaded(object sender, RoutedEventArgs e)
    {
        ((HwndSource)PresentationSource.FromVisual(this)).AddHook((IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled) =>
        {
            // 想要让窗口透明穿透鼠标和触摸等，需要同时设置 WS_EX_LAYERED 和 WS_EX_TRANSPARENT 样式
            // 确保窗口始终有 WS_EX_LAYERED 这个样式，并在开启穿透时设置 WS_EX_TRANSPARENT 样式
            // 但是WPF窗口在未设置 AllowsTransparency = true 时，会自动去掉 WS_EX_LAYERED 样式 (在 HwndTarget 类中)
            // 如果设置了 AllowsTransparency = true 将使用WPF内置的低性能的透明实现
            // 所以这里通过 Hook 的方式，在不使用WPF内置的透明实现的情况下，强行保证这个样式存在
            if (msg == Win32.WM_STYLECHANGING && (long)wParam == Win32.GWL_EXSTYLE)
            {
                var styleStruct = (STYLESTRUCT)Marshal.PtrToStructure(lParam, typeof(STYLESTRUCT));
                styleStruct.styleNew |= Win32.WS_EX_LAYERED;
                Marshal.StructureToPtr(styleStruct, lParam, false);
                handled = true;
            }

            return IntPtr.Zero;
        });

        this.DataContext = this;

        new Thread(DrawThread)
        {
            Name = "DrawThread",
            IsBackground = true
        }.Start();

        new Thread(MainThread)
        {
            Name = "MainThread",
            IsBackground = true
        }.Start();

        Console.Beep(600, 75);
    }

    private void Window_Draw_Closing(object sender, CancelEventArgs e)
    {
        isRunning = false;
    }

    private void DrawThread()
    {
        bool isShow = true;
        bool isChangeMPH = false;
        bool isChangeKPH = false;

        while (isRunning)
        {
            var windowData = Memory.GetGameWindowData();

            this.Dispatcher.Invoke(() =>
            {
                var width = Window_Draw.ActualWidth * ScreenMgr.GetScalingRatio();

                if (DrawData.IsDrawCenter)
                {
                    this.Left = windowData.Left + windowData.Width / 2 - width / 2;
                    this.Top = windowData.Top + windowData.Height - width;
                }
                else
                {
                    this.Left = windowData.Left + windowData.Width - width * 1.05;
                    this.Top = windowData.Top + windowData.Height - width;
                }

                this.Left /= ScreenMgr.GetScalingRatio();
                this.Top /= ScreenMgr.GetScalingRatio();

                if (Vehicle.IsInVehicle() && Memory.IsForegroundWindow())
                {
                    if (!isShow)
                    {
                        this.Show();
                        isShow = true;
                    }
                }
                else
                {
                    if (isShow)
                    {
                        this.Hide();
                        isShow = false;
                    }
                }

                if (DrawData.IsShowMPH)
                {
                    if (!isChangeMPH)
                    {
                        MeterPlate_Main.Unit = "MPH";
                        MeterPlate_Main.Maximum = 200;
                        SpeedUnit = MPH;
                        isChangeKPH = false;
                        isChangeMPH = true;
                    }
                }
                else
                {
                    if (!isChangeKPH)
                    {
                        MeterPlate_Main.Unit = "KPH";
                        MeterPlate_Main.Maximum = 400;
                        SpeedUnit = KPH;
                        isChangeKPH = true;
                        isChangeMPH = false;
                    }
                }
            });

            Thread.Sleep(200);
        }
    }

    private void MainThread()
    {
        while (isRunning)
        {
            DrawWindowModel.VehicleSpeed = GetVehicleSpeed();
            DrawWindowModel.VehicleGear = GetVehicleGear();

            Thread.Sleep(20);
        }
    }

    /// <summary>
    /// 获取载具速度
    /// </summary>
    /// <returns></returns>
    private int GetVehicleSpeed()
    {
        long pCPedFactory = Memory.Read<long>(Pointers.WorldPTR);
        long pCPed = Memory.Read<long>(pCPedFactory + CPedFactory.CPed);
        byte oInVehicle = Memory.Read<byte>(pCPed + CPed.InVehicle);

        if (oInVehicle == 0x01)
        {
            long pCVehicle = Memory.Read<long>(pCPed + CPed.CVehicle);

            var v3_1 = Memory.Read<Vector3>(pCVehicle + 0x7D0);
            var VehicleSpeed1 = Math.Sqrt(Math.Pow(v3_1.X, 2) + Math.Pow(v3_1.Y, 2) + Math.Pow(v3_1.Z, 2));

            var v3_2 = Memory.Read<Vector3>(pCVehicle + 0x7D0);
            var VehicleSpeed2 = Math.Sqrt(Math.Pow(v3_2.X, 2) + Math.Pow(v3_2.Y, 2) + Math.Pow(v3_2.Z, 2));

            var VehicleSpeed = VehicleSpeed1 + (VehicleSpeed2 - VehicleSpeed1) * 0.5;
            VehicleSpeed *= SpeedUnit;

            return VehicleSpeed > 0 ? (int)VehicleSpeed : 0;
        }
        else
        {
            return 0;
        }
    }

    /// <summary>
    /// 获取载具最大速度
    /// </summary>
    /// <returns></returns>
    private double GetVehicleMaxSpeed()
    {
        long pUnk = Memory.Read<long>(Pointers.UnkPTR);
        long offset = Memory.Read<long>(pUnk + 0x08);
        offset = Memory.Read<long>(pUnk + 0xD10);

        return Memory.Read<double>(pUnk + 0x8CC) * SpeedUnit;
    }

    /// <summary>
    /// 获取载具挡位
    /// </summary>
    /// <returns></returns>
    private string GetVehicleGear()
    {
        long pUnk = Memory.Read<long>(Pointers.UnkPTR);
        int gear = Memory.Read<int>(pUnk + 0xFD4);

        return gear == 0 ? "R" : gear.ToString();
    }

    /// <summary>
    /// 获取载具加速度
    /// </summary>
    /// <returns></returns>
    private float GetVehicleRPM()
    {
        long pUnk = Memory.Read<long>(Pointers.UnkPTR);

        return Memory.Read<float>(pUnk + 0xE50);
    }
}

/// <summary>
/// DrawWindow 数据模型
/// </summary>
public class DrawWindowModel : ObservableObject
{
    private int _vehicleSpeed;
    /// <summary>
    /// 载具速度
    /// </summary>
    public int VehicleSpeed
    {
        get => _vehicleSpeed;
        set => SetProperty(ref _vehicleSpeed, value);
    }

    private string _vehicleGear;
    /// <summary>
    /// 载具档位
    /// </summary>
    public string VehicleGear
    {
        get => _vehicleGear;
        set => SetProperty(ref _vehicleGear, value);
    }
}
