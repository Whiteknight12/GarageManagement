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
        [ObservableProperty]
        private bool qlDanhSachHieuXeActive = false;
        [ObservableProperty]
        private bool qlXeActive = false;
        [ObservableProperty]
        private bool thuTienActive = false;
        [ObservableProperty]
        private bool baoCaoDoanhSoActive = false;
        [ObservableProperty]
        private bool quanLiDanhSachLoaiVatTuActive = false;

        private readonly TiepNhanXePage _tiepNhanXe;
        private readonly TaoPhieuSuaChuaPage _taoPhieuSuaChua;
        private readonly LapPhieuNhapPage _taoPhieuNhap;
        private readonly QuanLiDanhSachHieuXePage _quanLiDanhSachHieuXe;
        private readonly QuanLiXePage _quanLiXePage;
        private readonly ThuTienPage _thuTienPage;
        private readonly BaoCaoDoanhSoPage _baoCaoDoanhSoPage;
        private readonly QuanLiDanhSachLoaiVatTuPage _quanLiDanhSachLoaiVatTuPage;
        public MainPageViewModel(TiepNhanXePage tiepNhanXe, 
            LapPhieuNhapPage taoPhieuNhap, 
            TaoPhieuSuaChuaPage taoPhieuSuaChua,
            QuanLiDanhSachHieuXePage quanLiDanhSachHieuXe,
            QuanLiXePage quanLiXePage,
            ThuTienPage thuTienPage,
            BaoCaoDoanhSoPage baoCaoDoanhSoPage,
            QuanLiDanhSachLoaiVatTuPage quanLiDanhSachLoaiVatTuPage)
        {
            currentPageContent = new NhanSuMainPage();
            _tiepNhanXe = tiepNhanXe;
            _taoPhieuNhap = taoPhieuNhap;
            _taoPhieuSuaChua = taoPhieuSuaChua;
            _quanLiDanhSachHieuXe = quanLiDanhSachHieuXe;
            _quanLiXePage = quanLiXePage;
            _thuTienPage = thuTienPage;
            _baoCaoDoanhSoPage = baoCaoDoanhSoPage;
            _quanLiDanhSachLoaiVatTuPage = quanLiDanhSachLoaiVatTuPage;
        }

        [RelayCommand]
        public void Navigate(string parameter)
        {
            TrangChuActive = parameter == "Home";
            TiepNhanXeActive = parameter == "TiepNhanXe";
            TaoPhieuNhapActive = parameter == "TaoPhieuNhap";
            TaoPhieuSuaChuaActive = parameter == "TaoPhieuSuaChua";
            QlDanhSachHieuXeActive = parameter == "QLDanhSachHieuXe";
            QlXeActive = parameter == "QLXe";
            ThuTienActive = parameter == "ThuTien";
            BaoCaoDoanhSoActive = parameter == "BaoCaoDoanhSo";
            QuanLiDanhSachLoaiVatTuActive = parameter == "QLDanhSachLoaiVatTu";
            CurrentPageContent = parameter switch
            {
                "Home" => new NhanSuMainPage(),
                "TiepNhanXe" => _tiepNhanXe,
                "TaoPhieuSuaChua" => _taoPhieuSuaChua,
                "TaoPhieuNhap" => _taoPhieuNhap,
                "QLDanhSachHieuXe" => _quanLiDanhSachHieuXe,
                "QLXe" => _quanLiXePage,
                "ThuTien" =>  _thuTienPage,
                "BaoCaoDoanhSo" => _baoCaoDoanhSoPage,
                "QLDanhSachLoaiVatTu" => _quanLiDanhSachLoaiVatTuPage,
                _ => new NhanSuMainPage()
            };
        }
    }
}
