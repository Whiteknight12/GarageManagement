using APIClassLibrary;
using APIClassLibrary.APIModels;
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
    public partial class ChiTietXePageViewModel: BaseViewModel
    {
        [ObservableProperty]
        private Guid id;
        [ObservableProperty]
        private string name;
        [ObservableProperty]
        private string model;
        [ObservableProperty]
        private string bienSo;
        [ObservableProperty]
        private string tenChuXe;
        [ObservableProperty]
        private string dienThoai;
        [ObservableProperty]
        private string diaChi;
        [ObservableProperty]
        private string tinhTrang;
        [ObservableProperty]
        private double tienNoCuaChuXe;

        private readonly APIClientService<Xe> _carservice;
        private readonly APIClientService<HieuXe> _hieuxeservice;
        private readonly APIClientService<KhachHang> _khachHangService;
        private Guid carid;
        public ChiTietXePageViewModel(Guid CarID, APIClientService<Xe> carservice, 
            APIClientService<HieuXe> hieuXeService, APIClientService<KhachHang> khachHangService)
        {
            _carservice = carservice;
            _hieuxeservice = hieuXeService;
            _khachHangService = khachHangService;
            carid = CarID;
            _ = LoadAsync();
        }
        private async Task LoadAsync()
        {
            var obj=await _carservice.GetByID(carid);
            var hieuXe=await _hieuxeservice.GetByID(obj.HieuXeId);
            var khachHang= await _khachHangService.GetByID(obj.KhachHangId);
            id = obj.Id;
            Name = obj.Ten;
            Model = hieuXe.TenHieuXe;
            BienSo = obj.BienSo;
            TenChuXe = khachHang.HoVaTen;
            DienThoai = khachHang.SoDienThoai;
            DiaChi = khachHang.DiaChi;
            bool tmp = obj.KhaDung ?? false;
            if (tmp) TinhTrang = "Dang Trong Gara";
            else TinhTrang = "Khong O Trong Gara";
            TienNoCuaChuXe = obj.TienNo ?? 0;
        }
        [RelayCommand]
        private async Task GoBack()
        {
            await Shell.Current.GoToAsync($"//{nameof(QuanLiXePage)}", true);
        }
    }
}
