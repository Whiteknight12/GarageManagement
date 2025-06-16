using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GarageManagement.Pages;

namespace GarageManagement.ViewModels
{
    public partial class SuaHieuXePageViewModel: BaseViewModel
    {
        private readonly APIClientService<HieuXe> _hieuXeService;

        public Guid hieuXeID;
        private string oldTenHieuXe;
        [ObservableProperty]
        private bool isUpdating = false;
        [ObservableProperty]
        private bool isReadOnly = true;
        [ObservableProperty]
        private string tenHieuXe;

        [ObservableProperty]
        private Guid id; 

        public SuaHieuXePageViewModel(APIClientService<HieuXe> hieuXeService)
        {
            _hieuXeService = hieuXeService;
        }

        public async Task LoadAsync()
        {
            var result = await _hieuXeService.GetByID(hieuXeID);
            TenHieuXe = result?.TenHieuXe ?? string.Empty;
            oldTenHieuXe = TenHieuXe;
            IsUpdating = false;
            IsReadOnly = true;
            Id = hieuXeID; 
        }
        [RelayCommand]
        private void ToggleUpdate()
        {
            IsReadOnly = false;
            IsUpdating = true;
        }
        [RelayCommand]
        private void Cancel()
        {
            IsReadOnly = true;
            IsUpdating = false;
            TenHieuXe = oldTenHieuXe;
        }
        [RelayCommand]
        public async Task UpdateHieuXe()
        {
            if (string.IsNullOrEmpty(TenHieuXe))
            {
                await Shell.Current.DisplayAlert("Lỗi", "Tên hiệu xe không được để trống.", "OK");
                return;
            }
            if (TenHieuXe == oldTenHieuXe)
            {
                await Shell.Current.DisplayAlert("Thông báo", "Không có thay đổi nào được thực hiện.", "OK");
                return;
            }
            var hxs = await _hieuXeService.GetAll(); 
            if(hxs.Any(hx=>hx.TenHieuXe == TenHieuXe))
            {
                await Shell.Current.DisplayAlert("Thông báo", "Đã tồn tại, nhập tên khác", "OK");
                return;
            }
            var hieuXe=await _hieuXeService.GetByID(hieuXeID);
            if (hieuXe is null)
            {
                await Shell.Current.DisplayAlert("Thông báo", "Hiệu xe không tồn tại", "OK");
                return;
            }
            hieuXe.TenHieuXe = TenHieuXe;
            await _hieuXeService.Update(hieuXe);
            var toast = Toast.Make("Cập nhật hiệu xe thành công", CommunityToolkit.Maui.Core.ToastDuration.Short);
            await LoadAsync();
            MessagingCenter.Send(this, "HieuXeUpdated");
            await toast.Show();
        }
    }
}
