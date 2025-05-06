using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GarageManagement.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageManagement.ViewModels
{
    public partial class ChiTietXePageViewModel: BaseViewModel
    {
        [ObservableProperty]
        private int id;
        [ObservableProperty]
        private string name;
        [ObservableProperty]
        private string model;
        [ObservableProperty]
        private string bienSo;
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

        private readonly APIClientService<Xe> _carservice;
        private string carid;
        public ChiTietXePageViewModel(string CarID, APIClientService<Xe> carservice)
        {
            _carservice = carservice;
            carid= CarID;
            _ = LoadAsync();
        }
        private async Task LoadAsync()
        {
            var obj=await _carservice.GetByID(int.Parse(carid));
            Id = obj.Id;
            Name = obj.Name;
            Model = obj.Model;
            BienSo = obj.BienSo;
            TenChuXe = obj.TenChuXe;
            DienThoai = obj.DienThoai;
            DiaChi = obj.DiaChi;
            bool tmp = obj.IsAvailable ?? false;
            if (tmp) TinhTrang = "Dang Trong Gara";
            else TinhTrang = "Khong O Trong Gara";
            TienNoCuaChuXe = obj.TienNoCuaChuXe ?? 0;
        }
        [RelayCommand]
        private async Task GoBack()
        {
            await Shell.Current.GoToAsync($"//{nameof(QuanLiXePage)}", true);
        }
    }
}
