using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class NhanSuMainPage : ContentView
{
	public NhanSuMainPage()
	{
		InitializeComponent();
		BindingContext = new NhanSuMainPageViewModel();
        StartClock();
	}

    void StartClock()
    {
        var timer = new System.Timers.Timer(1000);
        timer.Elapsed += (s, e) => MainThread.BeginInvokeOnMainThread(() => ClockLabel.Text = DateTime.Now.ToString("HH:mm:ss"));
        timer.Start();
    }
}