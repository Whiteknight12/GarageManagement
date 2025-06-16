using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GarageManagement.Pages;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace GarageManagement.ViewModels
{
    public partial class LoaiTienCongPageViewModel : BaseViewModel
    {
        private readonly APIClientService<NoiDungSuaChua> _noiDungService;
        private List<TienCong> _allTienCong = new();

        [ObservableProperty] private ObservableCollection<TienCong> listTienCong = new();

        [ObservableProperty] private string nameFilter = string.Empty;
        [ObservableProperty] private string priceFromText = string.Empty;
        [ObservableProperty] private string priceToText = string.Empty;

        partial void OnNameFilterChanged(string _) => ApplyFilter();
        partial void OnPriceFromTextChanged(string _) => ApplyFilter();
        partial void OnPriceToTextChanged(string _) => ApplyFilter();

        

        [ObservableProperty]
        private bool isDeleteMode;

        private readonly APIClientService<TienCong> _tienCongService;
        private readonly ThemLoaiTienCongPageViewModel _themLoaiTienCongPageViewModel;

        public LoaiTienCongPageViewModel(APIClientService<TienCong> tienCongService,
                                         ThemLoaiTienCongPageViewModel themLoaiTienCongPageViewModel,
                                         APIClientService<NoiDungSuaChua> noiDungService)
        {
            _tienCongService = tienCongService;
            _themLoaiTienCongPageViewModel = themLoaiTienCongPageViewModel;
            IsDeleteMode = false;
            _ = LoadAsync();
            _noiDungService = noiDungService;
        }

        public async Task LoadAsync()
        {
            // 1. Lấy toàn bộ tiền công
            _allTienCong = await _tienCongService.GetAll() ?? new();

            // 2. Lấy toàn bộ nội dung sửa chữa và build map
            var ndList = await _noiDungService.GetAll();
            var ndDict = ndList.ToDictionary(x => x.Id, x => x.TenNoiDungSuaChua);

            // 3. Gán vào mỗi bản ghi
            foreach (var tc in _allTienCong)
            {
                if (ndDict.TryGetValue(tc.NoiDungSuaChuaId, out var ten))
                    tc.TenNoiDungSuaChua = ten;
                else
                    tc.TenNoiDungSuaChua = "(Không xác định)";
            }

            ApplyFilter();
        }

        /* ========== LOCAL ADD (khi tạo mới) ========== */
        public void Load(TienCong item)
        {
            if (item == null) return;
            if (_allTienCong.Any(x => x.Id == item.Id)) return;

            _allTienCong.Add(item);
            ApplyFilter();
        }

        /* ========== FILTER ========== */
        private void ApplyFilter()
        {
            IEnumerable<TienCong> q = _allTienCong;

            /* ---- theo tên ---- */
            if (!string.IsNullOrWhiteSpace(NameFilter))
            {
                var key = NameFilter.Trim().ToLower();
                q = q.Where(t => (t.TenLoaiTienCong ?? "")
                                 .ToLower()
                                 .Contains(key));
            }

            /* ---- theo giá ---- */
            if (double.TryParse(PriceFromText, out var minPrice))
                q = q.Where(t => t.DonGiaLoaiTienCong >= minPrice);

            if (double.TryParse(PriceToText, out var maxPrice))
                q = q.Where(t => t.DonGiaLoaiTienCong <= maxPrice);

            ListTienCong = new ObservableCollection<TienCong>(q);
        }

        //[RelayCommand]
        //public void AddNewLoaiTienCong(ContentView page)
        //{
        //    if (page is LoaiTienCongPage loaiTienCongPage)
        //    {
        //        var themLoaiTienCongPage = new ThemLoaiTienCongPage(new ThemLoaiTienCongPageViewModel());
        //        MessagingCenter.Send(this, "ShowRightPane", themLoaiTienCongPage);
        //    }
        //}

        [RelayCommand]
        private void Add()
        {
            var newWindow = new Window
            {
                Page = new ThemLoaiTienCongPage(_themLoaiTienCongPageViewModel),
                MaximumHeight = 400,
                MaximumWidth = 400,
                MinimumHeight = 400,
                MinimumWidth = 400
            };

            _themLoaiTienCongPageViewModel.OnTienCongAdded = (TienCong tienCong) =>
            {
                Load(tienCong);
            };

            Application.Current.OpenWindow(newWindow);
        }

        [RelayCommand]
        private void Edit()
        {
            // TODO: Implement logic to edit selected item (gợi ý: chỉ cho phép 1 item được chọn)
        }

        [RelayCommand]
        private void ToggleDeleteMode()
        {
            IsDeleteMode = !IsDeleteMode;
        }

        [RelayCommand]
        private async Task DeleteAsync()
        {
            var selected = ListTienCong.Where(x => x.IsSelected).ToList();
            foreach (var t in selected)
            {
                await _tienCongService.Delete(t.Id);
                _allTienCong.Remove(t);
            }
            IsDeleteMode = false;
            ApplyFilter();
        }

        [RelayCommand]
        private void GoToChiTietTienCongPage(Guid id)
        {
            // TODO: Navigate to ChiTietTienCongPage and pass the selected item id
        }

        [RelayCommand]
        private void GoBack()
        {
            Shell.Current.GoToAsync("..");
        }

        [ObservableProperty] private TienCong selectedTienCong;
        [ObservableProperty] private bool isDetailPaneVisible;
        [ObservableProperty] private bool isEditing;
        [ObservableProperty] private bool isNotEditing = true;

        // backup bản gốc để hủy
        private TienCong _originalTienCong;

        // Khi SelectedTienCong thay đổi, show pane và reset chế độ edit
        partial void OnSelectedTienCongChanged(TienCong value)
        {
            IsDetailPaneVisible = value != null;
            IsEditing = false;
            IsNotEditing = true;
        }


        [RelayCommand]
        private void ShowDetail(TienCong tien)
        {
            SelectedTienCong = tien;
        }

        [RelayCommand]
        private void CloseDetail()
        {
            SelectedTienCong = null;
            IsDetailPaneVisible = false;
        }

        [RelayCommand]
        private void EditDetail()
        {
            if (SelectedTienCong == null) return;
            // lưu tạm
            _originalTienCong = new TienCong
            {
                Id = SelectedTienCong.Id,
                TenLoaiTienCong = SelectedTienCong.TenLoaiTienCong,
                DonGiaLoaiTienCong = SelectedTienCong.DonGiaLoaiTienCong
            };
            IsEditing = true;
            IsNotEditing = false;
        }

        [RelayCommand]
        private async Task SaveDetail()
        {
            if (SelectedTienCong == null)
                return;

            // 0) Kiểm tra ID hai bảng phải hợp lệ
            if (SelectedTienCong.Id == Guid.Empty || SelectedTienCong.NoiDungSuaChuaId == Guid.Empty)
            {
                await Shell.Current.DisplayAlert("Lỗi", "Dữ liệu không hợp lệ, vui lòng kiểm tra lại.", "OK");
                return;
            }

            // 1) Kiểm tra trùng tên tiền công
            var all = await _tienCongService.GetAll() ?? new List<TienCong>();
            var duplicate = all
                .Where(x => x.Id != SelectedTienCong.Id)
                .Any(x => x.TenLoaiTienCong.Trim().Equals(SelectedTienCong.TenLoaiTienCong.Trim(), StringComparison.OrdinalIgnoreCase));
            if (duplicate)
            {
                await Shell.Current.DisplayAlert("Lỗi", "Tên loại tiền công này đã tồn tại.", "OK");
                return;
            }

            // 2) Cập nhật tên nội dung sửa chữa
            var nd = new NoiDungSuaChua
            {
                Id = SelectedTienCong.NoiDungSuaChuaId,
                TenNoiDungSuaChua = SelectedTienCong.TenNoiDungSuaChua.Trim()
            };
            await _noiDungService.Update(nd);

            // 3) Cập nhật tiền công
            await _tienCongService.Update(SelectedTienCong);

            // 4) Reload danh sách để lấy dữ liệu mới nhất
            await LoadAsync();

            // 5) Hiển thị lại detail của bản ghi vừa lưu
            var updated = _allTienCong.FirstOrDefault(x => x.Id == SelectedTienCong.Id);
            if (updated != null)
                ShowDetail(updated);
            else
                await Shell.Current.DisplayAlert("Thông báo", "Tiền công không tồn tại", "OK");

            // 6) Toast thông báo thành công
            var toast = Toast.Make("Cập nhật thành công", CommunityToolkit.Maui.Core.ToastDuration.Short);
            await toast.Show();

            // 7) Chuyển về chế độ view
            IsEditing = false;
            IsNotEditing = true;
        }

        [RelayCommand]
        private void CancelDetail()
        {
            if (_originalTienCong != null)
            {
                // phục hồi
                SelectedTienCong.TenLoaiTienCong = _originalTienCong.TenLoaiTienCong;
                SelectedTienCong.DonGiaLoaiTienCong = _originalTienCong.DonGiaLoaiTienCong;
            }
            IsEditing = false;
            IsNotEditing = true;
        }

    }
}
