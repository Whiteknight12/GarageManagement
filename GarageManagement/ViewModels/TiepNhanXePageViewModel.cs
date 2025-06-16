using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Maui.Alerts;
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
        private DateTime ngayTiepNhan=DateTime.UtcNow.ToLocalTime();
        [ObservableProperty]
        private bool isCarExists = false;
        [ObservableProperty]
        private bool isCarNotFound = false;
        [ObservableProperty]
        private Guid carId;
        [ObservableProperty]
        private string cCCD;

        private string STORAGE_KEY = "user-account-status";
        private Guid ownerId;
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
                CarId = result.Id;
                TenXe = result.Ten;
                BienSo = result.BienSo;
                ownerId = result.KhachHangId;
                var hieuXe=await _hieuXeService.GetByID(result.HieuXeId);
                if (hieuXe is not null) TenHieuXe = hieuXe.TenHieuXe ?? "";
                var khachHang = await _userService.GetByID(result.KhachHangId);
                if (khachHang is not null)
                {
                    CCCD = khachHang.CCCD;
                    TenChuXe = khachHang.HoVaTen;
                    DiaChi = khachHang.DiaChi;
                    SoDienThoai = khachHang.SoDienThoai;
                }
            }
        }
        
        [RelayCommand]
        public async Task AddNewCarRecord()
        {
            var soXeTiepNhanToiDa = await _ruleService.GetThroughtSpecialRoute("SoXeToiDaTiepNhan");
            var listTiepNhan = await _recordService.GetListOnSpecialRequirement($"DayAndMonth/{DateTime.UtcNow.ToLocalTime().Day}/{DateTime.UtcNow.ToLocalTime().Month}");
            if (listTiepNhan?.Count >= (int)soXeTiepNhanToiDa?.GiaTri)
            {
                await Shell.Current.DisplayAlert("Thông báo", "Đã đạt số lượng xe tiếp nhận tối đa trong ngày", "OK");
                return;
            }
            if (!IsCarExists || IsCarNotFound)
            {
                Shell.Current?.DisplayAlert("Thông báo", "Xe không tồn tại trong hệ thống, vui lòng kiểm tra lại thông tin xe.", "OK");
                return;
            }
            if (string.IsNullOrEmpty(BienSo))
            {
                Shell.Current?.DisplayAlert("Thông báo", "Vui lòng nhập biển số xe", "OK");
                return;
            }
            if (NgayTiepNhan.Day < DateTime.UtcNow.ToLocalTime().Day)
            {
                Shell.Current?.DisplayAlert("Thông báo", "Ngày tiếp nhận không hợp lệ.", "OK");
                return;
            }
            var car=await _carService.GetThroughtSpecialRoute($"BienSo/{BienSo}");
            if (car is null)
            {
                await Shell.Current.DisplayAlert("Thông báo", "Xe không tồn tại", "OK");
                return;
            }
            var listPhieuTiepNhan = await _recordService.GetListOnSpecialRequirement($"XeId/{car.Id}");
            if (listPhieuTiepNhan?.Count>0)
            {
                foreach (var phieu in listPhieuTiepNhan)
                {
                    if (!phieu.DaHoanThanhBaoTri)
                    {
                        await Shell.Current.DisplayAlert("Thông báo", "Xe này đã được tiếp nhận và vẫn còn trong Gara", "OK");
                        return; 
                    }
                }
            }
            
            await _recordService.Create(new PhieuTiepNhan()
            {
                Id=Guid.NewGuid(),
                XeId = car.Id,
                NgayTiepNhan = NgayTiepNhan,
            });
            var toast = Toast.Make("Thêm phiếu tiếp nhận mới thành công", CommunityToolkit.Maui.Core.ToastDuration.Short);
            await toast.Show();
            BienSo = string.Empty;
            NgayTiepNhan = DateTime.UtcNow.ToLocalTime();
            IsCarExists = false;
            IsCarNotFound = true;
            MessagingCenter.Send(this, "ReloadData");
        }

        [RelayCommand]
        public void ViewCarDetails(Guid carId)
        {
            MessagingCenter.Send(this, "ShowCarDetails", carId);
        }

        [RelayCommand]
        public async Task ViewCarOwnerDetails()
        {
            MessagingCenter.Send(this, "ShowCustomerDetails", ownerId);
        }

        [RelayCommand]
        public async Task AddNewCar()
        {
            await Shell.Current.GoToAsync($"{nameof(ThemXePage)}");
        }
    }
}
