using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GarageManagement.ViewModels
{
    public partial class ChinhSuaPhieuThuTienPageViewModel : BaseViewModel
    {

        private double _originalSoTienThu;
        // dữ liệu gốc
        [ObservableProperty] private PhieuThuTien selectedPhieu;

        [ObservableProperty] private ObservableCollection<KhachHang> listChuXe;
        [ObservableProperty] private KhachHang selectedChuXe;
        [ObservableProperty] private ObservableCollection<Xe> listBienSo;
        [ObservableProperty] private Xe selectedBienSo;
        [ObservableProperty] private string dienThoai;
        [ObservableProperty] private string email;
        [ObservableProperty] private DateTime ngayThuTien;
        [ObservableProperty] private string soTienThu;
        [ObservableProperty] private string tenHieuXe;
        [ObservableProperty] private string tienNoXeSelected;
        [ObservableProperty] private bool isCustomerFound;
        [ObservableProperty]
        private string cCCD;
        [ObservableProperty]
        private string gioiTinh;

        // lọc
        [ObservableProperty]
        private ObservableCollection<string> filterFields = new ObservableCollection<string> { "CCCD", "Tên", "SĐT", "Email" };
        [ObservableProperty] private string selectedFilterField;
        [ObservableProperty] private string filterValue;

        // services
        private readonly APIClientService<PhieuThuTien> _phieuService;
        private readonly APIClientService<KhachHang> _khService;
        private readonly APIClientService<Xe> _xeService;
        private readonly APIClientService<HieuXe> _hieuXeService;
        private readonly APIClientService<ThamSo> _thamSoService; 
        public bool available { get; set; } = false;
        public ChinhSuaPhieuThuTienPageViewModel(
            APIClientService<PhieuThuTien> phieuService,
            APIClientService<KhachHang> khService,
            APIClientService<Xe> xeService,
            APIClientService<HieuXe> hieuXeService,
            APIClientService<ThamSo> thamSoService)
        {
            _phieuService = phieuService;
            _khService = khService;
            _xeService = xeService;
            _hieuXeService = hieuXeService;
            NgayThuTien = DateTime.Now;
            SelectedFilterField = "CCCD";
            _thamSoService = thamSoService;
        }

        public async Task LoadAsync(Guid id)
        {
            // 1) load phiếu
            var pt = await _phieuService.GetByID(id);
            if (pt == null)
            {
                await Shell.Current.DisplayAlert("Thông báo", "Không tìm thấy phiếu thu tiền", "OK");
                return;
            }

            SelectedPhieu = pt;

            // 2) load danh sách khách hàng để chọn (giống ThuTienPage)
            var kus = await _khService.GetAll();
            if (kus is not null) ListChuXe = new ObservableCollection<KhachHang>(kus);
            else kus = new List<KhachHang>();

            // 3) chọn lại KH hiện tại
            SelectedChuXe = kus.Find(k => k.Id == pt.KhachHangId) ?? new KhachHang() { 
                SoDienThoai="", 
                Email="" };
            DienThoai = SelectedChuXe?.SoDienThoai ?? "";
            Email = SelectedChuXe?.Email ?? "";

            // 4) lấy xe của KH
            var xeList = await _xeService.GetListOnSpecialRequirement($"PhoneNumber/{DienThoai}");
            if (xeList is not null) ListBienSo = new ObservableCollection<Xe>(xeList);
            else xeList = new List<Xe>();
            SelectedBienSo = xeList.Find(x => x.Id == pt.XeId) ?? new Xe()
            {
                //if we can't find the car, then it's empty 
            };

            // 5) load thông tin nợ/hiệu xe
            var hx = await _hieuXeService.GetByID(SelectedBienSo.HieuXeId);
            TenHieuXe = hx?.TenHieuXe ?? "";
            TienNoXeSelected = SelectedBienSo.TienNo.ToString() ?? "";
            _originalSoTienThu = pt.SoTienThu;
            // 6) hiển thị ngày & số tiền
            NgayThuTien = pt.NgayThuTien;
            SoTienThu = pt.SoTienThu.ToString();
            IsCustomerFound = true;
        }

        [RelayCommand]
        private async Task Filter()
        {
            available = true;
            if (string.IsNullOrEmpty(FilterValue))
            {
                await Shell.Current.DisplayAlert("Error", "Vui lòng nhập giá trị để lọc", "OK");
                return;
            }

            IsCustomerFound = false;
            SelectedChuXe = null;
            ListBienSo.Clear();

            var filtered = ListChuXe.FirstOrDefault(kh =>
            {
                return SelectedFilterField switch
                {
                    "CCCD" => kh.CCCD?.ToLower().TrimStart().TrimEnd().Contains(FilterValue.ToLower().TrimStart().TrimEnd()) ?? false,
                    "Tên" => kh.HoVaTen?.ToLower().TrimStart().TrimEnd().Contains(FilterValue.ToLower().TrimStart().TrimEnd()) ?? false,
                    "Số điện thoại" => kh.SoDienThoai?.ToLower().TrimStart().TrimEnd().Contains(FilterValue.ToLower().TrimStart().TrimEnd()) ?? false,
                    "Email" => kh.Email?.ToLower().TrimStart().TrimEnd().Contains(FilterValue.ToLower().TrimStart().TrimEnd()) ?? false,
                    _ => false
                };
            });

            if (filtered != null)
            {
                SelectedChuXe = filtered;
                DienThoai = filtered.SoDienThoai;
                Email = filtered.Email ?? "";
                CCCD = filtered.CCCD;
                GioiTinh = filtered.GioiTinh;
                var xeListBySDT = await _xeService.GetListOnSpecialRequirement($"PhoneNumber/{filtered.SoDienThoai}");
                if (xeListBySDT != null && xeListBySDT.Count != 0)
                {
                    SelectedBienSo = xeListBySDT[0];
                    ListBienSo.Clear();
                    available = false;
                    ListBienSo = new ObservableCollection<Xe>(xeListBySDT);
                }
                else
                {
                    await Shell.Current.DisplayAlert("Thông báo", "Khách hàng trên không có xe trong gara, không thể thu tiền", "OK");
                    return;
                }
                IsCustomerFound = true;
                SelectedBienSo = ListBienSo[0];
                var toast = Toast.Make("Đã lọc thông tin khách hàng và xe liên quan", CommunityToolkit.Maui.Core.ToastDuration.Short);
                await toast.Show();
            }
            else
            {
                await Shell.Current.DisplayAlert("Thông báo", "Không tìm thấy khách hàng", "OK");
            }
        }

        [RelayCommand]
        private async Task Update()
        {
            if (string.IsNullOrEmpty(Email))
            {
                await Shell.Current.DisplayAlert("Error", "Không được bỏ trống email", "OK");
                return;
            }
            if (SelectedChuXe is null)
            {
                await Shell.Current.DisplayAlert("Error", "Không được bỏ trống tên khách hàng", "OK");
                return;
            }
            if (SelectedBienSo is null)
            {
                await Shell.Current.DisplayAlert("Error", "Không được bỏ trống biển số xe", "OK");
                return;
            }
            if (string.IsNullOrEmpty(DienThoai))
            {
                await Shell.Current.DisplayAlert("Error", "Không được bỏ trống số điện thoại", "OK");
                return;
            }
            if (string.IsNullOrEmpty(SoTienThu))
            {
                await Shell.Current.DisplayAlert("Error", "Không được bỏ trống số tiền thu", "OK");
                return;
            }
            else
            {
                if (!SoTienThu.All(char.IsDigit))
                {
                    await Shell.Current.DisplayAlert("Error", "Số tiền thu chỉ được chứa số", "OK");
                    return;
                }
                if (double.Parse(SoTienThu) <= 0)
                {
                    await Shell.Current.DisplayAlert("Error", "Số tiền thu phải lớn hơn 0", "OK");
                    return;
                }
            }
            if (double.Parse(SoTienThu) == _originalSoTienThu)
            {
                await Shell.Current.DisplayAlert("Error", "Số tiền thu mới phải khác số tiền thu cũ", "OK");
                return;
            }
            var ts = await _thamSoService.GetThroughtSpecialRoute("VuotSoTienNo"); 
            if(ts.GiaTri == 0f && ts.GiaTri != 1f)
            {
                if (double.Parse(SoTienThu) > SelectedBienSo.TienNo)
                {
                    await Shell.Current.DisplayAlert("Error", "Không được thu nhiều tiền hơn số tiền nợ của xe", "OK");
                    return;
                }
            }
            
            if (string.IsNullOrEmpty(SelectedChuXe.Email) || SelectedChuXe.Email != Email)
            {
                SelectedChuXe.Email = Email;
                await _khService.Update(SelectedChuXe);
            }

            // parse số tiền mới
            var newThu = double.Parse(SoTienThu);

            // bù nợ: add lại phần đã trừ cũ, rồi trừ phần mới
            var diff = _originalSoTienThu - newThu;
            SelectedChuXe.TienNo += diff;
            SelectedBienSo.TienNo += diff;

            // cập nhật dữ liệu lên server
            if (SelectedChuXe.Email != Email)
            {
                SelectedChuXe.Email = Email;
                await _khService.Update(SelectedChuXe);
            }

            SelectedPhieu.XeId = SelectedBienSo.Id;
            SelectedPhieu.NgayThuTien = NgayThuTien;
            SelectedPhieu.KhachHangId = SelectedChuXe.Id;
            SelectedPhieu.SoTienThu = newThu;
            await _phieuService.Update(SelectedPhieu);

            await _xeService.Update(SelectedBienSo);
            await _khService.Update(SelectedChuXe);

            // sau khi update thành công, nếu muốn cho phép edit tiếp:
            _originalSoTienThu = newThu;

            SoTienThu = string.Empty;
            TienNoXeSelected = SelectedBienSo.TienNo.ToString();

            var toast = Toast.Make("Thông tin phiếu thu đã được cập nhật.", CommunityToolkit.Maui.Core.ToastDuration.Short);
            await toast.Show();
            MessagingCenter.Send(this, "PhieuThuTienUpdated");
        }

        [RelayCommand]
        private async Task GoBack()
        {
            await Shell.Current.Navigation.PopAsync();
        }

        partial void OnSelectedBienSoChanged(Xe value)
        {
            if (value == null)
                return;

            // gọi bất đồng bộ nhưng không chặn UI
            _ = RefreshVehicleInfoAsync(value);
        }
        private async Task RefreshVehicleInfoAsync(Xe xe)
        {
            // 1) Load lại thông tin hiệu xe
            var hx = await _hieuXeService.GetByID(xe.HieuXeId);
            TenHieuXe = hx?.TenHieuXe ?? "";

            // 2) Cập nhật lại tiền nợ của xe
            TienNoXeSelected = xe.TienNo.ToString() ?? "";
        }
    }
}
