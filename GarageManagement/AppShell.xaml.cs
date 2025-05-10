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
            }
            else
            {

            }
        }
    }
}
