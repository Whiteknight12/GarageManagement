using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.Json;

namespace GarageManagement.ViewModels
{
    public partial class NhanSuMainPageViewModel: BaseViewModel
    {
        [ObservableProperty]
        private string tenNguoiDung;

        private readonly APIClientService<NhanVien> _nhanVienService;
        private string STORAGE_KEY = "user-account-status";

        public NhanSuMainPageViewModel(APIClientService<NhanVien> nhanVienService)
        {
            _nhanVienService=nhanVienService;
        }

        public async Task LoadAsync()
        {
            var json = await SecureStorage.GetAsync(STORAGE_KEY);
            var currentAccount = JsonSerializer.Deserialize<taiKhoanSession>(json);
            var currentAccountId = currentAccount?.AccountId ?? Guid.Empty;
            var result = await _nhanVienService.GetThroughtSpecialRoute("TaiKhoanId", currentAccountId.ToString());
            TenNguoiDung = result?.HoTen ?? "";
        }
    }
}
