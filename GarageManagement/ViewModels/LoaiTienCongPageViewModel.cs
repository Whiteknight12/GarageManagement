using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GarageManagement.Pages;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace GarageManagement.ViewModels
{
    public partial class LoaiTienCongPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<TienCong> listTienCong = new();

        [ObservableProperty]
        private bool isDeleteMode;

        private readonly APIClientService<TienCong> _tienCongService;
        private readonly ThemLoaiTienCongPageViewModel _themLoaiTienCongPageViewModel;

        public LoaiTienCongPageViewModel(APIClientService<TienCong> tienCongService,
                                         ThemLoaiTienCongPageViewModel themLoaiTienCongPageViewModel)
        {
            _tienCongService = tienCongService;
            _themLoaiTienCongPageViewModel = themLoaiTienCongPageViewModel;
            IsDeleteMode = false;
            _ = LoadAsync();
        }

        public async Task LoadAsync()
        {
            var list = await _tienCongService.GetAll();
            ListTienCong = new ObservableCollection<TienCong>(list);
        }

        public void Load(TienCong item)
        {
            ListTienCong.Add(item);
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
        private void Delete()
        {
            var selectedItems = ListTienCong.Where(x => x.IsSelected).ToList();
            foreach (var item in selectedItems)
            {
                _ = _tienCongService.Delete(item.Id);
                ListTienCong.Remove(item);
            }

            IsDeleteMode = false;
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
    }
}
