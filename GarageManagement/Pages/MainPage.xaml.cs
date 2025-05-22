using CommunityToolkit.Maui.Views;
using GarageManagement.ViewModels;
using System.Runtime.CompilerServices;

namespace GarageManagement.Pages;

public partial class MainPage : ContentPage
{
    private readonly MainPageViewModel _viewModel;

    private ChiTietXePage _chiTietXePage;

    public MainPage(MainPageViewModel viewModel, ChiTietXePage chiTietXePage)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
        _chiTietXePage = chiTietXePage;
    }

    private void OnCollapseClicked(object sender, EventArgs e)
    {
        if (_viewModel.IsCollapsed)
        {
            // Expand
            CollapsibleToolbar.Animate("Expand", new Animation(v => CollapsibleToolbar.WidthRequest = v, 80, 200), 16, 200, Easing.CubicInOut);
            _viewModel.IsCollapsed = false;
        }
        else
        {
            // Collapse
            CollapsibleToolbar.Animate("Collapse", new Animation(v => CollapsibleToolbar.WidthRequest = v, 200, 80), 16, 200, Easing.CubicInOut);
            _viewModel.IsCollapsed = true;
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        MessagingCenter.Subscribe<TiepNhanXePageViewModel, Guid>(
        this, "ShowCarDetails",
        (sender, carId) =>
        {
            _chiTietXePage.setCarId(carId);
            _viewModel.ShowRightPane(_chiTietXePage);
        });
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        MessagingCenter.Unsubscribe<TiepNhanXePageViewModel, Guid>(this, "ShowCarDetails");
    }
}