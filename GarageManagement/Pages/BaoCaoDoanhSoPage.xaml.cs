using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class BaoCaoDoanhSoPage : ContentPage
{
	private readonly BaoCaoDoanSoPageViewModel _viewmodel;
	public BaoCaoDoanhSoPage(BaoCaoDoanSoPageViewModel viewmodel)
	{
		_viewmodel = viewmodel;
		BindingContext = viewmodel;
		InitializeComponent();
	}

	private async void OnMonthChanged(object sender, EventArgs e)
	{
		await _viewmodel.OnMonthChanged();
	}

	private async void OnYearChanged(object sender, EventArgs e)
	{
		await _viewmodel.OnYearChanged();
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
		if (BindingContext is BaoCaoDoanSoPageViewModel vm) vm.LoadAsync();
    }
}