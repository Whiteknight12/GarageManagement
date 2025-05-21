using CommunityToolkit.Maui.Views;

namespace GarageManagement.Pages;

public partial class TaskBarChoicePopup : Popup
{
	public TaskBarChoicePopup()
	{
		InitializeComponent();
	}
    private void OnCloseClicked(object sender, EventArgs e)
    {
        Close(); // Đóng popup
    }
}