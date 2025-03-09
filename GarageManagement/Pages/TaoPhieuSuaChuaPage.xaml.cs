using APIClassLibrary;
using APIClassLibrary.APIModels;
using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class TaoPhieuSuaChuaPage : ContentPage
{
	public TaoPhieuSuaChuaPage(APIClientService<Car> carservice, 
		APIClientService<TienCong> congservice,
		APIClientService<VatTuPhuTung> vattuservice,
		APIClientService<NoiDungPhieuSuaChua> noidungphieuservice,
		APIClientService<ChiTietTienCong> chitietcongservice,
		APIClientService<PhieuSuaChua> phieuservice)
	{
		InitializeComponent();
		BindingContext = new TaoPhieuSuaChuaPageViewModel(carservice, congservice, vattuservice, noidungphieuservice, chitietcongservice, phieuservice);
	}
}