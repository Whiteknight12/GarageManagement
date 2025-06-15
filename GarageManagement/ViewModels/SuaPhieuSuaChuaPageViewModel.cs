using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;

namespace GarageManagement.ViewModels
{
    public partial class SuaPhieuSuaChuaPageViewModel: BaseViewModel
    {
        private int index = 0;
        private int indexVTPT = 0;

        private readonly APIClientService<ChiTietPhieuSuaChua> _chiTietPhieuSuaService;
        private readonly APIClientService<PhieuSuaChua> _phieuSuaService;
        private readonly APIClientService<NoiDungSuaChua> _noiDungSuaService;
        private readonly APIClientService<TienCong> _tienCongService;
        private readonly APIClientService<Xe> _xeService;
        private readonly APIClientService<VTPTChiTietPhieuSuaChua> _vtChiTietPhieuService;
        private readonly APIClientService<VatTuPhuTung> _vatTuService;
        private readonly APIClientService<KhachHang> _khachHangService;
        private readonly APIClientService<PhieuSuaChua> _phieuService;
        private readonly APIClientService<VTPTChiTietPhieuSuaChua> _vtptChiTietPhieuSuaChuaService;

        public delegate void OnPhieuSuaChuaAddedDelegate(PhieuSuaChua phieuSuaChua);

        public OnPhieuSuaChuaAddedDelegate OnPhieuSuaChuaAdded { get; set; }

        [ObservableProperty]
        private ObservableCollection<string> listBienSoXe = new ObservableCollection<string>();

        [ObservableProperty]
        private string selectedBienSoXe;

        [ObservableProperty]
        private DateTime ngaySuaChua = DateTime.Now;

        [ObservableProperty]
        private ObservableCollection<ChiTietPhieuSuaChua> listNoiDung = new ObservableCollection<ChiTietPhieuSuaChua>();

        [ObservableProperty]
        public ObservableCollection<TienCong> listTienCong = new ObservableCollection<TienCong>();

        [ObservableProperty]
        private ObservableCollection<VatTuPhuTung> listVatTuPhuTung = new ObservableCollection<VatTuPhuTung>();

        [ObservableProperty]
        private double tongThanhTien = 0;

        public Guid phieuSuaChuaId { get; set; }

        [ObservableProperty]
        private ObservableCollection<NoiDungSuaChua> listNoiDungSuaChua = new ObservableCollection<NoiDungSuaChua>();



        public SuaPhieuSuaChuaPageViewModel(APIClientService<ChiTietPhieuSuaChua> chiTietPhieuSuaService,
            APIClientService<PhieuSuaChua> phieuSuaService,
            APIClientService<NoiDungSuaChua> noiDungSuaService,
            APIClientService<TienCong> tienCongService,
            APIClientService<Xe> xeService,
            APIClientService<VTPTChiTietPhieuSuaChua> vtChiTietPhieuService,
            APIClientService<VatTuPhuTung> vatTuService,
            APIClientService<KhachHang> khachHangService,
            APIClientService<PhieuSuaChua> phieuService,
            APIClientService<VTPTChiTietPhieuSuaChua> vpttChiTietPhieuSuaChuaService)
        {
            _chiTietPhieuSuaService = chiTietPhieuSuaService;
            _phieuSuaService = phieuSuaService;
            _noiDungSuaService = noiDungSuaService;
            _tienCongService = tienCongService;
            _xeService = xeService;
            _vtChiTietPhieuService = vtChiTietPhieuService;
            _vatTuService = vatTuService;
            _khachHangService = khachHangService;
            _phieuService = phieuService;
            _vtptChiTietPhieuSuaChuaService = vpttChiTietPhieuSuaChuaService;
        }

