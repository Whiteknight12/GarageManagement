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
        private Guid carId;
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

        public ChiTietXePageViewModel(APIClientService<Xe> carservice, 
            APIClientService<HieuXe> hieuXeService, APIClientService<KhachHang> khachHangService)
        {
            _carservice = carservice;
            _hieuxeservice = hieuXeService;
            _khachHangService = khachHangService;
        }

        public async Task LoadAsync()
        {
            var obj=await _carservice.GetByID(CarId);
            var hieuXe=await _hieuxeservice.GetByID(obj.HieuXeId);
            var khachHang= await _khachHangService.GetByID(obj.KhachHangId);
            Name = obj.Ten;
            Model = hieuXe.TenHieuXe ?? "";
            BienSo = obj.BienSo;
            TenChuXe = khachHang.HoVaTen;
            DienThoai = khachHang.SoDienThoai;
            DiaChi = khachHang.DiaChi;
            bool tmp = obj.KhaDung ?? false;
            if (tmp) TinhTrang = "Đang tiếp nhận trong Gara";
            else TinhTrang = "Không có trong Gara";
            TienNoCuaChuXe = obj.TienNo ?? 0;
        }
    }
}
