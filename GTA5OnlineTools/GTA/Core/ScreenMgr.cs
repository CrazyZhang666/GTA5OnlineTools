namespace GTA5OnlineTools.GTA.Core;

public static class ScreenMgr
{
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    [ResourceExposure(ResourceScope.None)]
    private static extern bool GetMonitorInfo(HandleRef hmonitor, [In, Out] MONITORINFOEX info);

    [DllImport("user32.dll", ExactSpelling = true)]
    [ResourceExposure(ResourceScope.None)]
    private static extern bool EnumDisplayMonitors(HandleRef hdc, COMRECT rcClip, MonitorEnumProc lpfnEnum, IntPtr dwData);

    private delegate bool MonitorEnumProc(IntPtr monitor, IntPtr hdc, IntPtr lprcMonitor, IntPtr lParam);

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
    private class MONITORINFOEX
    {
        internal int cbSize = Marshal.SizeOf(typeof(MONITORINFOEX));
        internal RECT rcMonitor = new();
        internal RECT rcWork = new();
        internal int dwFlags = 0;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        internal char[] szDevice = new char[32];
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;

        public RECT(Rect r)
        {
            left = (int)r.Left;
            top = (int)r.Top;
            right = (int)r.Right;
            bottom = (int)r.Bottom;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    private class COMRECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }

    private static HandleRef NullHandleRef = new(null, IntPtr.Zero);

    /// <summary>
    /// 获取缩放比例
    /// </summary>
    /// <returns></returns>
    public static double GetScalingRatio()
    {
        var logicalHeight = GetLogicalHeight();
        var actualHeight = GetActualHeight();

        if (logicalHeight > 0 && actualHeight > 0)
        {
            return logicalHeight / actualHeight;
        }

        return 1;
    }

    private static double GetActualHeight()
    {
        return SystemParameters.PrimaryScreenHeight;
    }

    private static double GetLogicalHeight()
    {
        var logicalHeight = 0.0;

        MonitorEnumProc proc = (m, h, lm, lp) =>
        {
            MONITORINFOEX info = new();
            GetMonitorInfo(new HandleRef(null, m), info);

            // 是否为主屏
            if ((info.dwFlags & 0x00000001) != 0)
            {
                logicalHeight = info.rcMonitor.bottom - info.rcMonitor.top;
            }

            return true;
        };

        EnumDisplayMonitors(NullHandleRef, null, proc, IntPtr.Zero);

        return logicalHeight;
    }
}
