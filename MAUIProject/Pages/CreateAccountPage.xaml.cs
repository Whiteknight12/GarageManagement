using MAUIProject.ViewModels;

namespace MAUIProject.Pages;

public partial class CreateAccountPage : ContentPage
{
	private readonly UserViewModel _viewModel;
    public CreateAccountPage(UserViewModel viewmodel)
	{
		InitializeComponent();
        BindingContext = viewmodel;
        _viewModel = viewmodel;
    }
    public void CreateAccount(Object sender, EventArgs e)
    {
        
    }
}