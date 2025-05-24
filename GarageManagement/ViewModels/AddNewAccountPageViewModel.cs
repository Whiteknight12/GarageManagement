using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace GarageManagement.ViewModels
{
    public partial class AddNewAccountPageViewModel: BaseViewModel
    {
        private readonly APIClientService<TaiKhoan> _accountService;
        private readonly APIClientService<KhachHang> _customerService;
        private readonly APIClientService<NhanVien> _userService;
        private readonly APIClientService<NhomNguoiDung> _groupUserService;

        [ObservableProperty]
        private ObservableCollection<UserForUI> listNguoiDung= new ObservableCollection<UserForUI>();

        [ObservableProperty]
        private ObservableCollection<NhomNguoiDung> nhomNguoiDung = new ObservableCollection<NhomNguoiDung>();

        [ObservableProperty]
        private UserForUI selectedUser;

        [ObservableProperty]
        private bool isSelectingUser = false;

        [ObservableProperty]
        private string hoVaTen;

        [ObservableProperty]
        private string cCCD;

        [ObservableProperty]
        private string phoneNumber;

        [ObservableProperty]
        private ObservableCollection<string> filterFields;

        public AddNewAccountPageViewModel(APIClientService<KhachHang> customerService, 
            APIClientService<NhanVien> userService,
            APIClientService<TaiKhoan> taiKhoanService,
            APIClientService<NhomNguoiDung> groupService)
        {
            _customerService = customerService;
            _userService = userService;
            _accountService = taiKhoanService;
            _groupUserService = groupService;
        }

        public async Task LoadAsync()
        {
            
            ListNguoiDung.Clear();
            var listKh=await _customerService.GetAll();
            if (listKh is not null)
            {
                foreach (var kh in listKh)
                {
                    if (kh.DaCoTaiKhoan) continue;
                    ListNguoiDung.Add(new UserForUI()
                    {
                        HoVaTen= kh.HoVaTen,
                        CCCD= kh.CCCD,
                        SDT= kh.SoDienThoai,
                        Id= kh.Id
                    });
                }
            }
            var listNv=await _userService.GetAll();
            if (listNv is not null)
            {
                foreach (var nv in listNv)
                {
                    if (nv.TaiKhoanId != null) continue;
                    ListNguoiDung.Add(new UserForUI()
                    {
                        HoVaTen = nv.HoTen,
                        CCCD = nv.CCCD,
                        SDT = nv.SoDienThoai,
                        Id = nv.Id
                    });
                }
            }
            var listNhom=await _groupUserService.GetAll();
            if (listNhom is not null) NhomNguoiDung = new ObservableCollection<NhomNguoiDung>(listNhom);
        }
    }
}
