﻿using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageManagement.ViewModels
{
    public partial class BaoCaoTonViewModel: BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<ChiTietBaoCaoTon> baoCaoList= new ObservableCollection<ChiTietBaoCaoTon>();
        [ObservableProperty]
        private ObservableCollection<int> years = new ObservableCollection<int>();
        [ObservableProperty]
        private ObservableCollection<int> months = new ObservableCollection<int>();
        [ObservableProperty]
        private int selectedYear;
        [ObservableProperty]
        private int selectedMonth;
        [ObservableProperty]
        private bool hasData;

        private readonly APIClientService<ChiTietBaoCaoTon> _chiTietService;
        private readonly APIClientService<BaoCaoTon> _baoCaoService;
        private readonly APIClientService<PhieuNhapVatTu> _phieuNhapService;

        public BaoCaoTonViewModel(APIClientService<ChiTietBaoCaoTon> chiTietService,
            APIClientService<BaoCaoTon> baoCaoService,
            APIClientService<PhieuNhapVatTu> phieuNhapService)
        {
            _chiTietService = chiTietService;
            _baoCaoService = baoCaoService;
            _phieuNhapService = phieuNhapService;
        }

        public async Task LoadAsync()
        {
            var phieuNhaps = await _phieuNhapService.GetAll();
            if (phieuNhaps is not null)
            {
                List<int> years = phieuNhaps.Select(p => p.NgayNhap.Year).Distinct().ToList();
                years.Sort();
                Years = new ObservableCollection<int>(years);
            }
            Months.Clear();
            for (int i=1; i <= 12; i++)
            {
                Months.Add(i); 
            }
        }

        public async Task onDateChanged()
        {
            if (SelectedYear ==0 || SelectedMonth == 0)
            {
                return;
            }
            if (SelectedYear > DateTime.UtcNow.ToLocalTime().Year)
            {
                await Shell.Current.DisplayAlert("Thông báo", "Chưa thể xem báo cáo tồn của năm này", "OK");
                return;
            }
            if (SelectedMonth >= DateTime.UtcNow.ToLocalTime().Month && SelectedYear == DateTime.UtcNow.ToLocalTime().Year)
            {
                await Shell.Current.DisplayAlert("Thông báo", "Chưa thể xem báo cáo tồn của tháng này", "OK");
                return;
            }
            BaoCaoList.Clear();
            var chiTietBaoCao = await _chiTietService.GetListOnSpecialRequirement($"month-and-year/{SelectedMonth}/{SelectedYear}");
            if (chiTietBaoCao is not null) BaoCaoList= new ObservableCollection<ChiTietBaoCaoTon>(chiTietBaoCao);
            if (!BaoCaoList.Any()) HasData = false;
        }
    }
}
