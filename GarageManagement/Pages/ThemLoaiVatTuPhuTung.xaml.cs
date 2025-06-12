using GarageManagement.ViewModels;

namespace GarageManagement.Pages
{
    public partial class ThemLoaiVatTuPhuTungPage : ContentPage
    {
        public ThemLoaiVatTuPhuTungPage(ThemLoaiVatTuPhuTungPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}