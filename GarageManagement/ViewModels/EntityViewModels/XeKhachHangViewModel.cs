using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageManagement.ViewModels.EntityViewModels
{
    public partial class XeKhachHangViewModel: BaseViewModel
    {
        public Xe Xe { get; }

        public XeKhachHangViewModel(Xe xe)
        {
            Xe = xe;
            LichSuTiepNhanList = xe.LichSuTiepNhanList ?? new();
            LichSuSuaChuaList = xe.LichSuSuaChuaList ?? new();
        }

        public string Ten => Xe.Ten;
        public string BienSo => Xe.BienSo;
        public string TenHieuXe => Xe.TenHieuXe ?? "";
        public string TinhTrang => Xe.TinhTrang ?? "";
        public double? TienNo => Xe.TienNo;
        public List<PhieuTiepNhan> LichSuTiepNhanList { get; }
        public List<PhieuSuaChua> LichSuSuaChuaList { get; }

        [ObservableProperty] 
        private bool isExpanded;
        [ObservableProperty] 
        private bool lichSuTiepNhanExpanded;
        [ObservableProperty] 
        private bool lichSuSuaChuaExpanded;

        [RelayCommand]
        private void ToggleSection(string section)
        {
            if (section == "LichSuTiepNhan")
                LichSuTiepNhanExpanded = !LichSuTiepNhanExpanded;
            else if (section == "LichSuSuaChua")
                LichSuSuaChuaExpanded = !LichSuSuaChuaExpanded;
        }

        [RelayCommand]
        private void ToggleXeSection()
        {
            IsExpanded = !IsExpanded;
        }
    }
}
