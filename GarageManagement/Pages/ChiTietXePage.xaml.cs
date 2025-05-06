using APIClassLibrary;
using APIClassLibrary.APIModels;
using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

[QueryProperty(nameof(CarID), "parameterID")]
public partial class ChiTietXePage : ContentPage
{
    public Guid CarID { get; set; }
    private readonly APIClientService<Xe> _carservice;
    private readonly APIClientService<HieuXe> _hieuxeservice;
    private readonly APIClientService<KhachHang> _khachhangservice;
    public ChiTietXePage(APIClientService<Xe> carservice, APIClientService<HieuXe> hieuxeservice, APIClientService<KhachHang> khachhangservice)
    {
        _carservice = carservice;
        _hieuxeservice = hieuxeservice;
        _khachhangservice = khachhangservice;
        InitializeComponent();
    }
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        if (CarID != null)
        {
            BindingContext = new ChiTietXePageViewModel(CarID, _carservice, _hieuxeservice, _khachhangservice);
        }
    }
}