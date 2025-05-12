using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace GarageManagement.ViewModels
{
    public partial class LapPhieuNhapPageViewModel : BaseViewModel
    {
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
            ListVatTu = new ObservableCollection<VatTuPhuTung>(l);
        }

        [RelayCommand]
        public void ThemChiTietPhieuNhap()
        {
            ListChiTietPhieuNhap.Add(new ChiTietPhieuNhapVatTu());
        }
    }
}
