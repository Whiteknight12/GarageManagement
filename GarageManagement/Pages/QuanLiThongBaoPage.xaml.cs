using APIClassLibrary.APIModels;
using GarageManagement.ViewModels;
using System.Diagnostics.Contracts;

namespace GarageManagement.Pages;

public partial class QuanLiThongBaoPage : ContentView
{
	public readonly QuanLiThongBaoPageViewModel _viewModel;
	
	public QuanLiThongBaoPage(QuanLiThongBaoPageViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext= _viewModel;
	}

    protected override void OnParentSet()
    {
        base.OnParentSet();
        MessagingCenter.Subscribe<TaoThongBaoPageViewModel>(this, "NewThongBaoCreated", async (sender) =>
        {
            await _viewModel.LoadAsync();
        });
        MessagingCenter.Subscribe<ChiTietThongBaoPageViewModel>(this, "NewThongBaoCreated", async (sender) =>
        {
            await _viewModel.LoadAsync();
        });
    }
}