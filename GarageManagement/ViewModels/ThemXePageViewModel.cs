using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
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
            var xe= await _xeService.Create(new Xe
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
            _ = checkCustomerExistence(value);
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
    }
}
