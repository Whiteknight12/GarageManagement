using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

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


        private Guid chuXeId;
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
            
            int tui;
            if (ChuXeExist == false)
            {  
                if (int.TryParse(this.Tuoi, out tui)) 
                {
                    NewGuid();
                    await _customerService.Create(new KhachHang
                    {
                        Id = chuXeId,
                        HoVaTen = this.HoVaTen,
                        Tuoi = tui,
                        DiaChi = this.DiaChi,
                        SoDienThoai = this.SoDienThoai,
                        TienNo = 0,
                        Email = this.Email
                    });
                }
            }
            var result=await _xeService.Create(new Xe
            {
                Id=Guid.NewGuid(),
                Ten=TenXe,
                BienSo = BienSoXe,
                HieuXeId = SelectedHieuXe.Id,
                KhachHangId=chuXeId,
                KhaDung=true,
                TienNo = 0
            });
            if (result!=null)
            {

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
        private void NewGuid() 
        {
            chuXeId = Guid.NewGuid();
        }
    }
}
