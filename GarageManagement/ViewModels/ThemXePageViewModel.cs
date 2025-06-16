using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

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
        [ObservableProperty]
        private string tuoi;
        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private ObservableCollection<string> filterFields = new ObservableCollection<string> { "Tên", "Số điện thoại", "Email" };
        [ObservableProperty]
        private string selectedFilterField;
        [ObservableProperty]
        private string filterValue;
        [ObservableProperty]
        private bool isCustomerFound;

        public delegate void OnXeAddedDelegate(Xe xe);
        public OnXeAddedDelegate OnXeAdded { get; set; }

        private Guid chuXeId = Guid.NewGuid();
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
            _ = LoadAsync();
        }

        public async Task LoadAsync()
        {
            var list=await _hieuXeService.GetAll();
            DanhSachHieuXe = new ObservableCollection<HieuXe>(list);
        }

        [RelayCommand]
        public async Task SaveButtonClick()
        {
            var namePattern = new Regex(@"^[\p{L}\s]+$");
            if (!namePattern.IsMatch(HoVaTen))
            {
                await Shell.Current.DisplayAlert("Lỗi", "Họ và tên chỉ được chứa chữ cái", "OK");
                return;
            }

            // 2. Validate Tuổi (số nguyên dương)
            var agePattern = new Regex(@"^[1-9]\d*$");
            if (!agePattern.IsMatch(Tuoi))
            {
                await Shell.Current.DisplayAlert("Lỗi", "Tuổi phải là số nguyên dương", "OK");
                return;
            }

            // 3. Validate Số điện thoại (10-11 chữ số)
            var phonePattern = new Regex(@"^\d{10,11}$");
            if (!phonePattern.IsMatch(SoDienThoai))
            {
                await Shell.Current.DisplayAlert("Lỗi", "Số điện thoại phải có 10–11 chữ số", "OK");
                return;
            }

            // 4. Validate Email
            var emailPattern = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!emailPattern.IsMatch(Email))
            {
                await Shell.Current.DisplayAlert("Lỗi", "Email không đúng định dạng", "OK");
                return;
            }

            if (ChuXeNotExist && (HoVaTen == string.Empty || DiaChi == string.Empty || SoDienThoai == string.Empty || Tuoi == string.Empty || Email == string.Empty))
            {
                await Shell.Current.DisplayAlert("Thông báo", "Xe phải có chủ xe", "OK");
                return;
            }

            var chuXe = await _customerService.GetThroughtSpecialRoute("PhoneNumber", SoDienThoai);           
            if (ChuXeExist == false)
            {  
                if (int.TryParse(this.Tuoi, out var tuoi)) 
                {
                    await _customerService.Create(new KhachHang
                    {
                        Id = chuXeId,
                        HoVaTen = this.HoVaTen,
                        Tuoi = tuoi,
                        DiaChi = this.DiaChi,
                        SoDienThoai = this.SoDienThoai,
                        TienNo = 0,
                        Email = this.Email
                    });
                }
            }
            else
            {
                chuXeId = chuXe.Id; 
            }
            var listXe = await _xeService.GetAll();
            
            if (listXe is not null)
            {
                foreach (var item in listXe)
                {
                    if (BienSoXe==item.BienSo)
                    {
                        await Shell.Current.DisplayAlert("Thông báo", "Biển số xe đã tồn tại", "OK");
                        BienSoXe = "";
                        return;
                    }
                }
            }

            var xe = await _xeService.Create(new Xe
            {
                Id = Guid.NewGuid(),
                Ten = TenXe,
                BienSo = BienSoXe,
                HieuXeId = SelectedHieuXe.Id,
                KhachHangId = chuXeId,
                KhaDung = true,
                TienNo = 0
            });
            var toast = Toast.Make("Thêm xe mới thành công", CommunityToolkit.Maui.Core.ToastDuration.Short);
            await toast.Show();
            chuXeId = Guid.NewGuid();
            TenXe = string.Empty;
            SelectedHieuXe = null;
            BienSoXe = string.Empty;
            FilterValue = null;
            HoVaTen = string.Empty;
            Tuoi = string.Empty;
            SoDienThoai = string.Empty;
            DiaChi = string.Empty;
            Email = string.Empty;
            OnXeAdded?.Invoke(xe);
            MessagingCenter.Send(this, "ReloadData");
        }

        [RelayCommand]
        private async Task Filter()
        {
            if (string.IsNullOrEmpty(FilterValue))
            {
                await Shell.Current.DisplayAlert("Error", "Vui lòng nhập giá trị để lọc", "OK");
                return;
            }
            var ListChuXe = await _customerService.GetAll();
            var filtered = ListChuXe.FirstOrDefault(kh =>
            {
                return SelectedFilterField switch
                {
                    "Tên" => kh.HoVaTen?.ToLower().TrimStart().TrimEnd().Contains(FilterValue.ToLower().TrimStart().TrimEnd()) ?? false,
                    "Số điện thoại" => kh.SoDienThoai?.ToLower().TrimStart().TrimEnd().Contains(FilterValue.ToLower().TrimStart().TrimEnd()) ?? false,
                    "Email" => kh.Email?.ToLower().TrimStart().TrimEnd().Contains(FilterValue.ToLower().TrimStart().TrimEnd()) ?? false,
                    _ => false
                };
            });
            if (filtered != null)
            {
                IsCustomerFound = true;
                ChuXeExist = true;
                ChuXeNotExist = false;
                HoVaTen = filtered.HoVaTen;
                DiaChi = filtered.DiaChi;
                SoDienThoai = filtered.SoDienThoai;
                Tuoi = filtered.Tuoi.ToString() ?? string.Empty;
                Email = filtered.Email ?? string.Empty;
            }
            else
            {
                IsCustomerFound = false;
                ChuXeExist = false;
                ChuXeNotExist = true;
                HoVaTen = string.Empty;
                DiaChi = string.Empty;
                SoDienThoai = string.Empty;
                Tuoi = string.Empty;
                Email = string.Empty;
            }
        }
        partial void OnSoDienThoaiChanged(string value)
        {
            //_ = checkCustomerExistence(value);
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
                Tuoi = result.Tuoi.ToString() ?? string.Empty; 
                Email = result.Email ?? string.Empty;
            }
            else
            {
                ChuXeExist = false;
                ChuXeNotExist = true;
                HoVaTen = string.Empty;
                DiaChi = string.Empty;
                Tuoi = string.Empty; 
                Email = string.Empty;
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

        [ObservableProperty]
        private bool isVehicleFormVisible = true;
        [ObservableProperty]
        private bool isCustomerFormVisible = false;

        [RelayCommand]
        private async void NextStep()
        {
            if (string.IsNullOrWhiteSpace(TenXe) ||
        SelectedHieuXe == null ||
        string.IsNullOrWhiteSpace(BienSoXe))
            {
                await Shell.Current.DisplayAlert(
                    "Thông báo",
                    "Vui lòng nhập đầy đủ tên xe, hiệu xe và biển số trước khi tiếp tục",
                    "OK");
                return;
            }
            var listXe = await _xeService.GetAll();
            var nl = listXe.Select(xe => xe.BienSo);
            if (nl.Contains(BienSoXe))
            {
                await Shell.Current.DisplayAlert(
                    "Thông báo",
                    "Biển số đã tồn tại, vui lòng nhập biển số khác",
                    "OK");
                return;
            }

                IsVehicleFormVisible = false;
            IsCustomerFormVisible = true;
        }

        [RelayCommand]
        private void PreviousStep()
        {
            IsVehicleFormVisible = true;
            IsCustomerFormVisible = false;
        }

        [RelayCommand]
        private void HuyChon()
        {
            // reset flag hiển thị
            IsCustomerFound = false;
            ChuXeExist = false;
            ChuXeNotExist = true;

            // clear tất cả field chủ xe
            HoVaTen = string.Empty;
            DiaChi = string.Empty;
            SoDienThoai = string.Empty;
            Tuoi = string.Empty;
            Email = string.Empty;

            // sinh lại id để tạo khách mới nếu next bấm Save
            chuXeId = Guid.NewGuid();

            // nếu muốn clear luôn filter text + picker
            FilterValue = string.Empty;
            SelectedFilterField = null;
        }

    }
}
