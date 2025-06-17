using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class ChangePasswordPage : ContentView
{
	private readonly ChangePasswordPageViewmodel _vm;
	Guid id; 
    public ChangePasswordPage(ChangePasswordPageViewmodel vm, Guid id)
	{
		InitializeComponent();
		BindingContext = _vm = vm;
		this.id = id;
    }
    protected override void OnParentSet()
    {
        base.OnParentSet();
        _vm.id = id;
        _vm.LoadData();
    }

}