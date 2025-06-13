using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GarageManagement.ViewModels
{
    public partial class QuanLiPhieuTiepNhanPageViewModel : BaseViewModel
    {
        private readonly APIClientService<PhieuTiepNhan> _phieuTiepNhanService;
        private readonly APIClientService<Xe> _xeService;
        private readonly APIClientService<KhachHang> _khachHangService;

        [ObservableProperty]
        private ObservableCollection<PhieuTiepNhan> listPhieuTiepNhan = new ObservableCollection<PhieuTiepNhan>();

        public QuanLiPhieuTiepNhanPageViewModel(APIClientService<PhieuTiepNhan> phieuTiepNhanService,
            APIClientService<Xe> xeService,
            APIClientService<KhachHang> khachHangService)
        {
            _phieuTiepNhanService = phieuTiepNhanService;
            _xeService = xeService;
            _khachHangService = khachHangService;
        }

        public async Task LoadAsync()
        {
            var list = await _phieuTiepNhanService.GetAll();
            if (list is not null)
            {
                foreach (var item in list)
                {
                    if (item.DaHoanThanhBaoTri) item.TinhTrang = "Lần tiếp nhận này đã hoàn thành bảo trì";
                    else item.TinhTrang = "Xe được tiếp nhận còn trong Gara để bảo trì";
                    var xe = await _xeService.GetByID(item.XeId);
                    if (xe is not null)
                    {
                        item.BienSoXe = xe.BienSo;
                        var kh = await _khachHangService.GetByID(xe.KhachHangId);
                        if (kh is not null)
                        {
                            item.TenKhachHang = kh.HoVaTen;
                            item.KhachHangId = kh.Id;
                        }
                    }
                    ListPhieuTiepNhan = new ObservableCollection<PhieuTiepNhan>(list);
                    IndexCalculator(ListPhieuTiepNhan);
                }
            }
        }

        [ObservableProperty] private bool isDeleteMode;
        [RelayCommand] private void ToggleDeleteMode() => IsDeleteMode = !IsDeleteMode;

        [RelayCommand]
        private async Task Delete()
        {
            var selected = ListPhieuTiepNhan.Where(p => p.IsSelected).ToList();
            foreach (var item in selected)
                await _phieuTiepNhanService.Delete(item.Id);   // hoặc API xoá của bạn

            foreach (var item in selected)
                ListPhieuTiepNhan.Remove(item);

            IsDeleteMode = false;
        }


        private void IndexCalculator(ObservableCollection<PhieuTiepNhan> list)
        {
            int c = 1;
            foreach (var item in list)
            {
                item.IdForUI = c++;
                item.OnPropertyChanged(nameof(item.IdForUI));
            }
        }

        [RelayCommand]
        public void ShowXeDetail(Guid XeId)
        {
            MessagingCenter.Send(this, "ShowCarDetails", XeId);
        }

        [RelayCommand]
        public void ShowKhachHangDetail(Guid KhachHangId)
        {
            MessagingCenter.Send(this, "ShowCustomerDetails", KhachHangId);
        }

        [RelayCommand]
        public async void Edit(Guid Id)
        {

        }
      
    }
}
