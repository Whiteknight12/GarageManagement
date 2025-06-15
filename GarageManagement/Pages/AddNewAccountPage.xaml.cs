using APIClassLibrary;
using APIClassLibrary.APIModels;
using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class AddNewAccountPage : ContentView
{
    public AddNewAccountPageViewModel _viewModel;

    private readonly APIClientService<TaiKhoan> _taiKhoanService;

    public AddNewAccountPage(AddNewAccountPageViewModel viewModel,
        APIClientService<TaiKhoan> taiKhoanService)
    {
        _viewModel = viewModel;
        _taiKhoanService = taiKhoanService;
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

    public async void OnUsernameChanged(object sender, EventArgs e)
    {
        if (sender is Entry entry)
        {
            var listTK = await _taiKhoanService.GetAll();
            foreach (var item in listTK)
            {
                if (item.TenDangNhap==entry.Text)
                {
                    await Shell.Current.DisplayAlert("Thông báo", "Tên đăng nhập đã tồn tại", "OK");
                    return;
                }
            }
        }
    }
}