using APIClassLibrary;
using APIClassLibrary.APIModels;
using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

[QueryProperty(nameof(CarIDString), "parameterID")]
public partial class ChiTietXePage : ContentPage
{
    string _carIdString;
    public string CarIDString
    {
        get => _carIdString;
        set
        {
            _carIdString = value;
            if (Guid.TryParse(value, out var g)) _viewModel.CarId = g;
        }
    }

    private readonly ChiTietXePageViewModel _viewModel;
    private readonly APIClientService<Xe> _carservice;
    private readonly APIClientService<HieuXe> _hieuxeservice;
    private readonly APIClientService<KhachHang> _khachhangservice;

    public ChiTietXePage(APIClientService<Xe> carservice, 
        APIClientService<HieuXe> hieuxeservice, 
        APIClientService<KhachHang> khachhangservice,
        ChiTietXePageViewModel viewModel)
    {
        _carservice = carservice;
        _hieuxeservice = hieuxeservice;
        _khachhangservice = khachhangservice;
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        _ = _viewModel.LoadAsync();
    }
}