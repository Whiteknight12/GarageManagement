﻿using GarageManagement.Pages;

namespace GarageManagement
{
    public partial class AppShell : Shell
    {
        private readonly App _app;
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("MainPage", typeof(MainPage));
            Routing.RegisterRoute("NhanSuMainPage", typeof(NhanSuMainPage));
            Routing.RegisterRoute("TiepNhanXePage", typeof(TiepNhanXePage));
            Routing.RegisterRoute("TaoPhieuSuaChuaPage", typeof(TaoPhieuSuaChuaPage));
            Routing.RegisterRoute("LapPhieuNhapPage", typeof(LapPhieuNhapPage));
            Routing.RegisterRoute("QuanLiDanhSachHieuXePage", typeof(QuanLiDanhSachHieuXePage));
            Routing.RegisterRoute("ThemXePage", typeof(ThemXePage));
            Routing.RegisterRoute("ChiTietXePage", typeof(ChiTietXePage));
            Routing.RegisterRoute("ChiTietKhachHangPage", typeof(ChiTietKhachHangPage));
            Routing.RegisterRoute("ChiTietNhanVienPage", typeof(ChiTietNhanVienPage));
            Routing.RegisterRoute("LoaiTienCongPage", typeof(LoaiTienCongPage));
            Routing.RegisterRoute("ThemLoaiTienCongPage", typeof(ThemLoaiTienCongPage));
            Routing.RegisterRoute("QuanLiDanhSachLoaiVatTuPage", typeof(QuanLiDanhSachLoaiVatTuPage));
            Routing.RegisterRoute("ThemLoaiVatTuPhuTungPage", typeof(ThemLoaiVatTuPhuTungPage));
            FlyoutBehavior = FlyoutBehavior.Disabled;
            _app = (App)Application.Current;
        }
        private void OnToggleThemeClicked(object sender, EventArgs e)
        {
            _app.ToggleTheme();
        }
    }
}
