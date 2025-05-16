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
        var timer = new System.Timers.Timer(1000);
        timer.Elapsed += (s, e) => MainThread.BeginInvokeOnMainThread(() => ClockLabel.Text = DateTime.Now.ToString("HH:mm:ss"));
        timer.Start();
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        if (width>0 && height>0) _=_viewModel.LoadAsync();
    }
}