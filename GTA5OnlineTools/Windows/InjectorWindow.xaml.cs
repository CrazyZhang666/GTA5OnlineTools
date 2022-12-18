using GTA5OnlineTools.Utils;
using GTA5OnlineTools.Features.Core;

namespace GTA5OnlineTools.Windows;

/// <summary>
/// InjectorWindow.xaml 的交互逻辑
/// </summary>
public partial class InjectorWindow
{
    private InjectInfo InjectInfo { get; set; } = new();

    public ObservableCollection<ProcessList> ProcessLists { get; set; } = new();

    public InjectorWindow()
    {
        InitializeComponent();
    }

    private void Window_Injector_Loaded(object sender, RoutedEventArgs e)
    {
        this.DataContext = this;

        Task.Run(() =>
        {
            foreach (Process process in Process.GetProcesses())
            {
                if (!string.IsNullOrEmpty(process.MainWindowTitle))
                {
                    this.Dispatcher.BeginInvoke(DispatcherPriority.Background, () =>
                    {
                        ProcessLists.Add(new ProcessList()
                        {
                            ProcID = process.Id,
                            ProcName = process.ProcessName,
                            MainWindowTitle = process.MainWindowTitle,
                            MainWindowHandle = process.MainWindowHandle,
                        });
                    });
                }
            }
        });
    }

    private void Window_Injector_Closing(object sender, CancelEventArgs e)
    {

    }

    /// <summary>
    /// 注入 按钮 点击事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_Inject_Click(object sender, RoutedEventArgs e)
    {
        

        if (string.IsNullOrEmpty(InjectInfo.DLLPath))
        {
            TextBlock_Status.Text = "请选择DLL文件路径";
            return;
        }

        if (InjectInfo.ProcID == 0)
        {
            TextBlock_Status.Text = "请选择目标进程";
            return;
        }

        foreach (ProcessModule module in Process.GetProcessById(InjectInfo.ProcID).Modules)
        {
            if (module.FileName == InjectInfo.DLLPath)
            {
                TextBlock_Status.Text = "该DLL已经被注入过了";
                return;
            }
        }

        try
        {
            BaseInjector.DLLInjector(InjectInfo.ProcID, InjectInfo.DLLPath);
            BaseInjector.SetForegroundWindow(InjectInfo.MainWindowHandle);
            TextBlock_Status.Text = $"DLL注入到进程 {InjectInfo.ProcName} 成功";
        }
        catch (Exception ex)
        {
            TextBlock_Status.Text = ex.Message;
        }
    }

    private void Button_Refush_Click(object sender, RoutedEventArgs e)
    {
        

        lock (this)
        {
            ProcessLists.Clear();

            InjectInfo.ProcID = 0;
            InjectInfo.ProcName = string.Empty;
            InjectInfo.MainWindowHandle = IntPtr.Zero;

            Task.Run(() =>
            {
                foreach (Process process in Process.GetProcesses())
                {
                    if (!string.IsNullOrEmpty(process.MainWindowTitle))
                    {
                        this.Dispatcher.BeginInvoke(DispatcherPriority.Background, () =>
                        {
                            ProcessLists.Add(new ProcessList()
                            {
                                ProcID = process.Id,
                                ProcName = process.ProcessName,
                                MainWindowTitle = process.MainWindowTitle,
                                MainWindowHandle = process.MainWindowHandle,
                            });
                        });
                    }
                }
            });
        }
    }

    private void TextBox_DLLPath_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        var openFileDialog = new OpenFileDialog
        {
            Multiselect = false,
            RestoreDirectory = true,
            Filter = "DLL文件|*.dll"
        };

        if (openFileDialog.ShowDialog() == true)
        {
            TextBox_DLLPath.Text = openFileDialog.FileName;
            InjectInfo.DLLPath = openFileDialog.FileName;
        }
        else
        {
            TextBox_DLLPath.Text = string.Empty;
            InjectInfo.DLLPath = string.Empty;
        }
    }

    private void ListView_Process_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ListView_Process.SelectedItem is ProcessList temp)
        {
            InjectInfo.ProcID = temp.ProcID;
            InjectInfo.ProcName = temp.ProcName;
            InjectInfo.MainWindowHandle = temp.MainWindowHandle;
        }
    }
}
