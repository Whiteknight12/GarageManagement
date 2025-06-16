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
using System.Windows.Input;

namespace GarageManagement.ViewModels
{
    public partial class QuanLiDanhSachHieuXePageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<HieuXe> listHieuXe = new();

        [ObservableProperty]
        private HieuXe selectedHieuXe;

        [ObservableProperty]
        private bool isDetailPaneVisible;

        public ICommand OpenHieuXeDetailCommand => new Command<HieuXe>(xe =>
        {
            SelectedHieuXe = xe;
            IsDetailPaneVisible = true;
        });

        [RelayCommand]
        private void SuaHieuXe(HieuXe xe)
        {
            if (xe == null) return;
            SelectedHieuXe = xe;
            // Đây là pane sửa cũ, sẽ bind SelectedHieuXe để hiển thị detail
            MessagingCenter.Send(this, "ShowEditHieuXe", xe.Id);
        }


        [ObservableProperty]
        private bool isDeleteMode;

        private readonly APIClientService<HieuXe> _hieuXeService;
        private readonly ThemHieuXePageViewModel _themHieuXePageViewModel;
        private readonly AuthenticationService _authenticationService;

        int count = 0;

        public QuanLiDanhSachHieuXePageViewModel(APIClientService<HieuXe> hieuXeService,
            ThemHieuXePageViewModel themHieuXePageViewModel,
            ILogger<QuanLiDanhSachHieuXePageViewModel> logger,
            AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _hieuXeService = hieuXeService;
            //_ = LoadAsync();
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
            count = 0;
            ListHieuXe = new ObservableCollection<HieuXe>(list);
            IndexCalculate(ListHieuXe);
        }

        public void Load(HieuXe item)
        {
            ListHieuXe.Add(item);
            IndexCalculate(ListHieuXe);
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

        //[RelayCommand]
        //public void SuaHieuXe(Guid Id)
        //{
        //    MessagingCenter.Send(this, "ShowEditHieuXe", Id);
        //}

        [RelayCommand]
        private void ToggleDeleteMode()
        {
            IsDeleteMode = !IsDeleteMode;
            if (IsDeleteMode == false)
            {
                var updatedList = new List<HieuXe>(ListHieuXe);
                foreach (var nv in updatedList)
                {
                    nv.IsSelected = false;
                }
                ListHieuXe = new ObservableCollection<HieuXe>(updatedList);
                IndexCalculate(ListHieuXe);
            }
        }

        [RelayCommand]
        private async Task Delete()
        {
            var selectedItems = ListHieuXe.Where(x => x.IsSelected).ToList();
            foreach (var item in selectedItems)
            {
                await _hieuXeService.Delete(item.Id);
                ListHieuXe.Remove(item);
            }
            IndexCalculate(ListHieuXe);
            IsDeleteMode = false; // Thoát chế độ xóa sau khi xóa xong
        }

        private void IndexCalculate(ObservableCollection<HieuXe> listHieuXe)
        {
            int c = 0;
            foreach (var item in ListHieuXe)
            {
                item.IdForUI = c++;
                item.OnPropertyChanged(nameof(item.IdForUI));
            }
        }
    }
}
