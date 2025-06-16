

using GarageManagement.ViewModels;

namespace GarageManagement.Pages;
[QueryProperty(nameof(StaffIdString), "parameterID")]
public partial class ChiTietNhanVienPage : ContentView
{
    string _staffIdString;
    public string StaffIdString
    {
        get => _staffIdString;
        set
        {
            _staffIdString = value;
            if (Guid.TryParse(value, out var g)) _viewModel.Id = g;
        }
    }
    public readonly ChiTietNhanVienPageViewModel _viewModel;
    public ChiTietNhanVienPage(ChiTietNhanVienPageViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
    protected override void OnParentSet()
    {
        base.OnParentSet();

        // Khi ContentView bị remove khỏi UI tree
        if (Parent == null)
        {
            MessagingCenter.Unsubscribe<ChiTietNhanVienPageViewModel>(
                this, "ReloadNhanVienList");
        }
    }
}