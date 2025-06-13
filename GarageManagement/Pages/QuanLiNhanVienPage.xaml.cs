using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class QuanLiNhanVienPage : ContentView
{
	public readonly QuanLiNhanVienPageViewModel _viewModel; 
    public QuanLiNhanVienPage(QuanLiNhanVienPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		_viewModel = viewModel;

        MessagingCenter.Subscribe<ChiTietNhanVienPageViewModel>(this, "ShowStaffDetails", async (sender) =>
        {
            await _viewModel.LoadAsync();
        });

    }
    
}