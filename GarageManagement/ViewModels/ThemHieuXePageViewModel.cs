using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GarageManagement.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageManagement.ViewModels
{
    public partial class ThemHieuXePageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string tenHieuXe = string.Empty;
        private readonly APIClientService<HieuXe> _hieuXeService;
        public delegate void OnHieuXeAddedDelegate(HieuXe hieuxe);
        public OnHieuXeAddedDelegate OnHieuXeAdded { get; set; }
        public ThemHieuXePageViewModel(APIClientService<HieuXe> hieuXeService)
        {
            _hieuXeService = hieuXeService; 
        }

        [RelayCommand]
        private async Task Save()
        {
            var newHieuXe = await _hieuXeService.Create(new HieuXe
            {
                Id = Guid.NewGuid(),
                TenHieuXe = TenHieuXe
            });
            var toast = Toast.Make("Thêm hiệu xe mới thành công", CommunityToolkit.Maui.Core.ToastDuration.Short);
            _ = toast.Show();
            TenHieuXe = string.Empty;
            OnHieuXeAdded(newHieuXe);
        }
        [RelayCommand]
        private void Cancel()
        {
            var window = Application.Current.Windows.FirstOrDefault(w => w.Page is ThemHieuXePage);
            if (window != null) Application.Current.CloseWindow(window);
        }
    }
}
