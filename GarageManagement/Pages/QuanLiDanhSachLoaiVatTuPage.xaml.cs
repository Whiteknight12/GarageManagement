using GarageManagement.ViewModels;

namespace GarageManagement.Pages
{
    public partial class QuanLiDanhSachLoaiVatTuPage : ContentView
    {
        public QuanLiDanhSachLoaiVatTuPage(QuanLiDanhSachLoaiVatTuPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}