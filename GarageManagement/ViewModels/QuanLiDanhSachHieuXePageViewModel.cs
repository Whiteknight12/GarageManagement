using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GarageManagement.Pages;
using GarageManagement.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageManagement.ViewModels
{
    public partial class QuanLiDanhSachHieuXePageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<HieuXe> listHieuXe = new();
        [ObservableProperty]
        private bool isDeleteMode;
        private readonly APIClientService<HieuXe> _hieuXeService;
        private readonly ThemHieuXePageViewModel _themHieuXePageViewModel;
        private readonly AuthenticationService _authenticationService;
        public QuanLiDanhSachHieuXePageViewModel(APIClientService<HieuXe> hieuXeService,
            ThemHieuXePageViewModel themHieuXePageViewModel,
            ILogger<QuanLiDanhSachHieuXePageViewModel> logger,
            AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _hieuXeService = hieuXeService;
            _ = LoadAsync();
            _themHieuXePageViewModel = themHieuXePageViewModel;
            IsDeleteMode = false;
            
        }

        public async Task LoadAsync()
        {
            _ = _authenticationService.FettaiKhoanSession();
            var httpClient = _hieuXeService.GetHttpClient;
            var token = _authenticationService.GetCurrentAccountStatus.Token;
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var list = await _hieuXeService.GetAll();
            Console.WriteLine(list[0].TenHieuXe);
            ListHieuXe = new ObservableCollection<HieuXe>(list);
        }
        public void Load(HieuXe item)
        {
            ListHieuXe.Add(item);
        }
        [RelayCommand]
        private void Add()
        {
            var newWindow = new Window
            {
                Page = new ThemHieuXePage(_themHieuXePageViewModel),
                MaximumHeight = 300, 
                MaximumWidth = 300,
                MinimumHeight = 300,
                MinimumWidth = 300
            };
            _themHieuXePageViewModel.OnHieuXeAdded = (HieuXe hieuXe) =>
            {
                Load(hieuXe);
            };
            Application.Current.OpenWindow(newWindow);
        }
        [RelayCommand]
        private void Edit()
        {

        }
        [RelayCommand]
        private void ToggleDeleteMode()
        {
            IsDeleteMode = !IsDeleteMode;
        }
        [RelayCommand]
        private void Delete()
        {
            var selectedItems = ListHieuXe.Where(x => x.IsSelected).ToList();
            foreach (var item in selectedItems)
            {
                _ = _hieuXeService.Delete(item.Id);
                ListHieuXe.Remove(item);
            }
            
            IsDeleteMode = false; // Thoát chế độ xóa sau khi xóa xong

        }

    }
}
