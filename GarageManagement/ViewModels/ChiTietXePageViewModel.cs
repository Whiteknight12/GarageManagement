using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace GarageManagement.ViewModels
{
    public partial class ChiTietXePageViewModel : BaseViewModel
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

        [ObservableProperty]
        private bool isReadOnly = true;

        [ObservableProperty]
        private bool isUpdating = false;

        [ObservableProperty]
        private ObservableCollection<HieuXe> listHieuXe;

        [ObservableProperty]
        private HieuXe selectedHieuXe;

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
            var obj = await _carservice.GetByID(CarId);
            var hieuXe = await _hieuxeservice.GetByID(obj.HieuXeId);
            var khachHang = await _khachHangService.GetByID(obj.KhachHangId);
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
            var list = await _hieuxeservice.GetAll();
            if (ListHieuXe.Any()) ListHieuXe.Clear();
            ListHieuXe = new ObservableCollection<HieuXe>(list);
        }

        [RelayCommand]
        public void ToggleEditMode()
        {
            IsReadOnly = false;
            isUpdating = true;
        }

        [RelayCommand]
        public async Task UpdateCar()
        {
            var car = await _carservice.GetByID(CarId);
            if (car is not null)
            {
                car.Ten = Name;
                car.BienSo = BienSo;
                car.HieuXeId = selectedHieuXe.Id;

            }
        }

        [RelayCommand]
        public void ExitEditMode()
        {
            IsReadOnly = true;
            IsUpdating = false;
        }
    }
}
