using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class BaoCaoDoanhSoPage : ContentView
{
	public readonly BaoCaoDoanSoPageViewModel _viewmodel;
	public BaoCaoDoanhSoPage(BaoCaoDoanSoPageViewModel viewmodel)
	{
		_viewmodel = viewmodel;
		BindingContext = viewmodel;
		InitializeComponent();
	}

	private async void OnMonthChanged(object sender, EventArgs e)
	{
		await _viewmodel.OnDateChanged();
	}

	private async void OnYearChanged(object sender, EventArgs e)
	{
		await _viewmodel.OnDateChanged();
	}

	protected override void OnSizeAllocated(double width, double height)
	{
		base.OnSizeAllocated(width, height);
        _ = _viewmodel.LoadAsync();
    }
}