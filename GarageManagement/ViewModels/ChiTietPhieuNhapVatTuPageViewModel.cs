using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GarageManagement.Pages;
using System.Collections.ObjectModel;

namespace GarageManagement.ViewModels
{
    public partial class ChiTietPhieuNhapVatTuPageViewModel: BaseViewModel
    {
        private readonly APIClientService<ChiTietPhieuNhapVatTu> _chiTietPhieuNhapService;
        private readonly APIClientService<VatTuPhuTung> _vatTuService;
        private readonly APIClientService<PhieuNhapVatTu> _phieuNhapService;

        public Guid phieuNhapId { get; set; }

        [ObservableProperty]
        private ObservableCollection<ChiTietPhieuNhapVatTu> listChiTietPhieuNhap = new ObservableCollection<ChiTietPhieuNhapVatTu>();

        [ObservableProperty]
        private string ngayNhap;

        [ObservableProperty]
        private double tongTien;

        public ChiTietPhieuNhapVatTuPageViewModel(APIClientService<ChiTietPhieuNhapVatTu> chiTietPhieuNhapService, 
            APIClientService<VatTuPhuTung> vatTuService,
            APIClientService<PhieuNhapVatTu> phieuNhapService)
        {
            _chiTietPhieuNhapService = chiTietPhieuNhapService;
            _vatTuService = vatTuService;
;           _phieuNhapService = phieuNhapService;
        }

        public async Task LoadAsync()
        {
            if (!string.IsNullOrEmpty(phieuNhapId.ToString()))
            {
                var result=await _phieuNhapService.GetByID(phieuNhapId);
                NgayNhap = result?.NgayNhap.ToString("dd/MM/yyyy") ?? string.Empty;
                TongTien = result?.TongTien ?? 0.0;
                var list= await _chiTietPhieuNhapService.GetAll();
                if (list != null)
                {
                    var newList= list.Where(x => x.PhieuNhapId == phieuNhapId).ToList();
                    foreach (var item in newList)
                    {
                        var vatTu = await _vatTuService.GetByID(item.VatTuId ?? Guid.Empty);
                        if (vatTu != null)
                        {
                            item.TenVatTu = vatTu.TenLoaiVatTuPhuTung;
                            item.ThanhTien = item.SoLuong * item.DonGia;
                        }
                    }
                    ListChiTietPhieuNhap = new ObservableCollection<ChiTietPhieuNhapVatTu>(newList);
                }
            }
            
        }
        [RelayCommand]
        private async Task Edit()
        {
            // 1) Create and initialize the VM
            var vm = new ChinhSuaPhieuNhapPageViewModel(
                _phieuNhapService,
                _chiTietPhieuNhapService,
                _vatTuService
            )
            {
                PhieuNhapId = phieuNhapId
            };
            await vm.LoadDataAsync();

            // 1) Build & open the window **and capture it in a local var**
            var view = new ChinhSuaPhieuNhapPage(vm);
            var wrapper = new ContentPage { Content = view, Padding = 0 };
            var editWindow = new Window
            {
                Page = wrapper,
                Title = "Chỉnh sửa phiếu nhập",
                MaximumHeight = 600,
                MaximumWidth = 800,
                MinimumHeight = 600,
                MinimumWidth = 800
            };
            Application.Current.OpenWindow(editWindow);

            // 2) Subscribe to the update message, closing *this* editWindow
            MessagingCenter.Subscribe<ChinhSuaPhieuNhapPageViewModel>(this, "PhieuNhapUpdated", async _ =>
            {
                // refresh your list
                await LoadAsync();

                // close the **exact** edit window
                Application.Current.CloseWindow(editWindow);

                // unsubscribe so you don’t leak
                MessagingCenter.Unsubscribe<ChinhSuaPhieuNhapPageViewModel>(this, "PhieuNhapUpdated");
            });
        }


    }
}
