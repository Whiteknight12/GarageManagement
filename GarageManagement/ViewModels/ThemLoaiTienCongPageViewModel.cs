using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GarageManagement.Pages;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GarageManagement.ViewModels
{
    public partial class ThemLoaiTienCongPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string tenLoaiTienCong = string.Empty;

        [ObservableProperty]
        private string donGiaLoaiTienCong = string.Empty;

        private readonly APIClientService<TienCong> _tienCongService;

        public delegate void OnTienCongAddedDelegate(TienCong tienCong);
        public OnTienCongAddedDelegate OnTienCongAdded { get; set; }

        public ThemLoaiTienCongPageViewModel(APIClientService<TienCong> tienCongService)
        {
            _tienCongService = tienCongService;
        }

        [RelayCommand]
        private async Task Save()   
        {
            if (!double.TryParse(DonGiaLoaiTienCong, out double donGia))
            {
                await Toast.Make("Đơn giá không hợp lệ", CommunityToolkit.Maui.Core.ToastDuration.Short).Show();
                return;
            }
            var listTC = await _tienCongService.GetAll();
            if (listTC is not null)
            {
                foreach (var item in listTC)
                {
                    if (item.TenLoaiTienCong==TenLoaiTienCong)
                    {
                        var newToast = Toast.Make("Tên loại tiền công bị trùng", CommunityToolkit.Maui.Core.ToastDuration.Short);
                        await newToast.Show();
                        return;
                    }
                }
            }
            var newTienCong = await _tienCongService.Create(new TienCong
            {
                Id = Guid.NewGuid(),
                TenLoaiTienCong = TenLoaiTienCong,
                DonGiaLoaiTienCong = donGia,
                NoiDungSuaChuaId = Guid.Empty // Có thể thay bằng ID thực tế nếu có
            });

            var toast = Toast.Make("Thêm loại tiền công mới thành công", CommunityToolkit.Maui.Core.ToastDuration.Short);
            _ = toast.Show();

            TenLoaiTienCong = string.Empty;
            DonGiaLoaiTienCong = string.Empty;

            OnTienCongAdded?.Invoke(newTienCong);
        }

        [RelayCommand]
        private void Cancel()
        {
            var window = Application.Current.Windows.FirstOrDefault(w => w.Page is ThemLoaiTienCongPage);
            if (window != null)
                Application.Current.CloseWindow(window);
        }
    }
}
