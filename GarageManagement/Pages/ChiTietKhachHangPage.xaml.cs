using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

[QueryProperty(nameof(CustomerIDString), "parameterID")]
public partial class ChiTietKhachHangPage : ContentPage
{
    string _customerIdString;
    public string CustomerIDString 
    {
        get => _customerIdString;
        set
        {
            _customerIdString = value;
            if (Guid.TryParse(value, out var g)) _viewModel.khachHangId = g;
        }
    }

    private readonly ChiTietKhachHangViewModel _viewModel;

    public ChiTietKhachHangPage(ChiTietKhachHangViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        _viewModel.LoadAsync();
    }
}