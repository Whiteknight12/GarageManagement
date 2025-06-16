using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace GarageManagement.ViewModels
{
    public partial class TaoPhieuSuaChuaPageViewModel: BaseViewModel
    {
        private int index = 0;
        private int indexVTPT = 0;

        private readonly APIClientService<Xe> _carservice;
        private readonly APIClientService<TienCong> _tiencongservice;
        private readonly APIClientService<VatTuPhuTung> _vattuservice;
        private readonly APIClientService<ChiTietPhieuSuaChua> _noidungphieuservice;
        private readonly APIClientService<PhieuSuaChua> _phieuservice;
        private readonly APIClientService<KhachHang> _userservice;
        private readonly APIClientService<NoiDungSuaChua> _noidungsuachuaService;
        private readonly APIClientService<VTPTChiTietPhieuSuaChua> _vtptChiTietPhieuSuaChuaService;

        private readonly APIClientService<ThamSo> _thamSoService;
        private readonly APIClientService<PhieuTiepNhan> _phieuTiepNhanService;
        public delegate void OnPhieuSuaChuaAddedDelegate(PhieuSuaChua phieuSuaChua);
        
        public OnPhieuSuaChuaAddedDelegate OnPhieuSuaChuaAdded { get; set; }

        [ObservableProperty]
        private ObservableCollection<string> listBienSoXe=new ObservableCollection<string>();

        [ObservableProperty]
        private string selectedBienSoXe;

        [ObservableProperty]
        private DateTime ngaySuaChua=DateTime.Now;

        [ObservableProperty]
        private ObservableCollection<ChiTietPhieuSuaChua> listNoiDung = new ObservableCollection<ChiTietPhieuSuaChua>();

        [ObservableProperty]
        public ObservableCollection<TienCong> listTienCong=new ObservableCollection<TienCong>();

        [ObservableProperty]
        private ObservableCollection<VatTuPhuTung> listVatTuPhuTung=new ObservableCollection<VatTuPhuTung>();

        [ObservableProperty]
        private double tongThanhTien=0;

        [ObservableProperty]
        private ObservableCollection<NoiDungSuaChua> listNoiDungSuaChua=new ObservableCollection<NoiDungSuaChua>();

        public TaoPhieuSuaChuaPageViewModel(APIClientService<Xe> carservice, 
            APIClientService<TienCong> tiencongservice,
            APIClientService<VatTuPhuTung> vattuservice,
            APIClientService<ChiTietPhieuSuaChua> noidungphieuservice,
            APIClientService<PhieuSuaChua> phieuservice,
            APIClientService<KhachHang> userservice,
            APIClientService<NoiDungSuaChua> noidungsuachuaService,
            APIClientService<VTPTChiTietPhieuSuaChua> vtptChiTietPhieuSuaChuaService, 
            APIClientService<PhieuTiepNhan> phieuTiepNhanService,
            APIClientService<ThamSo> thamSoService
   )
        {
            _carservice = carservice;
            _tiencongservice = tiencongservice;
            _vattuservice = vattuservice;
            _noidungphieuservice = noidungphieuservice;
            _phieuservice = phieuservice;
            _userservice = userservice;
            _noidungsuachuaService = noidungsuachuaService;
            _vtptChiTietPhieuSuaChuaService = vtptChiTietPhieuSuaChuaService;
            _phieuTiepNhanService = phieuTiepNhanService; 
            _thamSoService = thamSoService;
        }

        public async Task LoadAsync()
        {
            //double mul;
            var tile = await _thamSoService.GetThroughtSpecialRoute("TiLeDonGiaBan");
            //mul = tile.GiaTri;
            var listcar = await _carservice.GetAll();
            var listPhieu = await _phieuTiepNhanService.GetAll();
            var pl = listPhieu.Where(p => p.DaHoanThanhBaoTri == false);
            var ps = pl.Select(p => p.XeId).ToList();
            var lc = new List<Xe>(); 
            foreach(var p in ps)
            {
                var xe = await _carservice.GetByID(p);
                if (xe is not null) lc.Add(xe);
            }
            
            if (lc is not null)
            {
                ListBienSoXe.Clear();
                foreach (var car in lc)
                {
                    ListBienSoXe.Add(car.BienSo);
                }
                OnPropertyChanged(nameof(ListBienSoXe));
            }
            var listcong = await _tiencongservice.GetAll();
            if (listcong is not null)
            {
                ListTienCong.Clear();
                foreach (var tiencong in listcong) ListTienCong.Add(tiencong);
                OnPropertyChanged(nameof(ListTienCong));
            }
            var listphutung = await _vattuservice.GetAll();
            if (listphutung is not null)
            {
                ListVatTuPhuTung.Clear();
                foreach (var vattu in listphutung)
                {
                    ListVatTuPhuTung.Add(vattu);
                }
                OnPropertyChanged(nameof(ListVatTuPhuTung));
            }
            var listNoiDungSuaChua = await _noidungsuachuaService.GetAll();
            if (listNoiDungSuaChua is not null)
            {
                ListNoiDungSuaChua.Clear();
                foreach (var item in listNoiDungSuaChua) ListNoiDungSuaChua.Add(item);
                OnPropertyChanged(nameof(ListNoiDungSuaChua));
            }
            if (ListNoiDung.Count==0)
            {
                ListNoiDung.Add(new ChiTietPhieuSuaChua
                {
                    NoiDungId=index++
                });
            }

            _allBienSo = ListBienSoXe.ToList();
            ApplyBienSoFilter();
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
            Xe xe=await _carservice.GetThroughtSpecialRoute($"BienSo/{SelectedBienSoXe}");
            if (xe is null)
            {
                await Shell.Current.DisplayAlert("Thông báo", "Không tìm thấy xe có biển số xe trên", "OK");
                return;
            }
            var checkuser = await _userservice.GetByID(xe.KhachHangId);
            if (checkuser is null)
            {
                await Shell.Current.DisplayAlert("Thông báo", "Không tìm thấy chủ xe có biển số xe trên", "OK");
                return;
            }
            PhieuSuaChua obj=await _phieuservice.Create(new PhieuSuaChua
            {
                Id=Guid.NewGuid(),
                XeId= xe.Id, 
                NgaySuaChua = NgaySuaChua,
                TongTien=TongThanhTien
            });
            if (obj is null)
            {
                await Shell.Current.DisplayAlert("Thông báo", "Tạo phiếu sữa chữa mới thất bại", "OK");
                return;
            }
            foreach (var item in ListNoiDung)
            {
                var tienCong = await _tiencongservice.GetByID(item.TienCongId??Guid.Empty);
                var chiTietPhieuSuaChua=await _noidungphieuservice.Create(new ChiTietPhieuSuaChua
                {
                    NoiDungSuaChuaId=item.NoiDungSuaChuaId,
                    PhieuSuaChuaId = obj.Id,
                    TienCongId = item.TienCongId,
                    TienCongApDung=tienCong?.DonGiaLoaiTienCong ?? 0,
                    ThanhTien = item.ThanhTien
                });
                if (chiTietPhieuSuaChua is null)
                {
                    await Shell.Current.DisplayAlert("Thông báo", "Có lỗi xảy ra khi tạo chi tiết phiếu sữa chữa", "OK");
                    return;
                }
                if (item.ListSpecifiedVTPT is not null)
                {
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
                        var tmp_vtpt = await _vattuservice.GetByID(chiTietVTPT.SelectedVTPTId ?? Guid.Empty);
                        var result = await _vtptChiTietPhieuSuaChuaService.Create(new VTPTChiTietPhieuSuaChua
                        {
                            Id = Guid.NewGuid(),
                            ChiTietPhieuSuaChuaId = chiTietPhieuSuaChua.Id,
                            VatTuPhuTungId = chiTietVTPT.SelectedVTPTId ?? Guid.Empty,
                            DonGiaVTPTApDung = tmp_vtpt?.DonGiaBanLoaiVatTuPhuTung ?? 0,
                            SoLuong = chiTietVTPT.SoLuong
                        });
                        if (result is null)
                        {
                            await Shell.Current.DisplayAlert("Thông báo", "Có lỗi xảy ra", "OK");
                            return;
                        }
                        var vtpt = await _vattuservice.GetByID(chiTietVTPT.SelectedVTPTId ?? Guid.Empty);
                        if (vtpt is not null)
                        {
                            vtpt.SoLuong -= chiTietVTPT.SoLuong;
                            await _vattuservice.Update(vtpt);
                        }
                    }
                }
            }
            checkuser.TienNo += TongThanhTien;
            xe.TienNo += TongThanhTien;
            await _userservice.Update(checkuser);
            await _carservice.Update(xe);
            var toast = Toast.Make("Thêm phiếu sữa chữa mới thành công", CommunityToolkit.Maui.Core.ToastDuration.Short);
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
            ListNoiDung?.FirstOrDefault(u => u.NoiDungId == noiDungId)?.ListSpecifiedVTPT?.Add(new VTPTChiTietPhieuSuaChua
            {
                IdForUI=noiDungId,
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
                        updateItem.ThanhTien=updateItem.ListSpecifiedVTPT?.Sum(u => u.SoLuong * u.DonGia);
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

        // lưu nguyên danh sách
        private List<string> _allBienSo = new();

        [ObservableProperty]
        private string filterBienSo = string.Empty;
        partial void OnFilterBienSoChanged(string value) => ApplyBienSoFilter();

        [ObservableProperty]
        private ObservableCollection<string> filteredBienSo = new();

        private void ApplyBienSoFilter()
        {
            var q = string.IsNullOrWhiteSpace(FilterBienSo)
                ? _allBienSo
                : _allBienSo.Where(bs => bs.Contains(FilterBienSo, StringComparison.OrdinalIgnoreCase));
            FilteredBienSo = new ObservableCollection<string>(q);
        }

    }
}
