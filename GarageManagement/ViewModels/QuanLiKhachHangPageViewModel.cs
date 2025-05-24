using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GarageManagement.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageManagement.ViewModels
{
    public partial class QuanLiKhachHangPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<KhachHang> listKhachHang = new();
        [ObservableProperty]
        private bool isDeleteMode;
        private readonly APIClientService<KhachHang> _khachHangService;
        private readonly AuthenticationService _authenticationService;

        public QuanLiKhachHangPageViewModel(APIClientService<KhachHang> khachHangService,
            ILogger<QuanLiKhachHangPageViewModel> logger,
            AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _khachHangService = khachHangService;
            _ = LoadAsync();
            IsDeleteMode = false;
        }

        public async Task LoadAsync()
        {
            _ = _authenticationService.FettaiKhoanSession();
            var httpClient = _khachHangService.GetHttpClient;
            var token = _authenticationService.GetCurrentAccountStatus.Token;
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var list = await _khachHangService.GetAll();

            foreach (var kh in list)
            {
                kh.STT = list.IndexOf(kh) + 1;
            }
            ListKhachHang = new ObservableCollection<KhachHang>(list);
        }

        public void Load(KhachHang item)
        {
            ListKhachHang.Add(item);
            // Cập nhật STT cho tất cả khách hàng
            for (int i = 0; i < ListKhachHang.Count; i++)
            {
                ListKhachHang[i].STT = i + 1;
            }
        }

        [RelayCommand]
        private void Add()
        {
            // Logic để thêm khách hàng mới (có thể mở một trang mới để nhập thông tin)
        }

        [RelayCommand]
        private void Edit()
        {
            // Logic để sửa khách hàng (có thể mở một trang để chỉnh sửa thông tin khách hàng được chọn)
        }

        [RelayCommand]
        private void ToggleDeleteMode()
        {
            IsDeleteMode = !IsDeleteMode;
            if (!IsDeleteMode) // Khi hủy chế độ xóa
            {
                var updatedList = new List<KhachHang>(ListKhachHang);
                foreach (var kh in updatedList)
                {
                    kh.IsSelected = false;
                }
                // Gán lại ListKhachHang để buộc giao diện làm mới
                ListKhachHang = new ObservableCollection<KhachHang>(updatedList);
            }
        }

        [RelayCommand]
        private async Task Delete()
        {
            var selectedItems = ListKhachHang.Where(x => x.IsSelected).ToList();
            foreach (var item in selectedItems)
            {
                await _khachHangService.Delete(item.Id);
                ListKhachHang.Remove(item);
            }
            // Cập nhật STT sau khi xóa
            for (int i = 0; i < ListKhachHang.Count; i++)
            {
                ListKhachHang[i].STT = i + 1;
            }
            IsDeleteMode = false;
        }

        [RelayCommand]
        private async Task GoToChiTietKhachHangPage(Guid id)
        {
            await Shell.Current.GoToAsync($"ChiTietKhachHangPage?id={id}");
        }

    }
}
