using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class ChinhSuaPhieuThuTienPage : ContentPage
{
    private readonly ChinhSuaPhieuThuTienPageViewModel _vm;

    public ChinhSuaPhieuThuTienPage(
        ChinhSuaPhieuThuTienPageViewModel vm,
        Guid phieuId)
    {
        InitializeComponent();
        BindingContext = vm;
        _vm = vm;

        _ = _vm.LoadAsync(phieuId);
    }
}