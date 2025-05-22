using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace GarageManagement.ViewModels
{
    public partial class QuanLyThamSoPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<ThamSo> parameters = new(); 

        [ObservableProperty]
        private bool isEditFormVisible;

        [ObservableProperty]
        private ThamSo selectedParameter = new();

        private List<ThamSo> allThamSos = new(); 

        private readonly APIClientService<ThamSo> _thamSoService; 

        public QuanLyThamSoPageViewModel(APIClientService<ThamSo> thamSoService)
        {

            _thamSoService = thamSoService;
            _ = LoadAsync(); 
            // Khởi tạo danh sách tham số mẫu (tương tự dữ liệu trong HTML)
            

            IsEditFormVisible = false;
 
        }

        [RelayCommand]
        private void SelectParameter(ThamSo parameter)
        {
            SelectedParameter = new ThamSo
            {
                Id = parameter.Id,
                TenThamSo = parameter.TenThamSo,
                GiaTri = parameter.GiaTri,
                MoTa = parameter.MoTa
            };
            IsEditFormVisible = true;
        }

        [RelayCommand]
        private async Task SaveChanges()
        {
            if (SelectedParameter == null) return;

            // Cập nhật giá trị tham số trong danh sách
            var parameterToUpdate = Parameters.FirstOrDefault(p => p.TenThamSo == SelectedParameter.TenThamSo);
            if (parameterToUpdate != null)
            {
                var index = Parameters.IndexOf(parameterToUpdate); 
                parameterToUpdate.GiaTri = SelectedParameter.GiaTri;
                parameterToUpdate.MoTa = SelectedParameter.MoTa;
                await _thamSoService.Update(parameterToUpdate);
                Parameters[index] = parameterToUpdate;
            }

            // Ẩn form và thông báo
            IsEditFormVisible = false;
            var toast = Toast.Make("Thay đổi giá trị thành công", CommunityToolkit.Maui.Core.ToastDuration.Short);
            await toast.Show();
            
        }

        public async Task LoadAsync()
        {
            allThamSos = await _thamSoService.GetAll();
            Parameters = new ObservableCollection<ThamSo>
            {
                new ThamSo { Id = allThamSos[0].Id, TenThamSo = allThamSos[0].TenThamSo, GiaTri = allThamSos[0].GiaTri, MoTa = allThamSos[0].MoTa },
                new ThamSo { Id = allThamSos[1].Id, TenThamSo = allThamSos[1].TenThamSo, GiaTri = allThamSos[1].GiaTri, MoTa = allThamSos[1].MoTa },
                new ThamSo { Id = allThamSos[2].Id, TenThamSo = allThamSos[2].TenThamSo, GiaTri = allThamSos[2].GiaTri, MoTa = allThamSos[2].MoTa }
            };
        }
    }
}

