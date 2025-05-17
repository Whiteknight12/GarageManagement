using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class NhanSuMainPage : ContentView
{
    private readonly NhanSuMainPageViewModel _viewModel;
    
    public NhanSuMainPage(NhanSuMainPageViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
        StartClock();
	}

    void StartClock()
    {
        var now = DateTime.Now;
        ClockLabel.Text = now.ToString("HH:mm:ss");
        var timer = new System.Timers.Timer(1000);
        timer.Elapsed += (s, e) => MainThread.BeginInvokeOnMainThread(() => ClockLabel.Text = DateTime.Now.ToString("HH:mm:ss"));
        timer.AutoReset = true;
        timer.Start();
        DateLabel.Text = DateTime.UtcNow.ToLocalTime().ToString("dd/M/y");
        DayLabel.Text = DateTime.UtcNow.ToLocalTime().DayOfWeek.ToString();
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        if (width>0 && height>0) _=_viewModel.LoadAsync();
    }
}