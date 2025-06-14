using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace GarageManagement.ViewModels
{
    public partial class BaoCaoDoanhSoListPageViewModel : BaseViewModel
    {
        private readonly APIClientService<BaoCaoDoanhThuThang> _baoCaoService;

        public BaoCaoDoanhSoListPageViewModel(APIClientService<BaoCaoDoanhThuThang> baoCaoService)
        {
            _baoCaoService = baoCaoService;
            _ = LoadAsync();
        }

        [ObservableProperty]
        private ObservableCollection<BaoCaoDoanhThuThang> listBaoCao = new();

        [ObservableProperty]
        private bool isDeleteMode;

        private List<BaoCaoDoanhThuThang> _allBaoCao = new();

        public async Task LoadAsync()
        {
            var list = await _baoCaoService.GetAll();
            _allBaoCao = list ?? new List<BaoCaoDoanhThuThang>();

            int index = 1;
            foreach (var bc in _allBaoCao)
                bc.IdForUI = index++;

            ListBaoCao = new ObservableCollection<BaoCaoDoanhThuThang>(_allBaoCao);
        }

        [RelayCommand]
        private void ToggleDeleteMode()
        {
            IsDeleteMode = !IsDeleteMode;

            if (!IsDeleteMode)
            {
                foreach (var bc in ListBaoCao)
                    bc.IsSelected = false;

                ListBaoCao = new ObservableCollection<BaoCaoDoanhThuThang>(ListBaoCao);
            }
        }

        [RelayCommand]
        private async Task Delete()
        {
            var selected = ListBaoCao.Where(x => x.IsSelected).ToList();

            foreach (var item in selected)
                await _baoCaoService.Delete(item.Id);

            foreach (var item in selected)
                ListBaoCao.Remove(item);

            IsDeleteMode = false;

            int index = 1;
            foreach (var bc in ListBaoCao)
                bc.IdForUI = index++;
        }

        [RelayCommand]
        private void ViewDetail(Guid id)
        {
            MessagingCenter.Send(this, "ViewChiTietBaoCaoDoanhSo", id);
        }

        [RelayCommand]
        private void Add()
        {
            // Logic xử lý thêm báo cáo mới (tự xử lý)
        }
    }
}
