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
        private bool quanLiTaiKhoanActive = false;

        [ObservableProperty]
        private bool quanLiNhanVienActive = false;

        [ObservableProperty]
        private bool quanLiKhachHangActive = false;

        [ObservableProperty]
        private bool quanLiPhieuNhapActive = false;

        [ObservableProperty]
        private bool quanLiPhieuThuTienActive = false;

        [ObservableProperty]
        private bool quanLiPhieuSuaChuaActive = false;

        [ObservableProperty]
        private bool quanLiPhieuTiepNhanActive = false;

        [ObservableProperty] private bool baoCaoDoanhSoListActive = false;

        [ObservableProperty]
        private bool quanLiLichSuActive = false;

        [ObservableProperty]
        private bool danhSachXeActive = false;

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

        [ObservableProperty]
        private bool khachHangExpanded;

        [ObservableProperty]
        private bool isRightPaneVisible = false;

        [ObservableProperty]
        private ContentView rightPaneContent;

        [ObservableProperty]
        private bool quanLiDanhSachLoaiTienCongActive = false;

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
        private readonly QuanLiTaiKhoanPage _quanLiTaiKhoanPage;
        private readonly QuanLiNhanVienPage _quanLiNhanVienPage;
        private readonly QuanLiKhachHangPage _quanLiKhachHangPage;
        private readonly QuanLiPhieuNhapPage _quanLiPhieuNhapPage;
        private readonly QuanLiPhieuThuTienPage _quanLiPhieuThuTienPage;
        private readonly QuanLiPhieuSuaChuaPage _quanLiPhieuSuaChuaPage;
        private readonly QuanLiPhieuTiepNhanXePage _quanLiPhieuTiepNhanXePage;
        private readonly DanhSachXeKhachHangPage _danhSachXeKhachHangPage;
        private readonly LichSuPage _quanLiLichSuPage;
        private readonly LoaiTienCongPage _loaiTienCongPage;
        private readonly ThemLoaiTienCongPage _themLoaiTienCongPage;
        private readonly ThemLoaiVatTuPhuTungPage _themLoaiVatTuPhuTungPage;
        private readonly BaoCaoDoanhSoListPage _baoCaoDoanhSoListPage;

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
            QuanLyThamSoPage quanLyThamSoPage,
            QuanLiTaiKhoanPage quanLiTaiKhoanPage,
            QuanLiNhanVienPage quanLiNhanVienPage,
            QuanLiKhachHangPage quanLiKhachHangPage,
            QuanLiPhieuNhapPage quanLiPhieuNhapPage,
            QuanLiPhieuThuTienPage quanLiPhieuThuTienPage,
            QuanLiPhieuSuaChuaPage quanLiPhieuSuaChuaPage,
            QuanLiPhieuTiepNhanXePage quanLiPhieuTiepNhanXePage,
            DanhSachXeKhachHangPage danhSachXeKhachHangPage,
            LichSuPage quanLiLichSuPage,
            LoaiTienCongPage loaiTienCongPage,
            ThemLoaiTienCongPage themLoaiTienCongPage,
            ThemLoaiVatTuPhuTungPage themLoaiVatTuPhuTungPage,
            BaoCaoDoanhSoListPage baoCaoDoanhSoListPage)
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
            _quanLiTaiKhoanPage = quanLiTaiKhoanPage;
            _quanLiNhanVienPage = quanLiNhanVienPage;
            _quanLiDanhSachHieuXe = quanLiDanhSachHieuXe;
            _quanLiKhachHangPage = quanLiKhachHangPage;
            _quanLiPhieuNhapPage = quanLiPhieuNhapPage;
            _quanLiPhieuThuTienPage = quanLiPhieuThuTienPage;
            _quanLiPhieuSuaChuaPage = quanLiPhieuSuaChuaPage;
            _quanLiPhieuTiepNhanXePage = quanLiPhieuTiepNhanXePage;
            _danhSachXeKhachHangPage = danhSachXeKhachHangPage;
            _quanLiLichSuPage = quanLiLichSuPage;
            _loaiTienCongPage = loaiTienCongPage;
            _themLoaiTienCongPage = themLoaiTienCongPage;
            _themLoaiVatTuPhuTungPage = themLoaiVatTuPhuTungPage;
            _baoCaoDoanhSoListPage = baoCaoDoanhSoListPage;
            _ = LoadUserAsync();
        }

        [RelayCommand]
        public void Navigate(string parameter)
        {
            TrangChuActive = parameter == "Home";

            TiepNhanXeActive = parameter == "TiepNhanXe";
            //if (TiepNhanXeActive == true)
            //{
            //    _ = _tiepNhanXe._viewmodel.LoadAsync();
            //}

            TaoPhieuNhapActive = parameter == "TaoPhieuNhap";
            if (TaoPhieuNhapActive == true)
            {
                _ = _taoPhieuNhap._viewModel.LoadListVatTuAsync();
            }

            TaoPhieuSuaChuaActive = parameter == "TaoPhieuSuaChua";
            if (TaoPhieuSuaChuaActive == true)
            {
                _ = _taoPhieuSuaChua._viewmodel.LoadAsync();
            }

            QlDanhSachHieuXeActive = parameter == "QLDanhSachHieuXe";
            if (QlDanhSachHieuXeActive == true)
            {
                _ = _quanLiDanhSachHieuXe._viewModel.LoadAsync();
            }

            QlXeActive = parameter == "QLXe";
            if (QlXeActive == true)
            {
                _ = _quanLiXePage._quanLiXePageViewModel.LoadAsync();
            }

            ThuTienActive = parameter == "ThuTien";
            if (ThuTienActive == true)
            {
                _ = _thuTienPage._viewmodel.LoadAsync();
            }

            BaoCaoDoanhSoActive = parameter == "BaoCaoDoanhSo";
            if (BaoCaoDoanhSoActive == true)
            {
                _ = _baoCaoDoanhSoPage._viewmodel.LoadAsync();
            }

            QuanLiDanhSachLoaiVatTuActive = parameter == "QLDanhSachLoaiVatTu";
            if (QuanLiDanhSachLoaiVatTuActive == true)
            {
                if (_quanLiDanhSachLoaiVatTuPage.BindingContext is QuanLiDanhSachLoaiVatTuPageViewModel viewModel)
                {
                    _ = viewModel.LoadAsync();
                }
            }

            PhanQuyenActive = parameter == "PhanQuyen";
            if (PhanQuyenActive == true)
            {
                _ = _phanQuyenPage._viewModel.LoadAsync();
            }

            ThayDoiThamSoActive = parameter == "ThayDoiThamSo";
            if (ThayDoiThamSoActive == true)
            {
                _ = _quanLyThamSoPage._viewModel.LoadAsync();
            }

            QuanLiTaiKhoanActive = parameter == "QuanLiTaiKhoan";
            if (QuanLiTaiKhoanActive == true)
            {
                _ = _quanLiTaiKhoanPage._viewModel.LoadAsync();
            }

            QuanLiNhanVienActive = parameter == "QuanLiNhanVien";
            if (QuanLiNhanVienActive == true)
            {
                _ = _quanLiNhanVienPage._viewModel.LoadAsync();
            }

            QuanLiKhachHangActive = parameter == "QuanLiKhachHang";
            if (QuanLiKhachHangActive == true)
            {
                _ = _quanLiKhachHangPage._viewModel.LoadAsync();
            }

            QuanLiPhieuNhapActive = parameter == "QuanLiPhieuNhap";
            if (QuanLiPhieuNhapActive == true)
            {
                _ = _quanLiPhieuNhapPage._viewModel.LoadAsync();
            }

            QuanLiPhieuThuTienActive = parameter == "QuanLiPhieuThuTien";
            if (QuanLiPhieuThuTienActive == true)
            {
                _ = _quanLiPhieuThuTienPage._viewModel.LoadAsync();
            }

            QuanLiPhieuSuaChuaActive = parameter == "QuanLiPhieuSuaChua";
            if (QuanLiPhieuSuaChuaActive == true)
            {
                _ = _quanLiPhieuSuaChuaPage._viewModel.LoadAsync();
            }

            QuanLiPhieuTiepNhanActive = parameter == "QuanLiPhieuTiepNhan";
            if (QuanLiPhieuTiepNhanActive == true)
            {
                _ = _quanLiPhieuTiepNhanXePage._viewModel.LoadAsync();
            }

            DanhSachXeActive = parameter == "DanhSachXe";
            if (DanhSachXeActive == true)
            {
                _ = _danhSachXeKhachHangPage._viewModel.LoadAsync();
            }

            QuanLiLichSuActive = parameter == "QuanLiLichSu";
            if (QuanLiLichSuActive == true)
            {
                _ = _quanLiLichSuPage._viewModel.LoadAsync();
            }
            //QuanLiDanhSachLoaiTienCongActive = parameter == "QLDanhSachLoaiTienCong";
            //if (QuanLiDanhSachLoaiTienCongActive == true)
            //{
            //    _ = _loaiTienCongPage._viewModel.LoadAsync();
            //}

            QuanLiDanhSachLoaiTienCongActive = parameter == "QLDanhSachLoaiTienCong";
            if (QuanLiDanhSachLoaiTienCongActive == true)
            {
                if (_loaiTienCongPage.BindingContext is LoaiTienCongPageViewModel viewModel)
                {
                    _ = viewModel.LoadAsync(); 
                }
            }
            
            BaoCaoDoanhSoListActive = parameter == "BaoCaoDoanhSoListPage";
            if (BaoCaoDoanhSoListActive == true)
            {
                if (_baoCaoDoanhSoListPage.BindingContext is BaoCaoDoanhSoListPageViewModel viewModel)
                {
                    _ = viewModel.LoadAsync();
                }
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
                "QuanLiTaiKhoan" => _quanLiTaiKhoanPage,
                "QuanLiNhanVien" => _quanLiNhanVienPage,
                "QuanLiKhachHang" => _quanLiKhachHangPage,
                "QuanLiPhieuNhap" => _quanLiPhieuNhapPage,
                "QuanLiPhieuThuTien" => _quanLiPhieuThuTienPage,
                "QuanLiPhieuSuaChua" => _quanLiPhieuSuaChuaPage,
                "QuanLiPhieuTiepNhan" => _quanLiPhieuTiepNhanXePage,
                "DanhSachXe" => _danhSachXeKhachHangPage,
                "QuanLiLichSu" => _quanLiLichSuPage,
                "QLDanhSachLoaiTienCong" => _loaiTienCongPage,
                "BaoCaoDoanhSoList" => _baoCaoDoanhSoListPage,
                _ => new NhanSuMainPage(_viewModel)
            };

            if (IsRightPaneVisible) CloseRightPane();
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
                        KhachHangExpanded = false;
                    }
                    break;
                case "TaoLap":
                    TaoLapExpanded = !TaoLapExpanded;
                    if (TaoLapExpanded)
                    {
                        QuanLyExpanded = false;
                        QuanTriExpanded = false;
                        KhachHangExpanded = false;
                    }
                    break;
                case "QuanTri":
                    QuanTriExpanded = !QuanTriExpanded;
                    if (QuanTriExpanded)
                    {
                        QuanLyExpanded = false;
                        TaoLapExpanded = false;
                        KhachHangExpanded = false;
                    }
                    break;
                case "KhachHang":
                    KhachHangExpanded = !KhachHangExpanded;
                    if (KhachHangExpanded)
                    {
                        QuanLyExpanded = false;
                        TaoLapExpanded = false;
                        QuanTriExpanded = false;
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

        public void ShowRightPane(ContentView contentView)
        {
            RightPaneContent = contentView;
            IsRightPaneVisible = true;
        }

        [RelayCommand]
        public void CloseRightPane()
        {
            IsRightPaneVisible = false;
            RightPaneContent = null;
        }
    }
}
