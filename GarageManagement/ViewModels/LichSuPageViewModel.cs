using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageManagement.ViewModels
{
    public partial class LichSuPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<LichSu> lichSuList= new ObservableCollection<LichSu>();

        private readonly APIClientService<LichSu> _lichSuService;

        public LichSuPageViewModel(APIClientService<LichSu> lichSuService)
        {
            _lichSuService= lichSuService;
        }

        public async Task LoadAsync()
        {
            var lichSu = await _lichSuService.GetAll();
            if (lichSu is not null) LichSuList = new ObservableCollection<LichSu>(lichSu.OrderByDescending(ls => ls.ThoiDiemThucHien));
        }
    }
}
