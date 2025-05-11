using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class ThemXePage : ContentPage
{
    private readonly ThemXePageViewModel _viewModel;
    public ThemXePage(ThemXePageViewModel viewModel)
	{
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;
        base.OnNavigatedTo(args);
        _=_viewModel.LoadAsync();
    }
}