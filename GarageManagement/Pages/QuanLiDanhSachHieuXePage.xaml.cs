using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class QuanLiDanhSachHieuXePage : ContentView
{
	public readonly QuanLiDanhSachHieuXePageViewModel _viewModel; 

	public QuanLiDanhSachHieuXePage(QuanLiDanhSachHieuXePageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		_viewModel = viewModel;
        MessagingCenter.Subscribe<SuaHieuXePageViewModel>(this,
        "HieuXeUpdated",
        async (sender) => {
            await _viewModel.LoadAsync();
        });
    }

    protected override void OnParentChanged()
    {
        base.OnParentChanged();
        if (Parent is null) MessagingCenter.Unsubscribe<ThemHieuXePageViewModel>(this,"HieuXeUpdated");
    }
}