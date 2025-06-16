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
        private double tienNoCuaXe;

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
        private string cCCD;

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

        // Trigger TinhTrang update whenever SelectedTrangThaiXe changes
        partial void OnSelectedTrangThaiXeChanged(string value)
        {
            TinhTrang = value;
        }

        [ObservableProperty]
        private ObservableCollection<KhachHang> listKhachHang;

        [ObservableProperty]
        private ObservableCollection<string> filterFields = new ObservableCollection<string> { "CCCD", "Tên", "Số điện thoại", "Email" };

        [ObservableProperty]
        private string selectedFilterField;

        [ObservableProperty]
        private string filterValue;

        [ObservableProperty]
        private bool isCustomerFound;

        [ObservableProperty]
        private KhachHang selectedKhachHang;

        [ObservableProperty]
        private string nameValue;

        [ObservableProperty]
        private string emailValue;

        [ObservableProperty]
        private string cCCDValue;

        [ObservableProperty]
        private string phoneValue;

        public Guid selectedKhachHangId;

        private List<KhachHang> khs = new(); 

        private readonly APIClientService<Xe> _carservice;
        private readonly APIClientService<HieuXe> _hieuxeservice;
        private readonly APIClientService<KhachHang> _khachHangService;
        private readonly APIClientService<PhieuTiepNhan> _phieuTiepNhanService; 

        public ChiTietXePageViewModel(APIClientService<Xe> carservice,
            APIClientService<HieuXe> hieuXeService, APIClientService<KhachHang> khachHangService, APIClientService<PhieuTiepNhan> phieuTiepNhanService)
        {
            _carservice = carservice;
            _hieuxeservice = hieuXeService;
            _khachHangService = khachHangService;
            IsReadOnly = true;
            IsUpdating = false;
            _phieuTiepNhanService = phieuTiepNhanService;
        }



        public async Task LoadAsync()
        {
            khs = await _khachHangService.GetAll(); 
            IsReadOnly = true;
            IsUpdating = false;
            var obj = await _carservice.GetByID(CarId);
            if (obj is null) return;
            var hieuXe = await _hieuxeservice.GetByID(obj.HieuXeId);
            var khachHang = await _khachHangService.GetByID(obj.KhachHangId);
            Name = obj.Ten;
            Model = hieuXe?.TenHieuXe ?? string.Empty;
            var list = await _hieuxeservice.GetAll();
            ListHieuXe = new ObservableCollection<HieuXe>(list);
            if (hieuXe is not null) SelectedHieuXe = ListHieuXe.FirstOrDefault(x => x.Id == obj.HieuXeId);
            BienSo = obj.BienSo;
            TenChuXe = khachHang.HoVaTen;
            TienNoCuaXe = obj.TienNo.GetValueOrDefault();
            CCCD = khachHang.CCCD;
            DienThoai = khachHang.SoDienThoai;
            DiaChi = khachHang.DiaChi;
            // Set initial TinhTrang from object
            TinhTrang = (obj.KhaDung ?? false) ? "Đang tiếp nhận trong Gara" : "Không có trong Gara";
            ListTrangThaiXe = new ObservableCollection<string>
            {
                "Đang tiếp nhận trong Gara",
                "Không có trong Gara"
            };
            SelectedTrangThaiXe = TinhTrang; // will trigger OnSelectedTrangThaiXeChanged

            var listXe = await _carservice.GetListOnSpecialRequirement($"PhoneNumber/{khachHang.SoDienThoai}");
            if (listXe is not null) TienNoCuaChuXe = listXe.Sum(u => u.TienNo ?? 0);
            var listKhachHang = await _khachHangService.GetAll();
            ListKhachHang = new ObservableCollection<KhachHang>(listKhachHang);
            if (khachHang is not null)
            {
                selectedKhachHangId = ListKhachHang.FirstOrDefault(x => x.Id == obj.KhachHangId)?.Id ?? Guid.Empty;
                SelectedKhachHang = ListKhachHang.FirstOrDefault(x => x.Id == obj.KhachHangId);
            }
            SelectedFilterField = "CCCD";
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
            var allCars = await _carservice.GetAll();
            if (allCars.Any(x => x.BienSo == BienSo && x.Id != CarId))
            {
                await Shell.Current.DisplayAlert(
                    "Thông báo",
                    "Biển số này đã có trong hệ thống, vui lòng nhập biển số khác",
                    "OK");
                return;
            }
            var car = await _carservice.GetByID(CarId);
            if (car is not null)
            {
                car.Ten = Name;
                car.BienSo = BienSo;
                car.HieuXeId = SelectedHieuXe.Id;
                car.TienNo = TienNoCuaXe; 
                // update obj.KhaDung from selected status
                car.KhaDung = SelectedTrangThaiXe == "Đang tiếp nhận trong Gara";
                car.KhachHangId = selectedKhachHangId;
                await _carservice.Update(car);
                var khach = await _khachHangService.GetByID(SelectedKhachHang.Id);
                khach.TienNo = TienNoCuaChuXe;
                await _khachHangService.Update(khach); 
                var toast = Toast.Make("Cập nhật xe thành công", CommunityToolkit.Maui.Core.ToastDuration.Short);
                await LoadAsync();
                await toast.Show();
            }
            MessagingCenter.Send(this, "XeUpdated"); 
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
            var list = khs.ToList();

            if (!string.IsNullOrWhiteSpace(CCCDValue))
                list = list
                    .Where(x => !string.IsNullOrWhiteSpace(x.CCCD)
                                && x.CCCD.Contains(CCCDValue, StringComparison.OrdinalIgnoreCase))
                    .ToList();

            if (!string.IsNullOrWhiteSpace(NameValue))
                list = list
                    .Where(x => !string.IsNullOrWhiteSpace(x.HoVaTen)
                                && x.HoVaTen.Contains(NameValue, StringComparison.OrdinalIgnoreCase))
                    .ToList();

            if (!string.IsNullOrWhiteSpace(EmailValue))
                list = list
                    .Where(x => !string.IsNullOrWhiteSpace(x.Email)
                                && x.Email.Contains(EmailValue, StringComparison.OrdinalIgnoreCase))
                    .ToList();

            if (!string.IsNullOrWhiteSpace(PhoneValue))
                list = list
                    .Where(x => !string.IsNullOrWhiteSpace(x.SoDienThoai)
                                && x.SoDienThoai.Contains(PhoneValue, StringComparison.OrdinalIgnoreCase))
                    .ToList();

            if (SelectedKhachHang != null && !list.Contains(SelectedKhachHang))
            {
                SelectedKhachHang = null;
            }

            ListKhachHang = new ObservableCollection<KhachHang>(list);

            OnPropertyChanged(nameof(ListKhachHang));
        }

        [RelayCommand]
        public async Task MarkAsDone()
        {
            var car = await _carservice.GetByID(carId);
            var ps = await _phieuTiepNhanService.GetAll();
            var ps2 = ps.Where(p => p.XeId == car.Id);
            var ps3 = ps2.OrderByDescending(p => p.NgayTiepNhan);
            var p = ps3.FirstOrDefault();
            if (p.DaHoanThanhBaoTri == true)
            {
                await Shell.Current.DisplayAlert(
                    "Thông báo",
                    "",
                    "OK");
                return;
            }
            p.DaHoanThanhBaoTri = true;
            _phieuTiepNhanService.Update(p);
            car.KhaDung = false;
            _carservice.Update(car);
            
        }
    }
}
