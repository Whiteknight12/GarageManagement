
namespace GarageManagement
{
    public partial class App : Application
    {
        public static string appName = "MyGara";
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = base.CreateWindow(activationState);
            window.Title = appName;
            return window; 
        }
    }
}
