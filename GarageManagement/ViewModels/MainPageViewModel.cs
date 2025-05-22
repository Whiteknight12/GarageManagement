using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GarageManagement.Pages;
using GarageManagement.Services;
using System.Threading.Tasks;

namespace GarageManagement.ViewModels
{
    public partial class MainPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ContentView currentPageContent;

        [ObservableProperty]
        private bool trangChuActive = true;

        [ObservableProperty]
        private bool tiepNhanXeActive = false;

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

        [ObservableProperty]
        private bool phanQuyenActive = false;

        [ObservableProperty]
        private bool thayDoiThamSoActive = false;

        [ObservableProperty]
        private bool isCollapsed;

        [ObservableProperty]
        private string helloText = string.Empty;

        // Thêm các thuộc tính để quản lý trạng thái mở rộng của các section
        [ObservableProperty]
        private bool quanLyExpanded;

        [ObservableProperty]
        private bool taoLapExpanded;

        [ObservableProperty]
        private bool quanTriExpanded;

        private readonly TiepNhanXePage _tiepNhanXe;
        private readonly TaoPhieuSuaChuaPage _taoPhieuSuaChua;
        private readonly LapPhieuNhapPage _taoPhieuNhap;
        private readonly QuanLiDanhSachHieuXePage _quanLiDanhSachHieuXe;
        private readonly QuanLiXePage _quanLiXePage;
        private readonly ThuTienPage _thuTienPage;
        private readonly BaoCaoDoanhSoPage _baoCaoDoanhSoPage;
        private readonly QuanLiDanhSachLoaiVatTuPage _quanLiDanhSachLoaiVatTuPage;
        private readonly AuthenticationService _authenticationServices;
        private readonly NhanSuMainPageViewModel _viewModel;
        private readonly APIClientService<NhanVien> _nhanVienService;
        private readonly PhanQuyenPage _phanQuyenPage;
        private readonly QuanLyThamSoPage _quanLyThamSoPage;
        public MainPageViewModel(
            TiepNhanXePage tiepNhanXe,
            LapPhieuNhapPage taoPhieuNhap,
            TaoPhieuSuaChuaPage taoPhieuSuaChua,
            QuanLiDanhSachHieuXePage quanLiDanhSachHieuXe,
            QuanLiXePage quanLiXePage,
            ThuTienPage thuTienPage,
            BaoCaoDoanhSoPage baoCaoDoanhSoPage,
            QuanLiDanhSachLoaiVatTuPage quanLiDanhSachLoaiVatTuPage,
            AuthenticationService authenticationService,
            NhanSuMainPageViewModel viewModel,
            APIClientService<NhanVien> nhanVienService,
            PhanQuyenPage phanQuyenPage,
            QuanLyThamSoPage quanLyThamSoPage)
        {
            _viewModel = viewModel;
            currentPageContent = new NhanSuMainPage(_viewModel);
            _tiepNhanXe = tiepNhanXe;
            _taoPhieuNhap = taoPhieuNhap;
            _taoPhieuSuaChua = taoPhieuSuaChua;
            _quanLiDanhSachHieuXe = quanLiDanhSachHieuXe;
            _quanLiXePage = quanLiXePage;
            _thuTienPage = thuTienPage;
            _baoCaoDoanhSoPage = baoCaoDoanhSoPage;
            _quanLiDanhSachLoaiVatTuPage = quanLiDanhSachLoaiVatTuPage;
            _nhanVienService = nhanVienService;
            IsCollapsed = false;
            QuanLyExpanded = false; 
            TaoLapExpanded = false;
            QuanTriExpanded = false;
            _authenticationServices = authenticationService;
            _phanQuyenPage = phanQuyenPage;
            _quanLyThamSoPage = quanLyThamSoPage;
            _ = LoadUserAsync();
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
            PhanQuyenActive = parameter == "PhanQuyen";
            ThayDoiThamSoActive = parameter == "ThayDoiThamSo";

            if(TaoPhieuSuaChuaActive == true)
            {
                _ = _taoPhieuSuaChua._viewmodel.LoadAsync();
            }

            CurrentPageContent = parameter switch
            {
                "Home" => new NhanSuMainPage(_viewModel),
                "TiepNhanXe" => _tiepNhanXe,
                "TaoPhieuSuaChua" => _taoPhieuSuaChua,
                "TaoPhieuNhap" => _taoPhieuNhap,
                "QLDanhSachHieuXe" => _quanLiDanhSachHieuXe,
                "QLXe" => _quanLiXePage,
                "ThuTien" => _thuTienPage,
                "BaoCaoDoanhSo" => _baoCaoDoanhSoPage,
                "QLDanhSachLoaiVatTu" => _quanLiDanhSachLoaiVatTuPage,
                "PhanQuyen" => _phanQuyenPage,
                "ThayDoiThamSo" => _quanLyThamSoPage,
                _ => new NhanSuMainPage(_viewModel)
            };
        }

        [RelayCommand]
        public void ToggleSection(string section)
        {
            switch (section)
            {
                case "QuanLy":
                    QuanLyExpanded = !QuanLyExpanded;
                    if (QuanLyExpanded)
                    {
                        TaoLapExpanded = false;
                        QuanTriExpanded = false;
                    }
                    break;
                case "TaoLap":
                    TaoLapExpanded = !TaoLapExpanded;
                    if (TaoLapExpanded)
                    {
                        QuanLyExpanded = false;
                        QuanTriExpanded = false;
                    }
                    break;
                case "QuanTri":
                    QuanTriExpanded = !QuanTriExpanded;
                    if (QuanTriExpanded)
                    {
                        QuanLyExpanded = false;
                        TaoLapExpanded = false;
                    }
                    break;
            }
        }

        private async Task LoadUserAsync()
        {
            _ = _authenticationServices.FettaiKhoanSession();
            var user = _authenticationServices.GetCurrentAccountStatus;
            if (user != null)
            {
                var result = await _nhanVienService.GetThroughtSpecialRoute("TaiKhoanId", user.AccountId.ToString() ?? "");
                HelloText = $"Xin chào {user.Role}: {result?.HoTen}";
            }
            else
            {
                HelloText = "Không có thông tin account";
            }
        }
    }
}