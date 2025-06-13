
using APIClassLibrary;
using APIClassLibrary.APIModels;
using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class QuanLiXePage : ContentView
{
	public readonly QuanLiXePageViewModel _quanLiXePageViewModel;
    public QuanLiXePage(APIClientService<Xe> carservice,
		APIClientService<ChiTietPhieuSuaChua> noidungphieuservice,
		APIClientService<PhieuSuaChua> phieuservice,
		QuanLiXePageViewModel quanLiXePageViewModel)
	{
		_quanLiXePageViewModel = quanLiXePageViewModel;
		BindingContext = quanLiXePageViewModel; 
		InitializeComponent();
        MessagingCenter.Subscribe<ChiTietKhachHangViewModel, Guid>(
            this, "ReloadCustomerList", (sender, Id) =>
            {
                _ = _quanLiXePageViewModel.LoadAsync();
            });
        
    }

	protected override void OnSizeAllocated(double width, double height)
	{
		base.OnSizeAllocated(width, height);
		_ = _quanLiXePageViewModel.LoadAsync();
    }
}