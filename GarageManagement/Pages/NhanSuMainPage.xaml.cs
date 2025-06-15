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
    }

    protected override void OnParentSet()
    {
        base.OnParentSet();
        // at this point ClockLabel is guaranteed non-null
        ClockLabel.Text = DateTime.Now.ToString("HH:mm:ss");
        DateLabel.Text = DateTime.Now.ToString("dd/MM/yyyy");
        DayLabel.Text = DateTime.Now.DayOfWeek.ToString();

        Device.StartTimer(TimeSpan.FromSeconds(1), () =>
        {
            ClockLabel.Text = DateTime.Now.ToString("HH:mm:ss");
            return true; // keep repeating
        });
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        if (width > 0 && height > 0)
            _ = _viewModel.LoadAsync();
    }
}