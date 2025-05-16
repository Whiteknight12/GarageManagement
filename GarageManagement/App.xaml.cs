
namespace GarageManagement
{
    public partial class App : Application
    {
        public static string appName = "MyGara";
        private bool _isDarkMode = false;
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

        public void ToggleTheme()
        {
            _isDarkMode = !_isDarkMode;
            var mergedDictionaries = Resources.MergedDictionaries.FirstOrDefault();
            if (mergedDictionaries != null)
            {
                mergedDictionaries["BackgroundColor"] = _isDarkMode ? (Color)mergedDictionaries["BackgroundColorDark"] : (Color)mergedDictionaries["BackgroundColorLight"];
                mergedDictionaries["CardBackgroundColor"] = _isDarkMode ? (Color)mergedDictionaries["CardBackgroundColorDark"] : (Color)mergedDictionaries["CardBackgroundColorLight"];
                mergedDictionaries["TextColor"] = _isDarkMode ? (Color)mergedDictionaries["TextColorDark"] : (Color)mergedDictionaries["TextColorLight"];
                mergedDictionaries["InputBackgroundColor"] = _isDarkMode ? (Color)mergedDictionaries["InputBackgroundColorDark"] : (Color)mergedDictionaries["InputBackgroundColorLight"];
                mergedDictionaries["PlaceholderColor"] = _isDarkMode ? (Color)mergedDictionaries["PlaceholderColorDark"] : (Color)mergedDictionaries["PlaceholderColorLight"];
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("MergedDictionaries is null or not found.");
            }
        }
    }
}
