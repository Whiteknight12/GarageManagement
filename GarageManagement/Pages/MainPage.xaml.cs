﻿using CommunityToolkit.Maui.Views;
using GarageManagement.ViewModels;
using System.Runtime.CompilerServices;

namespace GarageManagement.Pages;

public partial class MainPage : ContentPage
{
    private readonly MainPageViewModel _viewModel;

    private ChiTietXePage _chiTietXePage;
    private AddNewAccountPage _addNewAccountPage;
    private readonly SuaHieuXePage _suaHieuXePage;
    
    public MainPage(MainPageViewModel viewModel, 
        ChiTietXePage chiTietXePage, 
        AddNewAccountPage addNewAccountPage,
        SuaHieuXePage suaHieuXePage)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
        _chiTietXePage = chiTietXePage;
        _addNewAccountPage = addNewAccountPage;
        _suaHieuXePage = suaHieuXePage;
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
            if (_viewModel.IsRightPaneVisible) _viewModel.CloseRightPane();
            else
            {
                _chiTietXePage.setCarId(carId);
                _=_chiTietXePage._viewModel.LoadAsync();
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
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        MessagingCenter.Unsubscribe<TiepNhanXePageViewModel, Guid>(this, "ShowCarDetails");
        MessagingCenter.Unsubscribe<QuanLiTaiKhoanPageViewModel>(this, "ShowAddNewAccount");
        MessagingCenter.Unsubscribe<QuanLiTaiKhoanPageViewModel, Guid>(this, "ShowEditHieuXe");
    }
}