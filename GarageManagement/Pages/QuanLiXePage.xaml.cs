using APIClassLibrary;
using APIClassLibrary.APIModels;
using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class QuanLiXePage : ContentPage
{
	public QuanLiXePage(APIClientService<Xe> carservice,
		APIClientService<ChiTietPhieuSuaChua> noidungphieuservice,
		APIClientService<PhieuSuaChua> phieuservice)
	{
		BindingContext = new QuanLiXePageViewModel(carservice, phieuservice, noidungphieuservice);
		InitializeComponent();
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
		if (BindingContext is QuanLiXePageViewModel vm) vm.LoadAsync();
    }
}