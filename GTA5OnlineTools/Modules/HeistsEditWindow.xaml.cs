using GTA5OnlineTools.Data;
using GTA5OnlineTools.Modules.HeistsEdit;

using CommunityToolkit.Mvvm.Input;

namespace GTA5OnlineTools.Modules;

/// <summary>
/// HeistsEditWindow.xaml 的交互逻辑
/// </summary>
public partial class HeistsEditWindow
{
    /// <summary>
    /// 导航菜单
    /// </summary>
    public List<MenuBar> MenuBars { get; set; } = new();
    /// <summary>
    /// 导航命令
    /// </summary>
    public RelayCommand<MenuBar> NavigateCommand { get; private set; }

    private readonly ContractView ContractView = new();
    private readonly PericoView PericoView = new();
    private readonly CasinoView CasinoView = new();
    private readonly DoomsdayView DoomsdayView = new();
    private readonly ApartmentView ApartmentView = new();

    ///////////////////////////////////////////////////////////////

    public HeistsEditWindow()
    {
        InitializeComponent();
    }

    private void Window_HeistPreps_Loaded(object sender, RoutedEventArgs e)
    {
        this.DataContext = this;

        // 创建菜单
        CreateMenuBar();
        // 绑定菜单切换命令
        NavigateCommand = new(Navigate);
        // 设置主页
        ContentControl_Main.Content = ContractView;

        ///////////////////////////////////////////////////////////////;
    }

    private void Window_HeistPreps_Closing(object sender, CancelEventArgs e)
    {

    }

    /// <summary>
    /// 创建导航菜单
    /// </summary>
    private void CreateMenuBar()
    {
        MenuBars.Add(new MenuBar() { Emoji = "🍎", Title = "事所合约", NameSpace = "ContractView" });
        MenuBars.Add(new MenuBar() { Emoji = "🍐", Title = "佩里克岛", NameSpace = "PericoView" });
        MenuBars.Add(new MenuBar() { Emoji = "🍋", Title = "赌场抢劫", NameSpace = "CasinoView" });
        MenuBars.Add(new MenuBar() { Emoji = "🍇", Title = "末日抢劫", NameSpace = "DoomsdayView" });
        MenuBars.Add(new MenuBar() { Emoji = "🍓", Title = "公寓抢劫", NameSpace = "ApartmentView" });
    }

    /// <summary>
    /// 页面导航（重复点击不会重复触发）
    /// </summary>
    /// <param name="menu"></param>
    private void Navigate(MenuBar menu)
    {
        if (menu == null || string.IsNullOrEmpty(menu.NameSpace))
            return;

        switch (menu.NameSpace)
        {
            case "ContractView":
                ContentControl_Main.Content = ContractView;
                break;
            case "PericoView":
                ContentControl_Main.Content = PericoView;
                break;
            case "CasinoView":
                ContentControl_Main.Content = CasinoView;
                break;
            case "DoomsdayView":
                ContentControl_Main.Content = DoomsdayView;
                break;
            case "ApartmentView":
                ContentControl_Main.Content = ApartmentView;
                break;
        }
    }
}
