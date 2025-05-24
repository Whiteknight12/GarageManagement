using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GarageManagement.Services;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;

namespace GarageManagement.ViewModels
{   
    public partial class QuanLiNhanVienPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<NhanVien> listNhanVien = new();
        [ObservableProperty]
        private bool isDeleteMode;
        [ObservableProperty]
        private int stt;
        private readonly APIClientService<NhanVien> _nhanVienService;
        //private readonly ThemNhanVienPageViewModel _themNhanVienPageViewModel;
        private readonly AuthenticationService _authenticationService;

        public QuanLiNhanVienPageViewModel(APIClientService<NhanVien> nhanVienService,
            //ThemNhanVienPageViewModel themNhanVienPageViewModel,
            ILogger<QuanLiNhanVienPageViewModel> logger,
            AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _nhanVienService = nhanVienService;
            //_themNhanVienPageViewModel = themNhanVienPageViewModel;
            _ = LoadAsync();
            IsDeleteMode = false;
        }

        public async Task LoadAsync()
        {
            _ = _authenticationService.FettaiKhoanSession();
            var httpClient = _nhanVienService.GetHttpClient;
            var token = _authenticationService.GetCurrentAccountStatus.Token;
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var list = await _nhanVienService.GetAll();
            
            foreach (var nv in list)
            {
                nv.STT = list.IndexOf(nv) + 1; 
            }
            ListNhanVien = new ObservableCollection<NhanVien>(list);
        }

        public void Load(NhanVien item)
        {
            ListNhanVien.Add(item);
        }

        [RelayCommand]
        private void Add()
        {
            //var newWindow = new Window
            //{
            //    Page = new ThemNhanVienPage(_themNhanVienPageViewModel),
            //    MaximumHeight = 400,
            //    MaximumWidth = 400,
            //    MinimumHeight = 400,
            //    MinimumWidth = 400
            //};
            //_themNhanVienPageViewModel.OnNhanVienAdded = (NhanVien nhanVien) =>
            //{
            //    Load(nhanVien);
            //};
            //Application.Current.OpenWindow(newWindow);
        }

        [RelayCommand]
        private void Edit()
        {
            // Lấy nhân viên được chọn (giả sử có thuộc tính IsSelected)
            //var selectedNhanVien = ListNhanVien.FirstOrDefault(x => x.IsSelected);
            //if (selectedNhanVien != null)
            //{
            //    var editWindow = new Window
            //    {
            //        Page = new SuaNhanVienPage(selectedNhanVien, this),
            //        MaximumHeight = 400,
            //        MaximumWidth = 400,
            //        MinimumHeight = 400,
            //        MinimumWidth = 400
            //    };
            //    Application.Current.OpenWindow(editWindow);
            //}
        }

        [RelayCommand]
        private void ToggleDeleteMode()
        {
            IsDeleteMode = !IsDeleteMode;
            if (IsDeleteMode == false)
            {
                var updatedList = new List<NhanVien>(ListNhanVien);
                foreach (var nv in updatedList)
                {
                    nv.IsSelected = false;
                }
                // Gán lại ListNhanVien để buộc giao diện làm mới
                ListNhanVien = new ObservableCollection<NhanVien>(updatedList);
            }
        }

        [RelayCommand]
        private async Task Delete()
        {
            var selectedItems = ListNhanVien.Where(x => x.IsSelected).ToList();
            foreach (var item in selectedItems)
            {
                await _nhanVienService.Delete(item.Id);
                ListNhanVien.Remove(item);
            }
            IsDeleteMode = false;
        }

        [RelayCommand]
        private async Task GoToChiTietNhanVienPage(Guid id)
        {
            await Shell.Current.GoToAsync($"ChiTietNhanVienPage?id={id}");
        }
    }
}