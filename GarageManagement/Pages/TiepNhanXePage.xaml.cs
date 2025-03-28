using APIClassLibrary;
using APIClassLibrary.APIModels;
using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class TiepNhanXePage : ContentPage
{
	public TiepNhanXePage(APIClientService<CarRecord> service, APIClientService<RuleVariable> ruleservice, 
		APIClientService<Car> carservice,
		APIClientService<HieuXe> hieuxeservice,
		APIClientService<User> userservice)
	{
		InitializeComponent();
		BindingContext = new TiepNhanXePageViewModel(service, ruleservice, carservice, hieuxeservice, userservice);
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
		if (BindingContext is TiepNhanXePageViewModel viewmodel) viewmodel.LoadAsync();
    }
}