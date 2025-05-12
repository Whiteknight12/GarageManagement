using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class LapPhieuNhapPage : ContentPage
{
	public LapPhieuNhapPage(LapPhieuNhapPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}