using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GarageManagement.Pages;
using GarageManagement.Services;

namespace GarageManagement.ViewModels
{
    public partial class TiepNhanXePageViewModel: BaseViewModel
    {
        [ObservableProperty]
        private string tenChuXe;
        [ObservableProperty]
        private string tenXe;
        [ObservableProperty]
        private string bienSo;
        [ObservableProperty]
        private string tenHieuXe;
        [ObservableProperty]
        private string diaChi;
        [ObservableProperty]
        private string soDienThoai;
        [ObservableProperty]
        private DateTime ngayTiepNhan=DateTime.Now;
        [ObservableProperty]
        private bool isCarExists = false;
        [ObservableProperty]
        private bool isCarNotFound = true; 

        private string STORAGE_KEY = "user-account-status";
        private readonly APIClientService<PhieuTiepNhan> _recordService;
        private readonly APIClientService<ThamSo> _ruleService;
        private readonly APIClientService<Xe> _carService;
        private readonly APIClientService<HieuXe> _hieuXeService;
        private readonly APIClientService<KhachHang> _userService;
        private readonly APIClientService<NhomNguoiDung> _groupService;
        private readonly UniqueConstraintCheckingService _checkService;

        public TiepNhanXePageViewModel(
            APIClientService<PhieuTiepNhan> recordService,
            APIClientService<ThamSo> ruleService,
            APIClientService<Xe> carService,
            APIClientService<HieuXe> hieuXeService,
            APIClientService<KhachHang> userService,
            APIClientService<NhomNguoiDung> groupService)
        {
            _recordService = recordService;
            _ruleService = ruleService;
            _carService = carService;
            _hieuXeService = hieuXeService;
            _userService = userService;
            _groupService = groupService;
        }


        partial void OnBienSoChanged(string value)
        {
            if (string.IsNullOrEmpty(value)) return;
            _=checkCarExist(value);
        }

        private async Task checkCarExist(string bienSo)
        {
            var result=await _carService.GetThroughtSpecialRoute($"BienSo/{bienSo}");
            if (result is null)
            {
                IsCarExists = false;
                IsCarNotFound = true;
            }
            else
            {
                IsCarExists = true;
                IsCarNotFound = false;
                TenXe = result.Ten;
                BienSo = result.BienSo;
                var hieuXe=await _hieuXeService.GetByID(result.HieuXeId);
                if (hieuXe is not null) TenHieuXe = hieuXe.TenHieuXe;
                var khachHang = await _userService.GetByID(result.KhachHangId);
                if (khachHang is not null)
                {
                    TenChuXe = khachHang.HoVaTen;
                    DiaChi = khachHang.DiaChi;
                    SoDienThoai = khachHang.SoDienThoai;
                }
            }
        }
        
        [RelayCommand]
        public async void AddNewCarRecord()
        {
            if (!IsCarExists || IsCarNotFound)
            {
                Shell.Current?.DisplayAlert("Thông báo", "Xe không tồn tại trong hệ thống, vui lòng kiểm tra lại thông tin xe.", "OK");
                return;
            }
            if (string.IsNullOrEmpty(BienSo))
            {
                Shell.Current?.DisplayAlert("Thông báo", "Vui lòng nhập đầy đủ thông tin chủ xe.", "OK");
                return;
            }
            if (NgayTiepNhan < DateTime.Now)
            {
                Shell.Current?.DisplayAlert("Thông báo", "Ngày tiếp nhận không hợp lệ.", "OK");
                return;
            }
            var car=await _carService.GetThroughtSpecialRoute($"BienSo/{bienSo}");
            await _recordService.Create(new PhieuTiepNhan()
            {
                Id=Guid.NewGuid(),
                XeId = car.Id,
                NgayTiepNhan = NgayTiepNhan,
            });
        }

        [RelayCommand]
        public async Task ViewCarDetails()
        {

        }

        [RelayCommand]
        public async Task ViewCarOwnerDetails()
        {

        }

        [RelayCommand]
        public async Task AddNewCar()
        {
            await Shell.Current.GoToAsync($"{nameof(ThemXePage)}");
        }
    }
}
