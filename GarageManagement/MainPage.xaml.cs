using APIClassLibrary;
using APIClassLibrary.APIModels;

namespace GarageManagement
{
    public partial class MainPage : ContentPage
    {
        private readonly APIClientService<Xe> _service;
        public MainPage(APIClientService<Xe> service)
        {
            InitializeComponent();
            _service = service;
        }
        public async void GetCars_Clicked(object sender, EventArgs e)
        {
            var cars = await _service.GetAll();
            CarCollectionView.ItemsSource = cars;
        }
    }

}