        public async Task LoadAsync()
        {
            var listcar = await _xeService.GetAll();
            if (listcar is not null)
            {
                ListBienSoXe.Clear();
                foreach (var car in listcar)
                {
                    ListBienSoXe.Add(car.BienSo);
                }
                OnPropertyChanged(nameof(ListBienSoXe));
            }
            var listcong = await _tienCongService.GetAll();
            if (listcong is not null)
            {
                ListTienCong.Clear();
                foreach (var tiencong in listcong) ListTienCong.Add(tiencong);
                OnPropertyChanged(nameof(ListTienCong));
            }
            var listphutung = await _vatTuService.GetAll();
            if (listphutung is not null)
            {
                ListVatTuPhuTung.Clear();
                foreach (var vattu in listphutung) ListVatTuPhuTung.Add(vattu);
                OnPropertyChanged(nameof(ListVatTuPhuTung));
            }
            var listNoiDungSuaChua = await _noiDungSuaService.GetAll();
            if (listNoiDungSuaChua is not null)
            {
                ListNoiDungSuaChua.Clear();
                foreach (var item in listNoiDungSuaChua) ListNoiDungSuaChua.Add(item);
                OnPropertyChanged(nameof(ListNoiDungSuaChua));
            }
            if (ListNoiDung.Count == 0)
            {
                ListNoiDung.Add(new ChiTietPhieuSuaChua
                {
                    NoiDungId = index++
                });
            }
            await LoadPhieuSuaChuaAsync(phieuSuaChuaId);
        }

        public async Task LoadPhieuSuaChuaAsync(Guid phieuId)
        {
            var phieu = await _phieuSuaService.GetByID(phieuId);
            if (phieu is null)
            {
                await Shell.Current.DisplayAlert("Thông báo", "Không tìm thấy phiếu sửa chữa", "OK");
                return;
            }

            // Gán thông tin chính
            phieuSuaChuaId = phieuId;
            NgaySuaChua = phieu.NgaySuaChua;

            // Biển số xe
            var xe = await _xeService.GetByID(phieu.XeId);
            SelectedBienSoXe = xe?.BienSo;

            // Load chi tiết
            var chiTietList = await _chiTietPhieuSuaService.GetAll();
            var listND = chiTietList.Where(u => u.PhieuSuaChuaId == phieuId).ToList();

            ListNoiDung.Clear();

            foreach (var item in listND)
            {
                var tienCong = await _tienCongService.GetByID(item.TienCongId ?? Guid.Empty);
                var noiDung = await _noiDungSuaService.GetByID(item.NoiDungSuaChuaId ?? Guid.Empty);

                double sum = tienCong?.DonGiaLoaiTienCong ?? 0;

                var chiTiet = new ChiTietPhieuSuaChua
                {
                    NoiDungId = index++,
                    NoiDungSuaChuaId = item.NoiDungSuaChuaId,
                    SelectedNoiDungSuaChua = ListNoiDungSuaChua.FirstOrDefault(x => x.Id == item.NoiDungSuaChuaId),
                    TienCongId = item.TienCongId,
                    GiaTienCong = tienCong?.DonGiaLoaiTienCong,
                    ThanhTien = 0,
                    ListSpecifiedVTPT = new ObservableCollection<VTPTChiTietPhieuSuaChua>()
                };

                // Load VTPT
                var vtptList = await _vtptChiTietPhieuSuaChuaService.GetAll();
                var listVTPT = vtptList.Where(v => v.ChiTietPhieuSuaChuaId == item.Id).ToList();

                foreach (var vt in listVTPT)
                {
                    var vtptObj = await _vatTuService.GetByID(vt.VatTuPhuTungId);
                    var vtpt = new VTPTChiTietPhieuSuaChua
                    {
                        IdForUI = chiTiet.NoiDungId ?? 0,
                        IdForDeleteUI = indexVTPT++,
                        VatTuPhuTungId = vt.VatTuPhuTungId,
                        SelectedVTPTId = vt.VatTuPhuTungId,
                        TenLoaiVatTuPhuTung = vtptObj?.TenLoaiVatTuPhuTung,
                        DonGia = vtptObj?.DonGiaBanLoaiVatTuPhuTung,
                        SoLuong = vt.SoLuong
                    };
                    sum += (vtpt.DonGia ?? 0) * vtpt.SoLuong;
                    chiTiet.ListSpecifiedVTPT.Add(vtpt);
                }

                chiTiet.ThanhTien = sum;
                ListNoiDung.Add(chiTiet);
            }

            TongThanhTien = ListNoiDung.Sum(u => u.ThanhTien ?? 0);
        }


