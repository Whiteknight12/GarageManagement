using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class QuanLiTaiKhoanPage : ContentView
{
    public readonly QuanLiTaiKhoanPageViewModel _viewModel;

    public QuanLiTaiKhoanPage(QuanLiTaiKhoanPageViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
        MessagingCenter.Subscribe<AddNewAccountPageViewModel>(this, "ReloadAccountList", async (sender) =>
        {
            await _viewModel.LoadAsync();
        });
        MessagingCenter.Subscribe<SuaTaiKhoanPageViewModel>(this, "UpdateAccountList", async (sender) =>
        {
            await _viewModel.LoadAsync();
        });
    }
}
