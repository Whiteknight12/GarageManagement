using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GarageManagement.ViewModels
{
    public partial class QuanLiTaiKhoanPageViewModel: BaseViewModel
    {
        private readonly APIClientService<TaiKhoan> _accountService;
        private readonly APIClientService<KhachHang> _customerService;
        private readonly APIClientService<NhanVien> _userService;
        private readonly APIClientService<NhomNguoiDung> _groupUserService;

        [ObservableProperty]
        private ObservableCollection<TaiKhoan> listTaiKhoan= new ObservableCollection<TaiKhoan>();

        [ObservableProperty]
        private ObservableCollection<NhomNguoiDung> listNhomNguoiDung=new ObservableCollection<NhomNguoiDung>();

        [ObservableProperty]
        private string cCCDValue;

        [ObservableProperty]
        private string nameValue;

        [ObservableProperty]
        private string usernameValue;

        [ObservableProperty]
        private NhomNguoiDung selectedRole;

        public QuanLiTaiKhoanPageViewModel(APIClientService<TaiKhoan> accountService, 
            APIClientService<KhachHang> customerService,
            APIClientService<NhanVien> userService,
            APIClientService<NhomNguoiDung> groupUserService)
        {
            _accountService = accountService;
            _customerService = customerService;
            _userService = userService;
            _groupUserService = groupUserService;
        }

        public async Task LoadAsync()
        {
            var list = await _accountService.GetAll();
            var listCustomer = await _customerService.GetAll();
            var listUser = await _userService.GetAll();
            var listGroup = await _groupUserService.GetAll();
            if (listGroup is not null) ListNhomNguoiDung = new ObservableCollection<NhomNguoiDung>(listGroup);
            ListNhomNguoiDung.Add(new NhomNguoiDung() { Id = Guid.Empty, TenNhom = "Trống" });
            int id = 0;
            if (list is not null)
            {
                foreach (var item in list)
                {
                    var result1=listCustomer.FirstOrDefault(x => x.TaiKhoanId == item.Id);
                    var result2=listUser.FirstOrDefault(x => x.TaiKhoanId == item.Id);
                    if (result1 is not null)
                    {
                        item.HoTenNguoiDung = result1.HoVaTen;
                        item.CCCD = result1.CCCD;
                    }
                    else if (result2 is not null)
                    {
                        item.HoTenNguoiDung = result2.HoTen;
                        item.CCCD = result2.CCCD;
                    }
                    if (item.NgayXoa is null) item.DangHoatDong = "Đang khả dụng";
                    else item.DangHoatDong = $"Bị xóa vào lúc {item.NgayXoa}";
                    item.VaiTro = ListNhomNguoiDung.FirstOrDefault(x => x.Id == item.NhomNguoiDungId)?.TenNhom;
                    item.UIId = id++;
                }
                ListTaiKhoan = new ObservableCollection<TaiKhoan>(list);
            }
        }

        [RelayCommand]
        public void AddNewAccount()
        {
            MessagingCenter.Send(this, "ShowAddNewAccount");
        }

        [RelayCommand]
        public async Task Filter()
        {
            await LoadAsync();
            if (!string.IsNullOrEmpty(UsernameValue))
            {
                var list = ListTaiKhoan.Where(u => u.TenDangNhap.ToLower().Contains(UsernameValue.ToLower())).ToList();
                ListTaiKhoan=new ObservableCollection<TaiKhoan>(list);
            }
            if (!string.IsNullOrEmpty(NameValue))
            {
                var list=ListTaiKhoan.Where(u=>u.HoTenNguoiDung.ToLower().Contains(NameValue.ToLower())).ToList();
                ListTaiKhoan=new ObservableCollection<TaiKhoan>(list);
            }
            if (!string.IsNullOrEmpty(CCCDValue))
            {
                var list=ListTaiKhoan.Where(u=>u.CCCD.ToLower().Contains(CCCDValue.ToLower())).ToList();
                ListTaiKhoan=new ObservableCollection<TaiKhoan>(list);
            }
            if (SelectedRole is not null && SelectedRole.TenNhom!="Trống")
            {
                var list=ListTaiKhoan.Where(u=>u.NhomNguoiDungId==SelectedRole.Id).ToList();
                ListTaiKhoan=new ObservableCollection<TaiKhoan>(list);
            }
        }
    }
}
