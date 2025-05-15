using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageManagement.ViewModels
{
    public partial class ThemXePageViewModel: BaseViewModel
    {
        [ObservableProperty]
        private string tenXe;
        [ObservableProperty]
        private ObservableCollection<HieuXe> danhSachHieuXe;
        [ObservableProperty]
        private bool chuXeExist = false;
        [ObservableProperty]
        private bool chuXeNotExist = true;
        [ObservableProperty]
        private string bienSoXe;
        [ObservableProperty]
        private string soDienThoai;
        [ObservableProperty]
        private HieuXe selectedHieuXe;
        [ObservableProperty]
        private string hoVaTen;
        [ObservableProperty]
        private string diaChi;

        private readonly APIClientService<Xe> _xeService;
        private readonly APIClientService<KhachHang> _customerService;
        private readonly APIClientService<HieuXe> _hieuXeService;

        public ThemXePageViewModel(APIClientService<KhachHang> customerService,
            APIClientService<Xe> xeService,
            APIClientService<HieuXe> hieuXeService)
        {
            _customerService = customerService;
            _xeService = xeService;
            _hieuXeService = hieuXeService;
        }

        public async Task LoadAsync()
        {
            var list=await _hieuXeService.GetAll();
            DanhSachHieuXe = new ObservableCollection<HieuXe>(list);
        }

        [RelayCommand]
        public async Task SaveButtonClick()
        {
            if (ChuXeNotExist || !ChuXeExist)
            {
                await Shell.Current.DisplayAlert("Thông báo", "Xe phải có chủ xe", "OK");
                return;
            }
            var chuXe = await _customerService.GetThroughtSpecialRoute("PhoneNumber", SoDienThoai);
            var result=await _xeService.Create(new Xe()
            {
                Id=Guid.NewGuid(),
                Ten=TenXe,
                BienSo = BienSoXe,
                HieuXeId = SelectedHieuXe.Id,
                KhachHangId=chuXe.Id,
                KhaDung=true,
                TienNo = 0,
            });
            if (result!=null)
            {

            }
        }

        partial void OnSoDienThoaiChanged(string value)
        {
            _=checkCustomerExistence(value);
        }

        private async Task checkCustomerExistence(string value)
        {
            if (string.IsNullOrEmpty(value)) return;
            var result = await _customerService.GetThroughtSpecialRoute("PhoneNumber", value);
            if (result is not null)
            {
                ChuXeExist = true;
                ChuXeNotExist = false;
                HoVaTen = result.HoVaTen;
                DiaChi = result.DiaChi;
                SoDienThoai = result.SoDienThoai;
            }
            else
            {
                ChuXeExist = false;
                ChuXeNotExist = true;
            }
        }

        [RelayCommand]
        private async Task XemChiTietChuXe()
        {
            
        }

        [RelayCommand]
        private async Task Back()
        {
            var navigationStack = Shell.Current.Navigation.NavigationStack;
            if (navigationStack.Count > 1)
            {
                await Shell.Current.Navigation.PopAsync();
            }
            else
            {
                await Shell.Current.DisplayAlert("Thông báo", "Không thể quay lại trang trước", "OK");
            }
        }
    }
}
