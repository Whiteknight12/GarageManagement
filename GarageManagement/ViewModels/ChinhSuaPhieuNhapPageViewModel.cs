using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace GarageManagement.ViewModels
{
    public partial class ChinhSuaPhieuNhapPageViewModel : BaseViewModel
    {
        public Guid PhieuNhapId { get; set; }

        [ObservableProperty]
        private ObservableCollection<VatTuPhuTung> listVatTu = new();

        [ObservableProperty]
        private ObservableCollection<ChiTietPhieuNhapVatTu> listChiTietPhieuNhap = new();

        [ObservableProperty]
        private DateTime selectedDate = DateTime.UtcNow;

        [ObservableProperty]
        private double tongGiaTien;

        private readonly APIClientService<VatTuPhuTung> _vatTuService;
        private readonly APIClientService<PhieuNhapVatTu> _phieuNhapVatTuService;
        private readonly APIClientService<ChiTietPhieuNhapVatTu> _chiTietPhieuNhapVatTuService;

        public delegate void OnUpdateSuccessDelegate();
        public OnUpdateSuccessDelegate OnUpdateSuccess { get; set; } = () => { };

        public ChinhSuaPhieuNhapPageViewModel(
            APIClientService<PhieuNhapVatTu> phieuNhapVatTuService,
            APIClientService<ChiTietPhieuNhapVatTu> chiTietPhieuNhapVatTuService,
            APIClientService<VatTuPhuTung> vatTuService)
        {
            _phieuNhapVatTuService = phieuNhapVatTuService;
            _chiTietPhieuNhapVatTuService = chiTietPhieuNhapVatTuService;
            _vatTuService = vatTuService;
        }

        public async Task LoadDataAsync()
        {
            // 1) Load all parts into your VM list
            var allVatTu = await _vatTuService.GetAll();
            ListVatTu = new ObservableCollection<VatTuPhuTung>(allVatTu);

            // 2) Fetch the master record
            var phieuNhap = await _phieuNhapVatTuService.GetByID(PhieuNhapId);
            if (phieuNhap == null)
                return;

            SelectedDate = phieuNhap.NgayNhap;
            TongGiaTien = phieuNhap.TongTien;

            // 3) Pull back all detail rows, then filter & enrich
            var allChiTiet = await _chiTietPhieuNhapVatTuService.GetAll()
                              ?? new List<ChiTietPhieuNhapVatTu>();
            var list = allChiTiet
                       .Where(x => x.PhieuNhapId == PhieuNhapId)
                       .Select(item =>
                       {
                           // find the matching VatTu object
                           var match = allVatTu.FirstOrDefault(v => v.VatTuPhuTungId == item.VatTuId);
                           // set up your new helper property
                           item.SelectedVatTu = match;
                           // keep your old UI fields in sync
                           item.TenVatTu = match?.TenLoaiVatTuPhuTung;
                           item.DonGia = match?.DonGiaBanLoaiVatTuPhuTung;
                           item.ThanhTien = (item.SoLuong ?? 0) * (item.DonGia ?? 0);
                           return item;
                       })
                       .ToList();

            // 4) Push into the observable
            ListChiTietPhieuNhap = new ObservableCollection<ChiTietPhieuNhapVatTu>(list);
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
            TongGiaTien = ListChiTietPhieuNhap.Sum(x => (x.DonGia ?? 0) * (x.SoLuong ?? 0));
        }

        [RelayCommand]
        private async Task LuuPhieuNhap()
        {
            var listChiTiet = ListChiTietPhieuNhap.ToList();

            if (listChiTiet.Count == 0 || listChiTiet.Any(d => d.DonGia == null || d.SoLuong == null))
            {
                await Shell.Current.DisplayAlert("Cảnh báo", "Không được để trống chi tiết hợp lệ", "OK");
                return;
            }

            var updatedPhieu = await _phieuNhapVatTuService.GetByID(PhieuNhapId);
            updatedPhieu.NgayNhap = SelectedDate;
            updatedPhieu.TongTien = listChiTiet.Sum(x => (x.DonGia ?? 0) * (x.SoLuong ?? 0));

            await _phieuNhapVatTuService.Update(updatedPhieu);

            var existingList = await _chiTietPhieuNhapVatTuService.GetAll();
            var currentChiTietIds = listChiTiet.Select(c => c.Id).ToHashSet();
            var toDelete = existingList.Where(x => x.PhieuNhapId == PhieuNhapId && !currentChiTietIds.Contains(x.Id));
            foreach (var item in toDelete)
            {
                await _chiTietPhieuNhapVatTuService.Delete(item.Id);
            }

            foreach (var item in listChiTiet)
            {
                item.PhieuNhapId = PhieuNhapId;
                if (item.Id == Guid.Empty)
                {
                    item.Id = Guid.NewGuid();
                    await _chiTietPhieuNhapVatTuService.Create(item);
                }
                else
                {
                    await _chiTietPhieuNhapVatTuService.Update(item);
                }
            }

            await Toast.Make("Cập nhật phiếu nhập thành công", CommunityToolkit.Maui.Core.ToastDuration.Short).Show();
            OnUpdateSuccess?.Invoke();
            MessagingCenter.Send(this, "PhieuNhapUpdated");
            var win = Application.Current.Windows.LastOrDefault();
            if (win != null)
                Application.Current.CloseWindow(win);
        }
    }
} 