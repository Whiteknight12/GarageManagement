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
    public partial class ChiTietPhieuSuaChuaPageViewModel: BaseViewModel
    {
        private readonly APIClientService<ChiTietPhieuSuaChua> _chiTietPhieuSuaService;
        private readonly APIClientService<PhieuSuaChua> _phieuSuaService;
        private readonly APIClientService<NoiDungSuaChua> _noiDungSuaService;
        private readonly APIClientService<TienCong> _tienCongService;
        private readonly APIClientService<Xe> _xeService;
        private readonly APIClientService<VTPTChiTietPhieuSuaChua> _vtChiTietPhieuService;
        private readonly APIClientService<VatTuPhuTung> _vatTuService;

        [ObservableProperty]
        private ObservableCollection<ChiTietPhieuSuaChua> listNoiDung = new ObservableCollection<ChiTietPhieuSuaChua>();

        [ObservableProperty]
        private string bienSoXe;

        [ObservableProperty]
        private DateTime ngaySuaChua;

        [ObservableProperty]
        private double tongThanhTien;

        public Guid phieuSuaId { get; set; } 

        public ChiTietPhieuSuaChuaPageViewModel(APIClientService<ChiTietPhieuSuaChua> chiTietPhieuSuaService, 
            APIClientService<PhieuSuaChua> phieuSuaService,
            APIClientService<NoiDungSuaChua> noiDungSuaService,
            APIClientService<TienCong> tienCongService,
            APIClientService<Xe> xeService,
            APIClientService<VTPTChiTietPhieuSuaChua> vtChiTietPhieuService,
            APIClientService<VatTuPhuTung> vatTuService)
        {
            _chiTietPhieuSuaService = chiTietPhieuSuaService;
            _phieuSuaService = phieuSuaService;
            _noiDungSuaService = noiDungSuaService;
            _tienCongService = tienCongService;
            _xeService = xeService;
            _vtChiTietPhieuService = vtChiTietPhieuService;
            _vatTuService = vatTuService;
        }

        public async Task LoadAsync()
        {
            if (!string.IsNullOrEmpty(phieuSuaId.ToString()))
            {
                var result = await _phieuSuaService.GetByID(phieuSuaId);
                if (result is not null)
                {
                    var xe=await _xeService.GetByID(result.XeId);
                    BienSoXe = xe?.BienSo ?? "";
                    NgaySuaChua = result.NgaySuaChua;
                    var list = await _chiTietPhieuSuaService.GetAll();
                    var listND = list.Where(u => u.PhieuSuaChuaId == phieuSuaId).ToList();
                    if (listND is not null)
                    {
                        foreach (var item in listND)
                        {
                            var noiDung = await _noiDungSuaService.GetByID(item.NoiDungSuaChuaId??Guid.Empty);
                            item.TenNoiDungSuaChua = noiDung?.TenNoiDungSuaChua;
                            var tienCong = await _tienCongService.GetByID(item.TienCongId??Guid.Empty);
                            item.GiaTienCong = tienCong?.DonGiaLoaiTienCong;
                            var tmpList = await _vtChiTietPhieuService.GetAll();
                            item.ListSpecifiedVTPT = new ObservableCollection<VTPTChiTietPhieuSuaChua>(tmpList.Where(U => U.ChiTietPhieuSuaChuaId == item.Id));
                            double sum = item.GiaTienCong??0;
                            if (item.ListSpecifiedVTPT is not null)
                            {
                                foreach (var vtpt in item.ListSpecifiedVTPT)
                                {
                                    var vatTu = await _vatTuService.GetByID(vtpt.VatTuPhuTungId);
                                    vtpt.TenLoaiVatTuPhuTung = vatTu?.TenLoaiVatTuPhuTung;
                                    vtpt.DonGia = vatTu?.DonGiaBanLoaiVatTuPhuTung;
                                    sum += ((vtpt.DonGia??0) * vtpt.SoLuong);
                                }
                            }
                            item.ThanhTien = sum;
                        }
                        ListNoiDung = new ObservableCollection<ChiTietPhieuSuaChua>(listND);
                        TongThanhTien = ListNoiDung.Sum(u => u.ThanhTien??0);
                    }
                }
            }
        }
    }
}
