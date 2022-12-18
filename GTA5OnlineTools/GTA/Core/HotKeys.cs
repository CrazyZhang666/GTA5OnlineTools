namespace GTA5OnlineTools.GTA.Core;

public static class HotKeys
{
    private static Dictionary<int, WinKey> WinHotKeys = new();

    public delegate void KeyHandler(WinVK vK);
    public static event KeyHandler KeyUpEvent;
    public static event KeyHandler KeyDownEvent;

    /// <summary>
    /// 初始化
    /// </summary>
    static HotKeys()
    {
        new Thread(UpdateHotKeyThread)
        {
            Name = "UpdateHotKeyThread",
            IsBackground = true
        }.Start();
    }

    /// <summary>
    /// 增加快捷按键（自动过滤重复按键）
    /// </summary>
    /// <param name="key"></param>
    public static void AddKey(WinVK key)
    {
        int keyId = (int)key;
        if (!WinHotKeys.ContainsKey(keyId))
        {
            WinHotKeys.Add(keyId, new WinKey() { Key = key });
        }
    }

    /// <summary>
    /// 清空全部快捷键
    /// </summary>
    public static void ClearKeys()
    {
        WinHotKeys.Clear();
        KeyUpEvent = null;
        KeyDownEvent = null;
    }

    /// <summary>
    /// 按键按下
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Name"></param>
    private static void OnKeyDown(WinVK vK)
    {
        KeyDownEvent?.Invoke(vK);
    }

    /// <summary>
    /// 按键弹起
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Name"></param>
    private static void OnKeyUp(WinVK vK)
    {
        KeyUpEvent?.Invoke(vK);
    }

    /// <summary>
    /// 按键监听线程
    /// </summary>
    /// <param name="sender"></param>
    private static void UpdateHotKeyThread(object sender)
    {
        while (MainWindow.IsAppRunning)
        {
            if (WinHotKeys.Count > 0)
            {
                var keysData = new List<WinKey>(WinHotKeys.Values);
                if (keysData != null && keysData.Count > 0)
                {
                    foreach (WinKey key in keysData)
                    {
                        if (Convert.ToBoolean(Win32.GetAsyncKeyState((int)key.Key) & Win32.KEY_PRESSED))
                        {
                            if (!key.IsKeyDown)
                            {
                                key.IsKeyDown = true;
                                OnKeyDown(key.Key);
                            }
                        }
                        else
                        {
                            if (key.IsKeyDown)
                            {
                                key.IsKeyDown = false;
                                OnKeyUp(key.Key);
                            }
                        }
                    }
                }
            }

            Thread.Sleep(20);
        }
    }
}

public class WinKey
{
    public WinVK Key { get; set; }
    public bool IsKeyDown { get; set; }
}
