using APIClassLibrary;
using APIClassLibrary.APIModels;
using GarageManagement.ViewModels;

namespace GarageManagement.Pages;

public partial class NhanSuMainPage : ContentView
{
    private readonly NhanSuMainPageViewModel _viewModel;
    bool _isAnimating, _isClockRunning;
    const float Hue = 0.58f, Sat = 0.4f;
    const float Lmin = 0.2f, Lmax = 0.75f;
    const int StopCount = 30;

    private readonly APIClientService<NguoiDungThongBao> _ndtbService;

    public NhanSuMainPage(NhanSuMainPageViewModel viewModel,
        APIClientService<NguoiDungThongBao> ndtbService)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
        _ndtbService = ndtbService;

        InitGradientStops(StopCount);
        StartClock();
        AnimateBackground();
    }

    void InitGradientStops(int count)
    {
        var stops = AnimatedBrush.GradientStops;
        stops.Clear();
        for (int i = 0; i <= count; i++)
        {
            float t = i / (float)count;
            // nội suy lightness HSL
            float l = Lmin + (Lmax - Lmin) * t;
            stops.Add(new GradientStop
            {
                Offset = t,
                Color = Color.FromHsla(Hue, Sat, l)
            });
        }
    }

    void AnimateBackground()
    {
        _isAnimating = true;
        // ~60fps ⇒ 16ms
        Device.StartTimer(TimeSpan.FromMilliseconds(16), () =>
        {
            if (!_isAnimating || AnimatedBrush?.GradientStops == null)
                return false;

            try
            {
                var stops = AnimatedBrush.GradientStops;
                for (int i = 0; i < stops.Count; i++)
                {
                    var o = stops[i].Offset + 0.005f;
                    stops[i].Offset = o > 1f ? o - 1f : o;
                }
            }
            catch { return false; }

            return true;
        });
    }

    protected override void OnParentSet()
    {
        base.OnParentSet();
        if (Parent == null)
        {
            _isAnimating = false;
            _isClockRunning = false;
        }
        MessagingCenter.Subscribe<NhanSuMainPageViewModel>(this, "ScrollRight", (sender) => ScrollToRight());
        MessagingCenter.Subscribe<NhanSuMainPageViewModel>(this, "ScrollLeft", (sender) => ScrollToLeft());
    }

    void StartClock()
    {
        _isClockRunning = true;
        UpdateClockLabels();
        Device.StartTimer(TimeSpan.FromSeconds(1), () =>
        {
            if (!_isClockRunning) return false;
            try { UpdateClockLabels(); } catch { return false; }
            return true;
        });
    }

    void UpdateClockLabels()
    {
        var now = DateTime.Now;
        ClockLabel.Text = now.ToString("HH:mm:ss");
        DateLabel.Text = now.ToString("dd/MM/yyyy");
        DayLabel.Text = now.DayOfWeek.ToString();
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        if (width > 0 && height > 0)
            _ = _viewModel.LoadAsync();
    }

    public async void OnThongBaoCheckedChanged(object sender, EventArgs e)
    {
        if (sender is CheckBox checkbox && checkbox.BindingContext is ThongBao thongBao)
        {
            // Nếu đã đánh dấu là đã đọc và chưa có bản ghi thì mới tạo
            if (checkbox.IsChecked)
            {
                var nguoiDungThongBao = new NguoiDungThongBao
                {
                    Id=Guid.NewGuid(),
                    nguoiDungId = _viewModel.NguoiDungId,
                    thongBaoId = thongBao.Id
                };

                try
                {
                    var result=await _ndtbService.Create(nguoiDungThongBao);
                    thongBao.DaDoc = true;
                    thongBao.Visible = false;
                    thongBao.OnPropertyChanged(nameof(thongBao.DaDoc));
                    thongBao.OnPropertyChanged(nameof(thongBao.Visible));
                }
                catch (Exception ex)
                {
                    //log e out
                }
            }
        }
    }

    private async void ScrollToRight()
    {
        double currentX = ThongBaoScrollView.ScrollX;
        await ThongBaoScrollView.ScrollToAsync(currentX + 250, 0, true); // scroll +250px
    }

    private async void ScrollToLeft()
    {
        double currentX = ThongBaoScrollView.ScrollX;
        double newX = currentX - 250;
        if (newX < 0) newX = 0;
        await ThongBaoScrollView.ScrollToAsync(newX, 0, true);
    }
}