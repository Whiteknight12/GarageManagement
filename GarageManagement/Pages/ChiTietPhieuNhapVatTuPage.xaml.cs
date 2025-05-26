using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class ChiTietPhieuNhapVatTuPage : ContentView
{
	public ChiTietPhieuNhapVatTuPageViewModel _viewModel;
	
	public ChiTietPhieuNhapVatTuPage(ChiTietPhieuNhapVatTuPageViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
        BindingContext=_viewModel;
    }
}