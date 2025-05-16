using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class MainPage : ContentPage
{
    private readonly MainPageViewModel _viewModel;

    public MainPage(MainPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
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
}