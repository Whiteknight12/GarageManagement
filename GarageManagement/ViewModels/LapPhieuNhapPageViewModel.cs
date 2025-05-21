using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace GarageManagement.ViewModels
{
    public partial class LapPhieuNhapPageViewModel : BaseViewModel
    {
        public List<Guid> listChiTietId = new();
        [ObservableProperty]
        private ObservableCollection<VatTuPhuTung> pickerSource = new(); 
        [ObservableProperty]
        private double tongGiaTien;
        [ObservableProperty]
        private ObservableCollection<VatTuPhuTung> listVatTu = new();
        [ObservableProperty]
        private ObservableCollection<ChiTietPhieuNhapVatTu> listChiTietPhieuNhap  = new();

        private readonly APIClientService<VatTuPhuTung> _vatTuService;
        private readonly APIClientService<PhieuNhapVatTu> _phieuNhapVatTuService;
        private readonly APIClientService<ChiTietPhieuNhapVatTu> _chiTietPhieuNhapVatTuService;

        public LapPhieuNhapPageViewModel(APIClientService<PhieuNhapVatTu> phieuNhapVatTuService,
            APIClientService<ChiTietPhieuNhapVatTu> chiTietPhieuNhapVatTuService,
            APIClientService<VatTuPhuTung> vatTuService)
        {
            _phieuNhapVatTuService = phieuNhapVatTuService;
            _chiTietPhieuNhapVatTuService = chiTietPhieuNhapVatTuService;
            _vatTuService = vatTuService;
            _ = LoadListVatTuAsync();
        }

        private async Task LoadListVatTuAsync()
        {
            var l = await _vatTuService.GetAll();
            PickerSource = ListVatTu = new ObservableCollection<VatTuPhuTung>(l);
            ThemChiTietPhieuNhap(); 
        }

        [RelayCommand]
        private void ThemChiTietPhieuNhap()
        {
            ListChiTietPhieuNhap.Add(new ChiTietPhieuNhapVatTu());
        }
        [RelayCommand]
        private void XoaChiTietPhieuNhap(ChiTietPhieuNhapVatTu chi)
        {
            ListChiTietPhieuNhap.Remove(chi);
            TongGiaTien = TongGiaTien - (chi.DonGia ?? 0) * (chi.SoLuong ?? 0); 
        }
        [RelayCommand]
        private void LuuPhieuNhap()
        {
            var listChiTiet = ListChiTietPhieuNhap.ToList();
            if (listChiTiet.Select(d => (d.DonGia == null || d.SoLuong == null)) != null)
            {
                Shell.Current.DisplayAlert("Cảnh báo", "Không được bỏ trống SỐ LƯỢNG và ĐƠN GIÁ", "OK");
                return;
            }
            var phieuNhapVatTu = new PhieuNhapVatTu
            {
                Id = Guid.NewGuid(),
                NgayNhap = DateTime.UtcNow.ToLocalTime(),
                TongTien = TongGiaTien
            };
            _ = _phieuNhapVatTuService.Create(phieuNhapVatTu);
            var finalListChiTiet = listChiTiet.Select(item => new ChiTietPhieuNhapVatTu
            {
                Id = Guid.NewGuid(),
                DonGia = item.DonGia,
                SoLuong = item.SoLuong,
                PhieuNhapId = phieuNhapVatTu.Id,
                VatTuId = item.VatTuId
            });
            foreach(var item in finalListChiTiet)
            {
                _ = _chiTietPhieuNhapVatTuService.Create(item); 
            }
            var toast = Toast.Make("Thêm phiếu nhập mới thành công", CommunityToolkit.Maui.Core.ToastDuration.Short);
            _ = toast.Show();
        }
    }
}
