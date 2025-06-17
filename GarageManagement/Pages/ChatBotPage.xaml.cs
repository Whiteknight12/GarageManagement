using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class ChatBotPage : ContentView
{
	public readonly ChatBotPageViewmodel _viewModel;

	public ChatBotPage(ChatBotPageViewmodel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;
	}
}