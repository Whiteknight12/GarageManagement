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

        [ObservableProperty]
        private ObservableCollection<VTPTChiTietPhieuSuaChua> listVTPT=new ObservableCollection<VTPTChiTietPhieuSuaChua>();

        public TaoPhieuSuaChuaPageViewModel(APIClientService<Xe> carservice, 
            APIClientService<TienCong> tiencongservice,
            APIClientService<VatTuPhuTung> vattuservice,
            APIClientService<ChiTietPhieuSuaChua> noidungphieuservice,
            APIClientService<PhieuSuaChua> phieuservice,
            APIClientService<KhachHang> userservice,
            APIClientService<NoiDungSuaChua> noidungsuachuaService)
        {
            _carservice = carservice;
            _tiencongservice = tiencongservice;
            _vattuservice = vattuservice;
            _noidungphieuservice = noidungphieuservice;
            _phieuservice = phieuservice;
            _userservice = userservice;
            _noidungsuachuaService = noidungsuachuaService;
        }

        public async Task LoadAsync()
        {
            var listcar = await _carservice.GetAll();
            if (listcar is not null)
            {
                ListBienSoXe.Clear();
                foreach (var car in listcar)
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
                foreach (var vattu in listphutung) ListVatTuPhuTung.Add(vattu);
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
            if (ListVTPT.Count==0)
            {
                ListVTPT.Add(new VTPTChiTietPhieuSuaChua
                {
                    IdForUI=indexVTPT++
                });
            }
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
                await Shell.Current.DisplayAlert("Error", "Khong duoc bo trong bien so xe", "OK");
                return;
            }
            if (ListNoiDung.Count == 0)
            {
                await Shell.Current.DisplayAlert("Error", "Không có nội dung sữa chữa", "OK");
                return;
            }
            foreach (var item in ListNoiDung)
            {
                if (string.IsNullOrEmpty(item.NoiDungSuaChuaId.ToString()))
                {
                    await Shell.Current.DisplayAlert("Error", "Khong duoc bo trong noi dung!", "OK");
                    return;
                }
                if (item.TienCongId is null)
                {
                    await Shell.Current.DisplayAlert("Error", "Khong duoc bo trong tien cong!", "OK");
                    return;
                }
                if (item.ThanhTien is null)
                {
                    await Shell.Current.DisplayAlert("Error", "Co ve nhu co loi xay ra", "OK");
                    return;
                }
                if (NgaySuaChua.Date<DateTime.Now.Date)
                {
                    await Shell.Current.DisplayAlert("Error", "Ngay sua chua khong duoc nho hon ngay hien tai!", "OK");
                    return;
                }
            }
            Xe xe=await _carservice.GetThroughtSpecialRoute($"BienSo/{SelectedBienSoXe}");
            if (xe is null)
            {
                await Shell.Current.DisplayAlert("Error", "Khong tim thay xe co bien so tren", "OK");
                return;
            }
            var checkuser = await _userservice.GetByID(xe.KhachHangId);
            if (checkuser is null)
            {
                await Shell.Current.DisplayAlert("Error", "Khong tim chu xe cua xe co bien so tren", "OK");
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
                await Shell.Current.DisplayAlert("Error", "Tao phieu sua chua moi that bai", "OK");
                return;
            }
            foreach (var item in ListNoiDung)
            {
                var chiTietPhieuSuaChua=await _noidungphieuservice.Create(new ChiTietPhieuSuaChua
                {
                    NoiDungSuaChuaId=item.NoiDungSuaChuaId,
                    PhieuSuaChuaId = obj.Id,
                    TienCongId = item.TienCongId,
                    ThanhTien = item.ThanhTien
                });
                //var vatTuPhuTung = await _vattuservice.GetByID(item.VatTuPhuTungId??Guid.Empty);
                //if (vatTuPhuTung != null) vatTuPhuTung.SoLuong -= item.SoLuong ?? 0;
                //await _vattuservice.Update(vatTuPhuTung);
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
        }

        [RelayCommand]
        public void Remove(int itemid)
        {
            var item=ListNoiDung.Where(u=>u.NoiDungId == itemid).FirstOrDefault();
            if (item != null) ListNoiDung.Remove(item);
            TongThanhTien=ListNoiDung.Sum(u=>u.ThanhTien ?? 0);
            OnPropertyChanged(nameof(TongThanhTien));
        }

        [RelayCommand]
        public void AddVTPT()
        {
            ListVTPT.Add(new VTPTChiTietPhieuSuaChua
            {
                IdForUI = indexVTPT++
            });
        }

        [RelayCommand]
        public void RemoveVTPT(int id)
        {
            var item = ListVTPT.Where(u => u.IdForUI == id).FirstOrDefault();
            if (item != null) ListVTPT.Remove(item);
        }
    }
}
