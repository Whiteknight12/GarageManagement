using GarageManagement.Pages;

namespace GarageManagement
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            FlyoutBehavior = FlyoutBehavior.Disabled;
            ItemTemplate = (DataTemplate)Resources["NavItemTemplate"];
            Routing.RegisterRoute(nameof(ThemXePage), typeof(ThemXePage));
        }

        public void setupMenu(string role)
        {
            Items.Clear();    
            FlyoutBehavior = FlyoutBehavior.Locked;
            if (role=="Admin")
            {

            }
            else if (role=="User")
            {
                Items.Add(new FlyoutItem
                {
                    Title = "Tiếp nhận xe",
                    Icon = "dotnet_bot.png",
                    Items = {new ShellContent
                    {
                        ContentTemplate = new DataTemplate(typeof(TiepNhanXePage))
                    } }
                });
                Items.Add(new FlyoutItem
                {
                    Title = "Quản lý xe",
                    Icon= "dotnet_bot.png",
                    Items = {new ShellContent
                    {
                        ContentTemplate = new DataTemplate(typeof(QuanLiXePage))
                    } }
                });
                Items.Add(new FlyoutItem
                {
                    Title = "Lập phiếu sữa chữa",
                    Icon = "dotnet_bot.png",
                    Items = {new ShellContent
                    {
                        ContentTemplate = new DataTemplate(typeof(TaoPhieuSuaChuaPage))
                    } }
                });
                Items.Add(new FlyoutItem
                {
                    Title = "Lập phiếu thu tiền",
                    Icon = "dotnet_bot.png",
                    Items = {new ShellContent
                    {
                        ContentTemplate = new DataTemplate(typeof(ThuTienPage))
                    } }
                });
                Items.Add(new FlyoutItem
                {
                    Title = "Báo cáo doanh số",
                    Icon = "dotnet_bot.png",
                    Items = {new ShellContent
                    {
                        ContentTemplate = new DataTemplate(typeof(BaoCaoDoanhSoPage))
                    } }
                });
            }
            else
            {

            }
        }
    }
}
