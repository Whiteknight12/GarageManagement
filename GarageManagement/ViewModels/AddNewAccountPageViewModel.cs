using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace GarageManagement.ViewModels
{
    public partial class AddNewAccountPageViewModel : BaseViewModel
    {
        private readonly APIClientService<TaiKhoan> _accountService;
        private readonly APIClientService<KhachHang> _customerService;
        private readonly APIClientService<NhanVien> _userService;
        private readonly APIClientService<NhomNguoiDung> _groupUserService;

        [ObservableProperty]
        private ObservableCollection<UserForUI> listNguoiDung = new ObservableCollection<UserForUI>();

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

        [ObservableProperty]
        private string newUsername;

        [ObservableProperty]
        private string newPassword;

        [ObservableProperty]
        private NhomNguoiDung selectedRole;

        [ObservableProperty]
        private string nameValue;

        [ObservableProperty]
        private string cCCDValue;

        [ObservableProperty]
        private string phoneValue;

        [ObservableProperty]
        private string emailValue;

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
            NewUsername= string.Empty;
            NewPassword = string.Empty;
            SelectedRole = null;
            IsSelectingUser = false;
            ListNguoiDung.Clear();
            var listKh = await _customerService.GetAll();
            if (listKh is not null)
            {
                foreach (var kh in listKh)
                {
                    if (kh.DaCoTaiKhoan) continue;
                    ListNguoiDung.Add(new UserForUI()
                    {
                        HoVaTen = kh.HoVaTen,
                        CCCD = kh.CCCD,
                        SDT = kh.SoDienThoai,
                        Id = kh.Id,
                        Email= kh.Email
                    });
                }
            }
            var listNv = await _userService.GetAll();
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
                        Id = nv.Id,
                        Email = nv.Email
                    });
                }
            }
            var listNhom = await _groupUserService.GetAll();
            if (listNhom is not null) NhomNguoiDung = new ObservableCollection<NhomNguoiDung>(listNhom);
        }

        [RelayCommand]
        public async Task AddNewAccount()
        {
            if (SelectedRole is null)
            {
                await Shell.Current.DisplayAlert("Lỗi", "Vui lòng chọn nhóm người dùng", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(NewUsername) || string.IsNullOrWhiteSpace(NewPassword))
            {
                await Shell.Current.DisplayAlert("Lỗi", "Vui lòng nhập tên đăng nhập và mật khẩu", "OK");
                return;
            }
            if (SelectedUser is null)
            {
                await Shell.Current.DisplayAlert("Lỗi", "Vui lòng chọn người dùng để tạo tài khoản", "OK");
                return;
            }
            var listTK=await _accountService.GetAll();
            if (listTK is not null)
            {
                foreach (var item in listTK)
                {
                    if (item.TenDangNhap == NewUsername)
                    {
                        await Shell.Current.DisplayAlert("Thông báo", "Tên đăng nhập đã tồn tại", "OK");
                        NewUsername = "";
                        return;
                    }
                }
            }
            var result=await _accountService.Create(new TaiKhoan()
            {
                TenDangNhap=NewUsername,
                MatKhau=NewPassword,
                NhomNguoiDungId=SelectedRole.Id,
                NgayCap=DateTime.UtcNow.ToLocalTime() 
            });
            var result1 = await _userService.GetByID(SelectedUser.Id??Guid.Empty);
            var result2 = await _customerService.GetByID(SelectedUser.Id??Guid.Empty);
            if (result1 is not null)
            {
                result1.TaiKhoanId = result.Id;
                await _userService.Update(result1);
            }
            else if (result2 is not null)
            {
                result2.TaiKhoanId = result.Id;
                await _customerService.Update(result2);
            }
            var toast=Toast.Make("Tạo tài khoản thành công", CommunityToolkit.Maui.Core.ToastDuration.Short, 14);
            await toast.Show();
            MessagingCenter.Send(this, "ReloadAccountList");
            NewUsername = string.Empty;
            NewPassword = string.Empty;
            SelectedRole = null;
            SelectedUser = null;
            IsSelectingUser = false;
        }

        [RelayCommand]
        public async Task Filter()
        {
            await LoadAsync();
            if (!string.IsNullOrEmpty(NameValue))
            {
                ListNguoiDung = new ObservableCollection<UserForUI>(ListNguoiDung.Where(x => x.HoVaTen?.Contains(NameValue, StringComparison.OrdinalIgnoreCase) == true));
            }
            if (!string.IsNullOrEmpty(CCCDValue))
            {
                ListNguoiDung = new ObservableCollection<UserForUI>(ListNguoiDung.Where(x => x.CCCD?.Contains(CCCDValue, StringComparison.OrdinalIgnoreCase) == true));
            }
            if (!string.IsNullOrEmpty(PhoneValue))
            {
                ListNguoiDung = new ObservableCollection<UserForUI>(ListNguoiDung.Where(x => x.SDT?.Contains(PhoneValue, StringComparison.OrdinalIgnoreCase) == true));
            }
            if (!string.IsNullOrEmpty(EmailValue))
            {
                ListNguoiDung = new ObservableCollection<UserForUI>(ListNguoiDung.Where(x => x.Email?.Contains(EmailValue, StringComparison.OrdinalIgnoreCase) == true));
            }
        }
    }
}