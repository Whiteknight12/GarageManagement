using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GarageManagement.Pages;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GarageManagement.ViewModels
{
    public partial class QuanLiDanhSachLoaiVatTuPageViewModel : BaseViewModel
    {
        private List<VatTuPhuTung> _allVatTu = new();          // dữ liệu gốc

        [ObservableProperty] private ObservableCollection<VatTuPhuTung> listVatTuPhuTung = new();

        [ObservableProperty] private string nameFilter = string.Empty;
        [ObservableProperty] private string priceFromText = string.Empty;
        [ObservableProperty] private string priceToText = string.Empty;
        [ObservableProperty] private string quantityFromText = string.Empty;
        [ObservableProperty] private string quantityToText = string.Empty;

        partial void OnNameFilterChanged(string _) => ApplyFilter();
        partial void OnPriceFromTextChanged(string _) => ApplyFilter();
        partial void OnPriceToTextChanged(string _) => ApplyFilter();
        partial void OnQuantityFromTextChanged(string _) => ApplyFilter();
        partial void OnQuantityToTextChanged(string _) => ApplyFilter();


        [ObservableProperty]
        private bool isDeleteMode;

        private readonly APIClientService<VatTuPhuTung> _vatTuPhuTungService;
        private readonly ThemLoaiVatTuPhuTungPageViewModel _themLoaiVatTuPhuTungPageViewModel;

        public QuanLiDanhSachLoaiVatTuPageViewModel(APIClientService<VatTuPhuTung> vatTuPhuTungService,
                                                    ThemLoaiVatTuPhuTungPageViewModel themLoaiVatTuPhuTungPageViewModel)
        {
            _vatTuPhuTungService = vatTuPhuTungService;
            _themLoaiVatTuPhuTungPageViewModel = themLoaiVatTuPhuTungPageViewModel;
            IsDeleteMode = false;
            //_ = LoadAsync();
        }

        public async Task LoadAsync()
        {
            try
            {
                _allVatTu = await _vatTuPhuTungService.GetAll() ?? new();
                ApplyFilter();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading VatTuPhuTung: {ex.Message}");
            }
        }

        /* ---------- LOCAL ADD (sau khi tạo mới) ---------- */
        public void Load(VatTuPhuTung item)
        {
            if (item == null) return;
            if (_allVatTu.Any(x => x.VatTuPhuTungId == item.VatTuPhuTungId)) return;

            _allVatTu.Add(item);
            ApplyFilter();
        }

        /* ---------- FILTER ---------- */
        private void ApplyFilter()
        {
            IEnumerable<VatTuPhuTung> q = _allVatTu;

            /* --- theo tên --- */
            if (!string.IsNullOrWhiteSpace(NameFilter))
            {
                var key = NameFilter.Trim().ToLower();
                q = q.Where(v => (v.TenLoaiVatTuPhuTung ?? "")
                                 .ToLower()
                                 .Contains(key));
            }

            /* --- theo giá --- */
            if (double.TryParse(PriceFromText, out var minPrice))
                q = q.Where(v => v.DonGiaBanLoaiVatTuPhuTung >= minPrice);

            if (double.TryParse(PriceToText, out var maxPrice))
                q = q.Where(v => v.DonGiaBanLoaiVatTuPhuTung <= maxPrice);

            /* --- theo số lượng tồn --- */
            if (int.TryParse(QuantityFromText, out var minQty))
                q = q.Where(v => v.SoLuong >= minQty);

            if (int.TryParse(QuantityToText, out var maxQty))
                q = q.Where(v => v.SoLuong <= maxQty);

            ListVatTuPhuTung = new ObservableCollection<VatTuPhuTung>(q);
        }

        [RelayCommand]
        private async void Add()
        {
            var newWindow = new Window
            {
                Page = new ThemLoaiVatTuPhuTungPage(_themLoaiVatTuPhuTungPageViewModel),
                MaximumHeight = 400,
                MaximumWidth = 400,
                MinimumHeight = 400,
                MinimumWidth = 400
            };

            _themLoaiVatTuPhuTungPageViewModel.OnVatTuPhuTungAdded = async (VatTuPhuTung vatTu) =>
            {
                Load(vatTu);
                await LoadAsync(); // Làm mới danh sách từ API
            };

            Application.Current.OpenWindow(newWindow);
        }

        [RelayCommand]
        private void Edit()
        {
            // TODO: Implement logic to edit selected item
        }

        [RelayCommand]
        private void ToggleDeleteMode()
        {
            IsDeleteMode = !IsDeleteMode;
        }

        [RelayCommand]
        private async Task DeleteAsync()
        {
            var selected = ListVatTuPhuTung.Where(x => x.IsSelected).ToList();
            foreach (var v in selected)
            {
                await _vatTuPhuTungService.Delete(v.VatTuPhuTungId);
                _allVatTu.Remove(v);
            }
            IsDeleteMode = false;
            ApplyFilter();
        }

        [RelayCommand]
        private void GoToChiTietVatTuPhuTungPage(Guid id)
        {
            // TODO: Navigate to ChiTietVatTuPhuTungPage
        }

        [RelayCommand]
        private void GoBack()
        {
            Shell.Current.GoToAsync("..");
        }


        [ObservableProperty] private VatTuPhuTung selectedVatTu;
        [ObservableProperty] private bool isDetailPaneVisible;
        [ObservableProperty] private bool isEditing;
        [ObservableProperty] private bool isNotEditing = true;

        private VatTuPhuTung _originalVatTu;

        partial void OnSelectedVatTuChanged(VatTuPhuTung value)
        {
            IsDetailPaneVisible = value != null;
            IsEditing = false;
            IsNotEditing = true;
        }

        [RelayCommand]
        private async Task ShowDetail(Guid id)
        {
            var item = await _vatTuPhuTungService.GetByID(id);
            if (item is not null) SelectedVatTu = item;
            else
            {
                await Shell.Current.DisplayAlert("Thông báo", "Vật tư phụ tùng không tồn tại", "OK");
                return;
            }
            IsDetailPaneVisible = true; 
        }

        [RelayCommand]
        private void CloseDetail()
        {
            SelectedVatTu = null;
            IsDetailPaneVisible = false;
        }

        [RelayCommand]
        private void EditDetail()
        {
            if (SelectedVatTu == null) return;
            _originalVatTu = new VatTuPhuTung
            {
                VatTuPhuTungId = SelectedVatTu.VatTuPhuTungId,
                TenLoaiVatTuPhuTung = SelectedVatTu.TenLoaiVatTuPhuTung,
                SoLuong = SelectedVatTu.SoLuong,
                DonGiaBanLoaiVatTuPhuTung = SelectedVatTu.DonGiaBanLoaiVatTuPhuTung
            };
            IsEditing = true;
            IsNotEditing = false;
        }

        [RelayCommand]
        private async Task SaveDetail()
        {
            if (SelectedVatTu == null) return;
            await _vatTuPhuTungService.Update(SelectedVatTu);
            IsEditing = false;
            IsNotEditing = true;
            await LoadAsync();
        }

        [RelayCommand]
        private void CancelDetail()
        {
            if (_originalVatTu != null)
            {
                SelectedVatTu.TenLoaiVatTuPhuTung = _originalVatTu.TenLoaiVatTuPhuTung;
                SelectedVatTu.SoLuong = _originalVatTu.SoLuong;
                SelectedVatTu.DonGiaBanLoaiVatTuPhuTung = _originalVatTu.DonGiaBanLoaiVatTuPhuTung;
            }
            IsEditing = false;
            IsNotEditing = true;
        }
    }
}