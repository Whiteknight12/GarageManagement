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

            // 2) SoXeToiDaTiepNhan phải là số nguyên và không âm
            if (SelectedParameter.TenThamSo == "SoXeToiDaTiepNhan"
                && (SelectedParameter.GiaTri < 0 || SelectedParameter.GiaTri % 1 != 0))
            {
                await Shell.Current.DisplayAlert(
                    "Thông báo",
                    "Số xe tối đa tiếp nhận phải là số nguyên không âm.",
                    "OK");
                return;
            }

            // 3) VuotSoTienNo chỉ được 0 hoặc 1
            if (SelectedParameter.TenThamSo == "VuotSoTienNo"
                && SelectedParameter.GiaTri != 0
                && SelectedParameter.GiaTri != 1)
            {
                await Shell.Current.DisplayAlert(
                    "Thông báo",
                    "Giá trị \"Vượt số tiền nợ\" chỉ được nhập 0 hoặc 1.",
                    "OK");
                return;
            }

            // cập nhật lên server, tắt form…
            var parameterToUpdate = Parameters
                .FirstOrDefault(p => p.TenThamSo == SelectedParameter.TenThamSo);
            if (parameterToUpdate != null)
            {
                var idx = Parameters.IndexOf(parameterToUpdate);
                parameterToUpdate.GiaTri = SelectedParameter.GiaTri;
                parameterToUpdate.MoTa = SelectedParameter.MoTa;
                await _thamSoService.Update(parameterToUpdate);
                Parameters[idx] = parameterToUpdate;
            }

            IsEditFormVisible = false;
            await Toast.Make("Thay đổi giá trị thành công", CommunityToolkit.Maui.Core.ToastDuration.Short).Show();
            await LoadAsync();
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

