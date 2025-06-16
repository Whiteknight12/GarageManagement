using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GarageManagement.Services;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace GarageManagement.ViewModels
{
    public partial class QuanLiNhanVienPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<NhanVien> listNhanVien = new();    

        private List<NhanVien> _allNhanVien = new();

        [ObservableProperty] private string cccdFilter = string.Empty;
        [ObservableProperty] private string nameFilter = string.Empty;
        [ObservableProperty] private string phoneFilter = string.Empty;

        [ObservableProperty] private List<string> listGioiTinh = new List<string> { "Nam", "Nữ" };
        partial void OnCccdFilterChanged(string _) => ApplyFilter();
        partial void OnNameFilterChanged(string _) => ApplyFilter();
        partial void OnPhoneFilterChanged(string _) => ApplyFilter();

        [ObservableProperty]
        private bool isDeleteMode;
        [ObservableProperty]
        private NhanVien? selectedNhanVien;
        [ObservableProperty]
        private bool isDetailPaneVisible;
        [ObservableProperty]
        private bool isEditing;
        [ObservableProperty] private bool isNotEditing;

        private readonly APIClientService<NhanVien> _nhanVienService;
        private readonly AuthenticationService _authenticationService;

        public QuanLiNhanVienPageViewModel(APIClientService<NhanVien> nhanVienService,
            ILogger<QuanLiNhanVienPageViewModel> logger,
            AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _nhanVienService = nhanVienService;
            //_ = LoadAsync();
            IsDeleteMode = false;
            IsDetailPaneVisible = false;
            IsEditing = false;
            IsNotEditing = true;
            MessagingCenter.Subscribe<ChiTietNhanVienPageViewModel>(
                    this, "ReloadNhanVienList",
                    async _ => await LoadAsync());
        }

        partial void OnIsEditingChanged(bool value) => IsNotEditing = !value;

        public async Task LoadAsync()
        {
            _ = _authenticationService.FettaiKhoanSession();
            var httpClient = _nhanVienService.GetHttpClient;
            var token = _authenticationService.GetCurrentAccountStatus.Token;
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var list = await _nhanVienService.GetAll();

            foreach (var nv in list)
            {
                nv.STT = list.IndexOf(nv) + 1;
            }
            _allNhanVien = list;
            ApplyFilter();
            ListNhanVien = new(_allNhanVien);
        }

        private void ApplyFilter()
        {
            IEnumerable<NhanVien> query = _allNhanVien;

            if (!string.IsNullOrWhiteSpace(CccdFilter))
            {
                var key = CccdFilter.Trim().ToLower();
                query = query.Where(nv => (nv.CCCD ?? "").ToLower().Contains(key));
            }
            if (!string.IsNullOrWhiteSpace(NameFilter))
            {
                var key = NameFilter.Trim().ToLower();
                query = query.Where(nv => (nv.HoTen ?? "").ToLower().Contains(key));
            }
            if (!string.IsNullOrWhiteSpace(PhoneFilter))
            {
                var key = PhoneFilter.Trim().ToLower();
                query = query.Where(nv => (nv.SoDienThoai ?? "").ToLower().Contains(key));
            }

            ListNhanVien = new ObservableCollection<NhanVien>(query);
        }

        public void Load(NhanVien item)
        {
            ListNhanVien.Add(item);
            for (int i = 0; i < ListNhanVien.Count; i++)
            {
                ListNhanVien[i].STT = i + 1;
            }
        }

        [RelayCommand]
        private void Add()
        {
            // Tạo bản ghi rỗng để binding ra Entry
            SelectedNhanVien = new NhanVien
            {
                Id = Guid.Empty
            };

            // Mở pane + bật chế độ chỉnh sửa
            IsDetailPaneVisible = true;
            IsEditing = true;   // OnIsEditingChanged sẽ tự gán IsNotEditing = false
            IsNotEditing = false;
        }


        [RelayCommand]
        private void EditDetail()
        {
            if (SelectedNhanVien == null) return;
            IsEditing = true;
        }

        [RelayCommand]
        private async Task SaveDetail()
        {
            if (SelectedNhanVien == null) return;

            if (string.IsNullOrWhiteSpace(SelectedNhanVien.CCCD))
            {
                await Shell.Current.DisplayAlert("Lỗi", "Vui lòng nhập CCCD", "OK");
                return;
            }

            // 1. Validate CCCD chỉ số
            var cccdPattern = new Regex(@"^\d+$");
            if (!cccdPattern.IsMatch(SelectedNhanVien.CCCD))
            {
                await Shell.Current.DisplayAlert("Lỗi", "CCCD chỉ được chứa chữ số", "OK");
                return;
            }

            // 2. Check bắt buộc nhập họ tên
            if (string.IsNullOrWhiteSpace(SelectedNhanVien.HoTen))
            {
                await Shell.Current.DisplayAlert("Lỗi", "Vui lòng nhập họ và tên", "OK");
                return;
            }
            // 3. Validate họ tên
            var namePattern = new Regex(@"^[\p{L}\s]+$");
            if (!namePattern.IsMatch(SelectedNhanVien.HoTen))
            {
                await Shell.Current.DisplayAlert("Lỗi", "Họ và tên chỉ được chứa chữ cái", "OK");
                return;
            }

            // 4. Tuổi
            if (string.IsNullOrWhiteSpace(SelectedNhanVien.Tuoi.ToString()))
            {
                await Shell.Current.DisplayAlert("Lỗi", "Vui lòng nhập tuổi", "OK");
                return;
            }
            if (!int.TryParse(SelectedNhanVien.Tuoi.ToString(), out var tuoiParsed) || tuoiParsed <= 0)
            {
                await Shell.Current.DisplayAlert("Lỗi", "Tuổi phải là số nguyên dương", "OK");
                return;
            }

            // 5. Giới tính
            if (string.IsNullOrWhiteSpace(SelectedNhanVien.GioiTinh))
            {
                await Shell.Current.DisplayAlert("Lỗi", "Vui lòng chọn giới tính", "OK");
                return;
            }

            // 6. SĐT
            if (string.IsNullOrWhiteSpace(SelectedNhanVien.SoDienThoai))
            {
                await Shell.Current.DisplayAlert("Lỗi", "Vui lòng nhập số điện thoại", "OK");
                return;
            }
            var phonePattern = new Regex(@"^\d{10,11}$");
            if (!phonePattern.IsMatch(SelectedNhanVien.SoDienThoai))
            {
                await Shell.Current.DisplayAlert("Lỗi", "Số điện thoại phải từ 10–11 chữ số", "OK");
                return;
            }

            // 7. Email
            if (string.IsNullOrWhiteSpace(SelectedNhanVien.Email))
            {
                await Shell.Current.DisplayAlert("Lỗi", "Vui lòng nhập email", "OK");
                return;
            }
            var emailPattern = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!emailPattern.IsMatch(SelectedNhanVien.Email))
            {
                await Shell.Current.DisplayAlert("Lỗi", "Email không đúng định dạng", "OK");
                return;
            }

            // Nếu pass hết thì tạo mới hoặc cập nhật
            if (SelectedNhanVien.Id == Guid.Empty)
            {
                await _nhanVienService.Create(SelectedNhanVien);
            }
            else
            {
                await _nhanVienService.Update(SelectedNhanVien);
            }
            var toast = Toast.Make("Lưu nhân viên thành công", CommunityToolkit.Maui.Core.ToastDuration.Short);
            await toast.Show();

            // Cập nhật lại STT và trạng thái editing
            for (int i = 0; i < ListNhanVien.Count; i++)
                ListNhanVien[i].STT = i + 1;

            IsEditing = false;
            IsNotEditing = true;
            CloseDetailPane();
            await LoadAsync();
        }


        [RelayCommand]
        private void ToggleDeleteMode()
        {
            IsDeleteMode = !IsDeleteMode;
            if (!IsDeleteMode)
            {
                var updatedList = new List<NhanVien>(ListNhanVien);
                foreach (var nv in updatedList)
                {
                    nv.IsSelected = false;
                }
                ListNhanVien = new ObservableCollection<NhanVien>(updatedList);
            }
        }

        [RelayCommand]
        private async Task Delete()
        {
            var selectedItems = ListNhanVien.Where(x => x.IsSelected).ToList();
            foreach (var item in selectedItems)
            {
                await _nhanVienService.Delete(item.Id);
                ListNhanVien.Remove(item);
            }
            for (int i = 0; i < ListNhanVien.Count; i++)
            {
                ListNhanVien[i].STT = i + 1;
            }
            IsDeleteMode = false;
            IsDetailPaneVisible = false;
        }

        [RelayCommand]
        private void ViewDetailNhanVien(Guid id)
        {
            SelectedNhanVien = ListNhanVien.FirstOrDefault(nv => nv.Id == id);
            IsDetailPaneVisible = SelectedNhanVien != null;
            MessagingCenter.Send(this, "ViewChiTietNhanVien", id);
        }

        [RelayCommand]
        private void CloseDetailPane()
        {
            IsDetailPaneVisible = false;
            SelectedNhanVien = null;
            if (IsEditing == true && IsNotEditing == false)
            {
                IsEditing = false;
                IsNotEditing = true; 
            }
        }

        [RelayCommand]
        private void GoToChiTietNhanVienPage(Guid Id)
        {
            MessagingCenter.Send(this, "ShowStaffDetails", Id);
        }

    }
}   