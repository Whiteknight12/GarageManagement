using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using GarageManagement.Services;
using System.Text.Json;

namespace GarageManagement.ViewModels
{
    public partial class NhanSuMainPageViewModel: BaseViewModel
    {
        [ObservableProperty]
        private string tenNguoiDung;

        private readonly APIClientService<NhanVien> _nhanVienService;
        private readonly AuthenticationService _authenticationServices;

        public NhanSuMainPageViewModel(APIClientService<NhanVien> nhanVienService, AuthenticationService authenticationService)
        {
            _nhanVienService=nhanVienService;
            _authenticationServices=authenticationService;
        }

        public async Task LoadAsync()
        {
            _ = _authenticationServices.FettaiKhoanSession();
            var currentAccount = _authenticationServices.GetCurrentAccountStatus;
            var currentAccountId = currentAccount?.AccountId ?? Guid.Empty;
            var result = await _nhanVienService.GetThroughtSpecialRoute("TaiKhoanId", currentAccountId.ToString());
            TenNguoiDung = result?.HoTen ?? "";
        }
    }
}
