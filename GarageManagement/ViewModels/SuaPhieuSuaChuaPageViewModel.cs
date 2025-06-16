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
        public bool IsInitializing { get; set; } = false;
        public bool IsLoadingDetails { get; set; }

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
            IsInitializing = true;
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
            await LoadPhieuSuaChuaAsync(phieuSuaChuaId);
            IsInitializing = false;
        }

        public async Task LoadPhieuSuaChuaAsync(Guid phieuId)
        {
            IsLoadingDetails = true;
            var phieu = await _phieuSuaService.GetByID(phieuId);
            if (phieu is null)
            {
                await Shell.Current.DisplayAlert("Thông báo", "Không tìm thấy phiếu sửa chữa", "OK");
                return;
            }
            NgaySuaChua = phieu.NgaySuaChua;
            var xe = await _xeService.GetByID(phieu.XeId);
            SelectedBienSoXe = xe?.BienSo ?? "";
            var chiTietList = await _chiTietPhieuSuaService.GetAll();
            var listND = chiTietList.Where(u => u.PhieuSuaChuaId == phieuId).ToList();
            foreach (var item in listND)
            {
                var tienCong = await _tienCongService.GetByID(item.TienCongId ?? Guid.Empty);
                var noiDung = await _noiDungSuaService.GetByID(item.NoiDungSuaChuaId ?? Guid.Empty);
                double sum = tienCong?.DonGiaLoaiTienCong ?? 0;
                item.NoiDungId = index++;
                item.GiaTienCong = item.TienCongApDung;
                item.isUpdatingNoiDungSuaChua = false;
                item.SelectedNoiDungSuaChua = ListNoiDungSuaChua.FirstOrDefault(x => x.Id == item.NoiDungSuaChuaId);
                item.ListSpecifiedVTPT = new ObservableCollection<VTPTChiTietPhieuSuaChua>();
                var vtptList = await _vtptChiTietPhieuSuaChuaService.GetAll();
                var listVTPT = vtptList.Where(v => v.ChiTietPhieuSuaChuaId == item.Id).ToList();
                foreach (var vt in listVTPT)
                {
                    var vtptObj = await _vatTuService.GetByID(vt.VatTuPhuTungId);
                    var vtpt = new VTPTChiTietPhieuSuaChua
                    {
                        IdForUI = item.NoiDungId ?? 0,
                        IdForDeleteUI = indexVTPT++,
                        VatTuPhuTungId = vt.VatTuPhuTungId,
                        SelectedVTPTId = vt.VatTuPhuTungId,
                        TenLoaiVatTuPhuTung = vtptObj?.TenLoaiVatTuPhuTung,
                        DonGia = vt.DonGiaVTPTApDung,
                        SoLuong = vt.SoLuong,
                        oldValue = vt.SoLuong
                    };
                    sum += (vtpt.DonGia ?? 0) * vtpt.SoLuong;
                    item.ListSpecifiedVTPT.Add(vtpt);
                }
            }
            ListNoiDung = new ObservableCollection<ChiTietPhieuSuaChua>(listND);
            TongThanhTien = phieu.TongTien;
            IsLoadingDetails = false;
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

            if (ListNoiDung == null || ListNoiDung.Count == 0)
            {
                await Shell.Current.DisplayAlert("Thông báo", "Không có nội dung sửa chữa", "OK");
                return;
            }

            if (NgaySuaChua.Date < DateTime.UtcNow.ToLocalTime().Date)
            {
                await Shell.Current.DisplayAlert("Thông báo", "Ngày sửa chữa không được nhỏ hơn ngày hiện tại!", "OK");
                return;
            }

            foreach (var item in ListNoiDung)
            {
                if (item.NoiDungSuaChuaId == null || item.TienCongId == null || item.ThanhTien == null)
                {
                    await Shell.Current.DisplayAlert("Thông báo", "Dữ liệu nội dung sửa chữa không hợp lệ", "OK");
                    return;
                }

                if (item.ListSpecifiedVTPT is not null)
                {
                    foreach (var vt in item.ListSpecifiedVTPT)
                    {
                        if (vt.SelectedVTPTId == null || vt.SoLuong <= 0)
                        {
                            await Shell.Current.DisplayAlert("Thông báo", "Vật tư phụ tùng không hợp lệ", "OK");
                            return;
                        }
                    }
                }
            }

            Xe xe = await _xeService.GetThroughtSpecialRoute($"BienSo/{SelectedBienSoXe}");
            if (xe is null)
            {
                await Shell.Current.DisplayAlert("Thông báo", "Không tìm thấy xe", "OK");
                return;
            }

            var khach = await _khachHangService.GetByID(xe.KhachHangId);
            if (khach is null)
            {
                await Shell.Current.DisplayAlert("Thông báo", "Không tìm thấy chủ xe", "OK");
                return;
            }

            var phieu = await _phieuService.GetByID(phieuSuaChuaId);
            if (phieu is null)
            {
                await Shell.Current.DisplayAlert("Thông báo", "Không tìm thấy phiếu sửa chữa", "OK");
                return;
            }

            // Hoàn tiền nợ cũ
            var oldXe = await _xeService.GetByID(phieu.XeId);
            if (oldXe != null) 
            { 
                oldXe.TienNo -= phieu.TongTien; 
                await _xeService.Update(oldXe); 
            }

            var oldKhach = await _khachHangService.GetByID(oldXe?.KhachHangId ?? Guid.Empty);
            if (oldKhach != null) 
            { 
                oldKhach.TienNo -= phieu.TongTien; 
                await _khachHangService.Update(oldKhach); 
            }

            // Cập nhật phiếu
            phieu.XeId = xe.Id;
            phieu.NgaySuaChua = NgaySuaChua;
            phieu.TongTien = TongThanhTien;
            await _phieuService.Update(phieu);

            // Xoá chi tiết cũ
            var listCT = await _chiTietPhieuSuaService.GetListOnSpecialRequirement($"GetListByPhieuSuaChuaID/{phieu.Id}");
            if (listCT is null) return;
            foreach (var ct in listCT)
            {
                var listVT = await _vtptChiTietPhieuSuaChuaService.GetListOnSpecialRequirement($"ChiTietPhieuSuaChuaId/{ct.Id}");
                if (listVT is null) return;
                foreach (var vt in listVT)
                {
                    var vtpt = await _vatTuService.GetByID(vt.VatTuPhuTungId);
                    if (vtpt != null)
                    {
                        vtpt.SoLuong += vt.SoLuong;
                        await _vatTuService.Update(vtpt);
                    }
                    await _vtptChiTietPhieuSuaChuaService.Delete(vt.Id);
                }
                await _chiTietPhieuSuaService.Delete(ct.Id);
            }

            // Tạo mới chi tiết
            foreach (var item in ListNoiDung)
            {
                var ct = await _chiTietPhieuSuaService.Create(new ChiTietPhieuSuaChua
                {
                    Id=Guid.NewGuid(),
                    TienCongApDung=item.GiaTienCong,
                    NoiDungSuaChuaId = item.NoiDungSuaChuaId,
                    PhieuSuaChuaId = phieu.Id,
                    TienCongId = item.TienCongId,
                    ThanhTien = item.ThanhTien
                });

                if (item.ListSpecifiedVTPT is null) return;

                foreach (var vt in item.ListSpecifiedVTPT)
                {
                    await _vtptChiTietPhieuSuaChuaService.Create(new VTPTChiTietPhieuSuaChua
                    {
                        Id = Guid.NewGuid(),
                        ChiTietPhieuSuaChuaId = ct.Id,
                        VatTuPhuTungId = vt.SelectedVTPTId ?? Guid.Empty,
                        SoLuong = vt.SoLuong,
                        DonGiaVTPTApDung=vt.DonGia??0
                    });

                    var vtpt = await _vatTuService.GetByID(vt.SelectedVTPTId ?? Guid.Empty);
                    if (vtpt != null)
                    {
                        vtpt.SoLuong -= vt.SoLuong;
                        await _vatTuService.Update(vtpt);
                    }
                }
            }

            // Cập nhật nợ mới
            khach.TienNo += TongThanhTien;
            xe.TienNo += TongThanhTien;
            await _khachHangService.Update(khach);
            await _xeService.Update(xe);

            await Toast.Make("Cập nhật phiếu sửa chữa thành công", CommunityToolkit.Maui.Core.ToastDuration.Short).Show();

            await LoadAsync();
            MessagingCenter.Send(this, "ReloadPhieuSuaChuaList", phieuSuaChuaId);
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
            ListNoiDung?.FirstOrDefault(u => u.NoiDungId == noiDungId)?.ListSpecifiedVTPT?.Add(new VTPTChiTietPhieuSuaChua
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
                var deleteItem = item.ListSpecifiedVTPT?.FirstOrDefault(u => u.IdForDeleteUI == id);
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
                    item.ListSpecifiedVTPT?.Remove(deleteItem);
                    item.OnPropertyChanged(nameof(item.ListSpecifiedVTPT));
                    break;
                }
            }
            TongThanhTien = ListNoiDung.Sum(u => u.ThanhTien ?? 0);
        }

        [RelayCommand]
        private void UpdateNoiDungSuaChua(int id)
        {
            foreach (var item in ListNoiDung)
            {
                if (item.NoiDungId == id)
                {
                    item.isUpdatingNoiDungSuaChua = !item.isUpdatingNoiDungSuaChua;
                    item.OnPropertyChanged(nameof(item.isUpdatingNoiDungSuaChua));
                    break;
                }
            }
        }

        [RelayCommand]
        private void UpdateVTPT(int id)
        {
            foreach (var item in ListNoiDung)
            {
                var editItem = item.ListSpecifiedVTPT?.FirstOrDefault(u => u.IdForDeleteUI == id);
                if (editItem is not null)
                {
                    editItem.isUpdatingVTPT = !editItem.isUpdatingVTPT;
                    editItem.OnPropertyChanged(nameof(editItem.isUpdatingVTPT));
                    break;
                }
            }
        }
    }
}
