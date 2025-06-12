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
        [ObservableProperty]
        private ObservableCollection<VatTuPhuTung> listVatTuPhuTung = new();

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
            _ = LoadAsync();
        }

        public async Task LoadAsync()
        {
            try
            {
                var list = await _vatTuPhuTungService.GetAll();
                ListVatTuPhuTung.Clear();
                if (list != null && list.Any())
                {
                    foreach (var item in list)
                    {
                        ListVatTuPhuTung.Add(item);
                    }
                    Debug.WriteLine($"Loaded {ListVatTuPhuTung.Count} items.");
                }
                else
                {
                    Debug.WriteLine("No items loaded from API.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading VatTuPhuTung: {ex.Message}");
            }
        }

        public void Load(VatTuPhuTung item)
        {
            if (item != null && !ListVatTuPhuTung.Any(x => x.VatTuPhuTungId == item.VatTuPhuTungId))
            {
                ListVatTuPhuTung.Add(item);
                Debug.WriteLine($"Added new item: {item.TenLoaiVatTuPhuTung}");
            }
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
        private async void Delete()
        {
            var selectedItems = ListVatTuPhuTung.Where(x => x.IsSelected).ToList();
            foreach (var item in selectedItems)
            {
                await _vatTuPhuTungService.Delete(item.VatTuPhuTungId);
                ListVatTuPhuTung.Remove(item);
            }

            IsDeleteMode = false;
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
    }
}