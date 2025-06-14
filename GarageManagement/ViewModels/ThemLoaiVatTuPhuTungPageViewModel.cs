using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GarageManagement.Pages;
using System;
using System.Threading.Tasks;

namespace GarageManagement.ViewModels
{
    public partial class ThemLoaiVatTuPhuTungPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string tenLoaiVatTuPhuTung = string.Empty;

        [ObservableProperty]
        private string soLuong = string.Empty;

        [ObservableProperty]
        private string donGiaBanLoaiVatTuPhuTung = string.Empty;

        private readonly APIClientService<VatTuPhuTung> _vatTuPhuTungService;

        public delegate void OnVatTuPhuTungAddedDelegate(VatTuPhuTung vatTu);
        public OnVatTuPhuTungAddedDelegate OnVatTuPhuTungAdded { get; set; }

        public ThemLoaiVatTuPhuTungPageViewModel(APIClientService<VatTuPhuTung> vatTuPhuTungService)
        {
            _vatTuPhuTungService = vatTuPhuTungService;
        }

        [RelayCommand]
        private async Task Save()
        {
            try
            {
                if (!int.TryParse(SoLuong, out int soLuongValue) || !double.TryParse(DonGiaBanLoaiVatTuPhuTung, out double donGiaValue))
                {
                    await Toast.Make("Số lượng hoặc đơn giá không hợp lệ", CommunityToolkit.Maui.Core.ToastDuration.Short).Show();
                    return;
                }

                var newVatTu = await _vatTuPhuTungService.Create(new VatTuPhuTung
                {
                    VatTuPhuTungId = Guid.NewGuid(),
                    TenLoaiVatTuPhuTung = TenLoaiVatTuPhuTung,
                    SoLuong = soLuongValue,
                    DonGiaBanLoaiVatTuPhuTung = donGiaValue
                });

                var toast = Toast.Make("Thêm loại vật tư phụ tùng mới thành công", CommunityToolkit.Maui.Core.ToastDuration.Short);
                _ = toast.Show();

                TenLoaiVatTuPhuTung = string.Empty;
                SoLuong = string.Empty;
                DonGiaBanLoaiVatTuPhuTung = string.Empty;

                OnVatTuPhuTungAdded?.Invoke(newVatTu);

                // Đóng window sau khi lưu thành công
                var window = Application.Current.Windows.FirstOrDefault(w => w.Page is ThemLoaiVatTuPhuTungPage);
                if (window != null)
                {
                    Application.Current.CloseWindow(window);
                }
            }
            catch (Exception ex)
            {
                await Toast.Make($"Lỗi: {ex.Message}", CommunityToolkit.Maui.Core.ToastDuration.Short).Show();
            }
        }

        [RelayCommand]
        private void Cancel()
        {
            var window = Application.Current.Windows.FirstOrDefault(w => w.Page is ThemLoaiVatTuPhuTungPage);
            if (window != null)
                Application.Current.CloseWindow(window);
        }
    }
}