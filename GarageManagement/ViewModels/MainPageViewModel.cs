using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GarageManagement.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageManagement.ViewModels
{
    public partial class MainPageViewModel: BaseViewModel
    {
        [ObservableProperty]
        private ContentView currentPageContent;
        [ObservableProperty]
        private bool trangChuActive = true;
        [ObservableProperty]
        private bool tiepNhanXeActive= false;
        [ObservableProperty]
        private bool taoPhieuNhapActive = false;
        [ObservableProperty]
        private bool taoPhieuSuaChuaActive = false;

        private readonly TiepNhanXePage _tiepNhanXe;
        private readonly TaoPhieuSuaChuaPage _taoPhieuSuaChua;
        private readonly LapPhieuNhapPage _taoPhieuNhap;

        public MainPageViewModel(TiepNhanXePage tiepNhanXe, LapPhieuNhapPage taoPhieuNhap, TaoPhieuSuaChuaPage taoPhieuSuaChua)
        {
            currentPageContent = new NhanSuMainPage();
            _tiepNhanXe = tiepNhanXe;
            _taoPhieuNhap = taoPhieuNhap;
            _taoPhieuSuaChua = taoPhieuSuaChua;
        }

        [RelayCommand]
        public void Navigate(string parameter)
        {
            TrangChuActive = parameter == "Home";
            TiepNhanXeActive = parameter == "TiepNhanXe";
            TaoPhieuNhapActive = parameter == "TaoPhieuNhap";
            TaoPhieuSuaChuaActive = parameter == "TaoPhieuSuaChua";
            CurrentPageContent = parameter switch
            {
                "Home" => new NhanSuMainPage(),
                "TiepNhanXe" => _tiepNhanXe,
                "TaoPhieuSuaChua" => _taoPhieuSuaChua,
                _ => new NhanSuMainPage()
            };
        }
    }
}
