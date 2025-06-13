using APIClassLibrary.APIModels;
using CommunityToolkit.Maui.Views;
using GarageManagement.ViewModels;
using System.Runtime.CompilerServices;

namespace GarageManagement.Pages;

public partial class MainPage : ContentPage
{
    private readonly MainPageViewModel _viewModel;

    private ChiTietXePage _chiTietXePage;
    private AddNewAccountPage _addNewAccountPage;
    private readonly SuaHieuXePage _suaHieuXePage;
    private readonly ChiTietKhachHangPage _chiTietKhachHangPage;
    private readonly SuaTaiKhoanPage _suaTaiKhoanPage;
    private readonly ChiTietPhieuNhapVatTuPage _chiTietPhieuNhapVatTuPage;
    private readonly ChiTietPhieuSuaChuaPage _chiTietPhieuSuaChuaPage;
    private readonly ChiTietNhanVienPage _chiTietNhanVienPage;

    public MainPage(MainPageViewModel viewModel, 
        ChiTietXePage chiTietXePage, 
        AddNewAccountPage addNewAccountPage,
        SuaHieuXePage suaHieuXePage,
        ChiTietKhachHangPage chiTietKhachHangPage,
        SuaTaiKhoanPage suaTaiKhoanPage,
        ChiTietPhieuNhapVatTuPage chiTietPhieuNhapVatTuPage,
        ChiTietPhieuSuaChuaPage chiTietPhieuSuaChuaPage,
        ChiTietNhanVienPage chiTietNhanVienPage)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
        _chiTietXePage = chiTietXePage;
        _addNewAccountPage = addNewAccountPage;
        _suaHieuXePage = suaHieuXePage;
        _chiTietKhachHangPage = chiTietKhachHangPage;
        _suaTaiKhoanPage = suaTaiKhoanPage;
        _chiTietPhieuNhapVatTuPage = chiTietPhieuNhapVatTuPage;
        _chiTietPhieuSuaChuaPage = chiTietPhieuSuaChuaPage;
        _chiTietNhanVienPage = chiTietNhanVienPage;
    }

    private void OnCollapseClicked(object sender, EventArgs e)
    {
        if (_viewModel.IsCollapsed)
        {
            // Expand
            CollapsibleToolbar.Animate("Expand", new Animation(v => CollapsibleToolbar.WidthRequest = v, 80, 200), 16, 200, Easing.CubicInOut);
            _viewModel.IsCollapsed = false;
        }
        else
        {
            // Collapse
            CollapsibleToolbar.Animate("Collapse", new Animation(v => CollapsibleToolbar.WidthRequest = v, 200, 80), 16, 200, Easing.CubicInOut);
            _viewModel.IsCollapsed = true;
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        MessagingCenter.Subscribe<TiepNhanXePageViewModel, Guid>(
        this, "ShowCarDetails",
        (sender, carId) =>
        {
            if (_viewModel.IsRightPaneVisible)
            {
                _viewModel.CloseRightPane();
                _chiTietXePage._viewModel.ExitEditMode();
            }
            else
            {
                _chiTietXePage.setCarId(carId);
                _ = _chiTietXePage._viewModel.LoadAsync();
                _viewModel.ShowRightPane(_chiTietXePage);
            }
        });
        MessagingCenter.Subscribe<QuanLiPhieuTiepNhanPageViewModel, Guid>(
        this, "ShowCarDetails",
        (sender, carId) =>
        {
            if (_viewModel.IsRightPaneVisible)
            {
                _viewModel.CloseRightPane();
                _chiTietXePage._viewModel.ExitEditMode();   
            }
            else
            {
                _chiTietXePage.setCarId(carId);
                _ = _chiTietXePage._viewModel.LoadAsync();
                _viewModel.ShowRightPane(_chiTietXePage);
            }
        });
        MessagingCenter.Subscribe<QuanLiTaiKhoanPageViewModel>(
        this, "ShowAddNewAccount",
        (sender) =>
        {
            if (_viewModel.IsRightPaneVisible) _viewModel.CloseRightPane();
            else
            {
                _ = _addNewAccountPage._viewModel.LoadAsync();
                _viewModel.ShowRightPane(_addNewAccountPage);
            }
        });
        MessagingCenter.Subscribe<QuanLiDanhSachHieuXePageViewModel, Guid>(
        this, "ShowEditHieuXe",
        (sender, Id) =>
        {
            if (_viewModel.IsRightPaneVisible) _viewModel.CloseRightPane();
            else
            {
                _suaHieuXePage._viewModel.hieuXeID = Id;
                _ = _suaHieuXePage._viewModel.LoadAsync();
                _viewModel.ShowRightPane(_suaHieuXePage);
            }
        });
        MessagingCenter.Subscribe<QuanLiPhieuTiepNhanPageViewModel, Guid>(
        this, "ShowCustomerDetails",
        (sender, Id) =>
        {
            if (_viewModel.IsRightPaneVisible)
            {
                _viewModel.CloseRightPane();
                _=_chiTietKhachHangPage._viewModel.ExitUpdate();
            }
            else
            {
                _chiTietKhachHangPage._viewModel.khachHangId = Id;
                _=_chiTietKhachHangPage._viewModel.LoadAsync();
                _viewModel.ShowRightPane(_chiTietKhachHangPage);
            }
        });
        MessagingCenter.Subscribe<TiepNhanXePageViewModel, Guid>(
        this, "ShowCustomerDetails",
        (sender, Id) =>
        {
            if (_viewModel.IsRightPaneVisible)
            {
                _viewModel.CloseRightPane();
                _ = _chiTietKhachHangPage._viewModel.ExitUpdate();
            }
            else
            {
                _chiTietKhachHangPage._viewModel.khachHangId = Id;
                _ = _chiTietKhachHangPage._viewModel.LoadAsync();
                _viewModel.ShowRightPane(_chiTietKhachHangPage);
            }
        });
        MessagingCenter.Subscribe<QuanLiKhachHangPageViewModel, Guid>(
        this, "ShowCustomerDetails",
        (sender, Id) =>
        {
            if (_viewModel.IsRightPaneVisible)
            {
                _viewModel.CloseRightPane();
                _ = _chiTietKhachHangPage._viewModel.ExitUpdate();
            }
            else
            {
                _chiTietKhachHangPage._viewModel.khachHangId = Id;
                _ = _chiTietKhachHangPage._viewModel.LoadAsync();
                _viewModel.ShowRightPane(_chiTietKhachHangPage);
            }
        });
        MessagingCenter.Subscribe<QuanLiTaiKhoanPageViewModel, Guid>(
            this, "EditAccount", (sender, Id) =>
            {
                if (_viewModel.IsRightPaneVisible)
                {
                    _viewModel.CloseRightPane();
                }
                else
                {
                    _suaTaiKhoanPage._viewModel.taiKhoanId = Id;
                    _ = _suaTaiKhoanPage._viewModel.LoadAsync();
                    _viewModel.ShowRightPane(_suaTaiKhoanPage);
                }
            });
        MessagingCenter.Subscribe<QuanLiPhieuNhapPageViewModel, Guid>(
            this, "ViewChiTietPhieuNhap", (sender, id) =>
            {
                if (_viewModel.IsRightPaneVisible)
                {
                    _viewModel.CloseRightPane();
                }
                else
                {
                    _chiTietPhieuNhapVatTuPage._viewModel.phieuNhapId = id;
                    _= _chiTietPhieuNhapVatTuPage._viewModel.LoadAsync();
                    _viewModel.ShowRightPane(_chiTietPhieuNhapVatTuPage);
                }
            });
        MessagingCenter.Subscribe<QuanLiPhieuSuaChuaPageViewModel, Guid>(
            this, "ViewChiTietPhieuSuaChua", (sender, id) =>
            {
                if (_viewModel.IsRightPaneVisible)
                {
                    _viewModel.CloseRightPane();
                }
                else
                {
                    _chiTietPhieuSuaChuaPage._viewModel.phieuSuaId = id;
                    _= _chiTietPhieuSuaChuaPage._viewModel.LoadAsync();
                    _viewModel.ShowRightPane(_chiTietPhieuSuaChuaPage);
                }
            });
        MessagingCenter.Subscribe<QuanLiNhanVienPageViewModel, Guid>(
            this, "ShowStaffDetails", (sender, id) =>
            {
                if (_viewModel.IsRightPaneVisible)
                {
                    _viewModel.CloseRightPane();
                }
                else
                {
                    _chiTietNhanVienPage._viewModel.id = id;
                    _ = _chiTietNhanVienPage._viewModel.LoadAsync();
                    _viewModel.ShowRightPane(_chiTietNhanVienPage);
                }
            });
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        MessagingCenter.Unsubscribe<TiepNhanXePageViewModel, Guid>(this, "ShowCarDetails");
        MessagingCenter.Unsubscribe<QuanLiPhieuTiepNhanPageViewModel, Guid>(this, "ShowCarDetails");
        MessagingCenter.Unsubscribe<QuanLiTaiKhoanPageViewModel>(this, "ShowAddNewAccount");
        MessagingCenter.Unsubscribe<QuanLiTaiKhoanPageViewModel, Guid>(this, "ShowEditHieuXe");
        MessagingCenter.Unsubscribe<QuanLiPhieuTiepNhanPageViewModel, Guid>(this, "ShowCustomerDetails");
        MessagingCenter.Unsubscribe<TiepNhanXePageViewModel, Guid>(this, "ShowCustomerDetails");
        MessagingCenter.Unsubscribe<QuanLiPhieuNhapPageViewModel, PhieuNhapVatTu>(this, "ViewChiTietPhieuNhap");
        MessagingCenter.Unsubscribe<QuanLiPhieuSuaChuaPageViewModel, Guid>(this, "ViewChiTietPhieuSuaChua");
        MessagingCenter.Unsubscribe<QuanLiNhanVienPageViewModel, Guid>(this, "ShowStaffDetails");
    }
}