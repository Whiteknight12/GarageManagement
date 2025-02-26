using MAUIProject.Pages;

namespace MAUIProject
{
    public partial class App : Application
    {
        public App(LoginPage loginpage)
        {
            InitializeComponent();
            MainPage = loginpage;
        }
    }
}
