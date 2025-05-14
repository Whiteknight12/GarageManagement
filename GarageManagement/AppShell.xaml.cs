using GarageManagement.Pages;

namespace GarageManagement
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("MainPage", typeof(MainPage));
            Routing.RegisterRoute("NhanSuMainPage", typeof(NhanSuMainPage));
            Routing.RegisterRoute("TiepNhanXePage", typeof(TiepNhanXePage));
            Routing.RegisterRoute("TaoPhieuSuaChuaPage", typeof(TaoPhieuSuaChuaPage));
            Routing.RegisterRoute("LapPhieuNhap", typeof(LapPhieuNhapPage));
        }
    }
}
