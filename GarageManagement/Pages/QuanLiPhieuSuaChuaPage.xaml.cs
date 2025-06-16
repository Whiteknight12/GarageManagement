using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class QuanLiPhieuSuaChuaPage : ContentView
{
    public readonly QuanLiPhieuSuaChuaPageViewModel _viewModel;
    public QuanLiPhieuSuaChuaPage(QuanLiPhieuSuaChuaPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    protected override void OnParentSet()
    {
        base.OnParentSet();

        if (Parent != null)
        {
            MessagingCenter.Subscribe<SuaPhieuSuaChuaPageViewModel, Guid>(this, "ReloadPhieuSuaChuaList", (sender, id) =>
            {
                _ = _viewModel.LoadAsync();
                MessagingCenter.Send(this, "ViewChiTietPhieuSuaChua", id);
            });
        }
        else
        {
            MessagingCenter.Unsubscribe<SuaPhieuSuaChuaPageViewModel>(this, "ReloadPhieuSuaChuaList");
        }
    }
}