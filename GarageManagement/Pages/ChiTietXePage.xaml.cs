using APIClassLibrary;
using APIClassLibrary.APIModels;
using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

[QueryProperty(nameof(CarID), "parameterID")]
public partial class ChiTietXePage : ContentPage
{
    public string CarID { get; set; }
    private readonly APIClientService<Car> _carservice;
    public ChiTietXePage(APIClientService<Car> carservice)
    {
        _carservice = carservice;
        InitializeComponent();
    }
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        if (!string.IsNullOrEmpty(CarID))
        {
            BindingContext = new ChiTietXePageViewModel(CarID, _carservice);
        }
    }
}