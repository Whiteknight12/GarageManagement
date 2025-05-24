using APIClassLibrary.APIModels;
using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class AddNewAccountPage : ContentView
{
    public AddNewAccountPageViewModel _viewModel;

    public AddNewAccountPage(AddNewAccountPageViewModel viewModel)
    {
        _viewModel = viewModel;
        BindingContext = _viewModel;
        InitializeComponent();
    }

    public void OnUserSelected(object sender, EventArgs e)
    {
        if (sender is not Picker picker) return;
        if (picker.SelectedItem is not UserForUI user) return;
        _viewModel.IsSelectingUser = true;
        _viewModel.HoVaTen = user.HoVaTen ?? "";
        _viewModel.CCCD = user.CCCD ?? "";
        _viewModel.PhoneNumber = user.SDT ?? "";
    }
}