        [RelayCommand]
        public void ThemNoiDungSuaChua()
        {
            ListNoiDung.Add(new ChiTietPhieuSuaChua
            {
                NoiDungId = index++
            });
        }

        [RelayCommand]
        public async void LuuPhieuSuaChua()
        {
            if (SelectedBienSoXe is null)
            {
                await Shell.Current.DisplayAlert("Thông báo", "Không được bỏ trống biển số xe", "OK");
                return;
            }
            if (ListNoiDung.Count == 0)
            {
                await Shell.Current.DisplayAlert("Thông báo", "Không có nội dung sữa chữa", "OK");
                return;
            }
            if (NgaySuaChua.Date < DateTime.UtcNow.ToLocalTime().Date)
            {
                await Shell.Current.DisplayAlert("Thông báo", "Ngày sửa chữa không được nhỏ hơn ngày hiện tại!", "OK");
                return;
            }
            foreach (var item in ListNoiDung)
            {
                if (string.IsNullOrEmpty(item.NoiDungSuaChuaId.ToString()))
                {
                    await Shell.Current.DisplayAlert("Thông báo", "Không được bỏ trống nội dung!", "OK");
                    return;
                }
                if (item.TienCongId is null)
                {
                    await Shell.Current.DisplayAlert("Thông báo", "Không được bỏ trống tiền công!", "OK");
                    return;
                }
                if (item.ThanhTien is null)
                {
                    await Shell.Current.DisplayAlert("Thông báo", "Có vẻ như đã có lỗi xảy ra", "OK");
                    return;
                }
            }
            Xe xe = await _xeService.GetThroughtSpecialRoute($"BienSo/{SelectedBienSoXe}");
            if (xe is null)
            {
                await Shell.Current.DisplayAlert("Thông báo", "Không tìm thấy xe có biển số xe trên", "OK");
                return;
            }
            var checkuser = await _khachHangService.GetByID(xe.KhachHangId);
            if (checkuser is null)
            {
                await Shell.Current.DisplayAlert("Thông báo", "Không tìm thấy chủ xe có biển số xe trên", "OK");
                return;
            }
            PhieuSuaChua obj = await _phieuService.Create(new PhieuSuaChua
            {
                Id = Guid.NewGuid(),
                XeId = xe.Id,
                NgaySuaChua = NgaySuaChua,
                TongTien = TongThanhTien
            });
            if (obj is null)
            {
                await Shell.Current.DisplayAlert("Thông báo", "Tạo phiếu sữa chữa mới thất bại", "OK");
                return;
            }
            foreach (var item in ListNoiDung)
            {
                var chiTietPhieuSuaChua = await _chiTietPhieuSuaService.Create(new ChiTietPhieuSuaChua
                {
                    NoiDungSuaChuaId = item.NoiDungSuaChuaId,
                    PhieuSuaChuaId = obj.Id,
                    TienCongId = item.TienCongId,
                    ThanhTien = item.ThanhTien
                });
                if (chiTietPhieuSuaChua is null)
                {
                    await Shell.Current.DisplayAlert("Thông báo", "Có lỗi xảy ra khi tạo chi tiết phiếu sữa chữa", "OK");
                    return;
                }
                foreach (var chiTietVTPT in item.ListSpecifiedVTPT)
                {
                    if (chiTietVTPT.SelectedVTPTId == null)
                    {
                        await Shell.Current.DisplayAlert("Thông báo", "Không được bỏ trống vật tư phụ tùng", "OK");
                        return;
                    }
                    if (string.IsNullOrEmpty(chiTietVTPT.SoLuong.ToString()) || chiTietVTPT.SoLuong == 0)
                    {
                        await Shell.Current.DisplayAlert("Thông báo", "Không được bỏ trống số lượng", "OK");
                        return;
                    }
                    var result = await _vtptChiTietPhieuSuaChuaService.Create(new VTPTChiTietPhieuSuaChua
                    {
                        Id = Guid.NewGuid(),
                        ChiTietPhieuSuaChuaId = chiTietPhieuSuaChua.Id,
                        VatTuPhuTungId = chiTietVTPT.SelectedVTPTId ?? Guid.Empty,
                        SoLuong = chiTietVTPT.SoLuong
                    });
                    if (result is null)
                    {
                        await Shell.Current.DisplayAlert("Thông báo", "Có lỗi xảy ra", "OK");
                        return;
                    }
                    var vtpt = await _vatTuService.GetByID(chiTietVTPT.SelectedVTPTId ?? Guid.Empty);
                    if (vtpt is not null)
                    {
                        vtpt.SoLuong -= chiTietVTPT.SoLuong;
                        await _vatTuService.Update(vtpt);
                    }
                }
            }
            checkuser.TienNo += TongThanhTien;
            xe.TienNo += TongThanhTien;
            await _khachHangService.Update(checkuser);
            await _xeService.Update(xe);
            var toast = Toast.Make("Cập nhật phiếu sữa chữa mới thành công", CommunityToolkit.Maui.Core.ToastDuration.Short);
            await toast.Show();
            SelectedBienSoXe = null;
            ListNoiDung = new ObservableCollection<ChiTietPhieuSuaChua>();
            TongThanhTien = 0;
            OnPhieuSuaChuaAdded?.Invoke(obj);
        }

