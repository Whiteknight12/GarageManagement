using APIClassLibrary;
using APIClassLibrary.APIModels;

namespace GarageManagement
{
    public partial class MainPage : ContentPage
    {
        private readonly APIClientService<Car> _service;
        public MainPage(APIClientService<Car> service)
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
