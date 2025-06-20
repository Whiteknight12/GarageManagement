﻿using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace GarageManagement.ViewModels
{
    public partial class QuanLiTaiKhoanPageViewModel : BaseViewModel
    {
        private readonly APIClientService<TaiKhoan> _accountService;
        private readonly APIClientService<KhachHang> _customerService;
        private readonly APIClientService<NhanVien> _userService;
        private readonly APIClientService<NhomNguoiDung> _groupUserService;

        [ObservableProperty]
        private ObservableCollection<TaiKhoan> listTaiKhoan = new ObservableCollection<TaiKhoan>();

        [ObservableProperty]
        private ObservableCollection<NhomNguoiDung> listNhomNguoiDung = new ObservableCollection<NhomNguoiDung>();

        [ObservableProperty]
        private string cCCDValue;

        [ObservableProperty]
        private string nameValue;

        [ObservableProperty]
        private string usernameValue;

        [ObservableProperty]
        private string selectedRole;

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
                    var result1 = listCustomer.FirstOrDefault(x => x.TaiKhoanId == item.Id);
                    var result2 = listUser.FirstOrDefault(x => x.TaiKhoanId == item.Id);
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
                ListTaiKhoan = new ObservableCollection<TaiKhoan>(list);
            }
            if (!string.IsNullOrEmpty(NameValue))
            {
                var list = ListTaiKhoan.Where(u => u.HoTenNguoiDung!=null && u.HoTenNguoiDung.ToLower().Contains(NameValue.ToLower())).ToList();
                ListTaiKhoan = new ObservableCollection<TaiKhoan>(list);
            }
            if (!string.IsNullOrEmpty(CCCDValue))
            {
                var list = ListTaiKhoan.Where(u => u.CCCD!=null && u.CCCD.ToLower().Contains(CCCDValue.ToLower())).ToList();
                ListTaiKhoan = new ObservableCollection<TaiKhoan>(list);
            }
            if (SelectedRole != null && SelectedRole != "Trống")
            {
                var roles = await _groupUserService.GetAll(); 
                var role = roles.FirstOrDefault(x => x.TenNhom == SelectedRole);
                var list = ListTaiKhoan.Where(u => u.NhomNguoiDungId == role.Id).ToList();
                ListTaiKhoan = new ObservableCollection<TaiKhoan>(list);
            }
        }

        [RelayCommand]
        public void EditAccount(Guid Id)
        {
            MessagingCenter.Send(this, "EditAccount", Id);
        }

        [RelayCommand]
        public async Task DeleteAccount(Guid Id)
        {
            var confirm = await Shell.Current.DisplayAlert(
                "Xác nhận",
                "Bạn có chắc chắn muốn xóa tài khoản này không?",
                "Xóa",
                "Hủy"
            );
            if (!confirm) return;
            var account = ListTaiKhoan.FirstOrDefault(x => x.Id == Id);
            if (account is not null)
            {
                var list1 = await _userService.GetAll();
                var list2 = await _customerService.GetAll();
                var user = list1.FirstOrDefault(x => x.TaiKhoanId == Id);
                var customer = list2.FirstOrDefault(x => x.TaiKhoanId == Id);
                if (user is not null)
                {
                    user.TaiKhoanId = null;
                    await _userService.Update(user);
                }
                else if (customer is not null)
                {
                    customer.TaiKhoanId = null;
                    await _customerService.Update(customer);
                }
                await _accountService.Delete(Id);
                var toast=Toast.Make("Xóa tài khoản thành công", CommunityToolkit.Maui.Core.ToastDuration.Short, 14);
                await LoadAsync();
                await toast.Show();
            }
        }
    }
}