using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class BaoCaoTonPage : ContentView
{
	public readonly BaoCaoTonViewModel _viewModel;
	
	public BaoCaoTonPage(BaoCaoTonViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;
    }

	public void onDateChanged(object sender, EventArgs e)
	{
		_=_viewModel.onDateChanged();
    }
}