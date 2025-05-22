using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Maui.Alerts;
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

        [ObservableProperty]
        private ObservableCollection<string> listTrangThaiXe;

        [ObservableProperty]
        private string selectedTrangThaiXe;

        [ObservableProperty]
        private ObservableCollection<KhachHang> listKhachHang;

        [ObservableProperty]
        private ObservableCollection<string> filterFields = new ObservableCollection<string> { "Tên", "Số điện thoại", "Email" };

        [ObservableProperty]
        private string selectedFilterField;

        [ObservableProperty]
        private string filterValue;

        [ObservableProperty]
        private bool isCustomerFound;

        public Guid selectedKhachHangId;

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
            var list = await _hieuxeservice.GetAll();
            ListHieuXe = new ObservableCollection<HieuXe>(list);
            if (hieuXe is not null) SelectedHieuXe = ListHieuXe?.FirstOrDefault(x => x.Id == obj.HieuXeId);
            BienSo = obj.BienSo;
            TenChuXe = khachHang.HoVaTen;
            DienThoai = khachHang.SoDienThoai;
            DiaChi = khachHang.DiaChi;
            bool tmp = obj.KhaDung ?? false;
            if (tmp) TinhTrang = "Đang tiếp nhận trong Gara";
            else TinhTrang = "Không có trong Gara";
            var listXe=await _carservice.GetListOnSpecialRequirement($"PhoneNumber/{khachHang.SoDienThoai}");
            if (listXe is not null) TienNoCuaChuXe = listXe.Sum(u => u.TienNo ?? 0);
            ListTrangThaiXe = new ObservableCollection<string>()
            {
                "Đang tiếp nhận trong Gara",
                "Không có trong Gara"
            };
            if (obj.KhaDung ?? false) SelectedTrangThaiXe = "Đang tiếp nhận trong Gara";
            else SelectedTrangThaiXe = "Không có trong Gara";
            var listKhachHang = await _khachHangService.GetAll();
            ListKhachHang = new ObservableCollection<KhachHang>(listKhachHang);
            if (khachHang is not null) selectedKhachHangId = ListKhachHang?.FirstOrDefault(x => x.Id == obj.KhachHangId).Id ?? Guid.Empty;
        }

        [RelayCommand]
        public void ToggleEditMode()
        {
            IsReadOnly = false;
            IsUpdating = true;
        }

        [RelayCommand]
        public async Task UpdateCar()
        {
            if (selectedKhachHangId == Guid.Empty)
            {
                await Shell.Current.DisplayAlert("Thông báo", "Vui lòng chọn khách hàng", "OK");
                return;
            }
            if (string.IsNullOrEmpty(BienSo))
            {
                await Shell.Current.DisplayAlert("Thông báo", "Vui lòng nhập biển số xe", "OK");
                return;
            }
            if (string.IsNullOrEmpty(Name))
            {
                await Shell.Current.DisplayAlert("Thông báo", "Vui lòng nhập tên xe", "OK");
                return;
            }
            var car = await _carservice.GetByID(CarId);
            if (car is not null)
            {
                car.Ten = Name;
                car.BienSo = BienSo;
                car.HieuXeId = selectedHieuXe.Id;
                car.KhaDung= SelectedTrangThaiXe.Equals("Đang tiếp nhận trong Gara") ? true : false;
                car.KhachHangId = selectedKhachHangId;
                car.HieuXeId = SelectedHieuXe.Id;
                await _carservice.Update(car);
                var toast = Toast.Make("Đăng nhập thành công", CommunityToolkit.Maui.Core.ToastDuration.Short);
                await LoadAsync();
                await toast.Show();
            }
        }

        [RelayCommand]
        public void ExitEditMode()
        {
            IsReadOnly = true;
            IsUpdating = false;
            _ = LoadAsync();
        }

        [RelayCommand]
        public void Filter()
        {
            if (SelectedFilterField=="Tên")
            {
                var list=ListKhachHang.Where(x => x.HoVaTen.ToLower().Contains(FilterValue.ToLower())).ToList();
                ListKhachHang.Clear();
                if (list is not null) ListKhachHang=new ObservableCollection<KhachHang>(list);
            }
            else if (SelectedFilterField=="Số điện thoại")
            {
                var list=ListKhachHang.Where(x => x.SoDienThoai.ToLower().Contains(FilterValue.ToLower())).ToList();
                ListKhachHang.Clear();
                ListKhachHang=new ObservableCollection<KhachHang>(list);
            }
            else
            {
                var list = new List<KhachHang>();
                foreach (var kh in ListKhachHang)
                {
                    if (kh.Email is not null && kh.Email.ToLower().Contains(FilterValue.ToLower()))
                    {
                        list.Add(kh);
                    }
                }
                ListKhachHang.Clear();
                ListKhachHang=new ObservableCollection<KhachHang>(list);
            }
        }
    }
}