        [RelayCommand]
        public void Remove(int itemid)
        {
            var item = ListNoiDung.FirstOrDefault(u => u.NoiDungId == itemid);
            if (item is not null) ListNoiDung.Remove(item);
            TongThanhTien = ListNoiDung.Sum(u => u.ThanhTien ?? 0);
        }

        [RelayCommand]
        public void AddVTPT(int noiDungId)
        {
            ListNoiDung?.FirstOrDefault(u => u.NoiDungId == noiDungId)?.ListSpecifiedVTPT.Add(new VTPTChiTietPhieuSuaChua
            {
                IdForUI = noiDungId,
                IdForDeleteUI = indexVTPT++,
            });
            var item = ListNoiDung?.FirstOrDefault(u => u.NoiDungId == noiDungId);
            item?.OnPropertyChanged(nameof(item.ListSpecifiedVTPT));
        }

        [RelayCommand]
        public void RemoveVTPT(int id)
        {
            foreach (var item in ListNoiDung)
            {
                var deleteItem = item.ListSpecifiedVTPT.FirstOrDefault(u => u.IdForDeleteUI == id);
                if (deleteItem is not null)
                {
                    var updateItem = ListNoiDung.FirstOrDefault(u => u.NoiDungId == deleteItem.IdForUI);
                    if (updateItem is not null)
                    {
                        updateItem.ListSpecifiedVTPT?.Remove(deleteItem);
                        updateItem.ThanhTien = updateItem.ListSpecifiedVTPT?.Sum(u => u.SoLuong * u.DonGia);
                        updateItem.ThanhTien += updateItem.GiaTienCong ?? 0;
                        updateItem.OnPropertyChanged(nameof(updateItem.ThanhTien));
                    }
                    item.ListSpecifiedVTPT.Remove(deleteItem);
                    item.OnPropertyChanged(nameof(item.ListSpecifiedVTPT));
                    break;
                }
            }
            TongThanhTien = ListNoiDung.Sum(u => u.ThanhTien ?? 0);
        }
    }
}
