using System.Collections.ObjectModel;
using System.Text;

namespace SmartOtp.ViewModels;

public class HomeViewModel : ViewModelBase
{
    private string _searchText;
    private IOtpService _otpService;
    private ObservableCollection<SmartOtpModel> _smartOtpModels;
    private IDispatcherTimer _timer;

    public string SearchText
    {
        get => _searchText;
        set => SetProperty(ref _searchText, value);
    }

    public ICommand AddCodeCommand { get; private set; }

    public ObservableCollection<SmartOtpModel> SmartOtpModels
    {
        get => _smartOtpModels;
        set => SetProperty(ref _smartOtpModels, value);
    }

    public ICommand SelectCodeCommand { get; private set; }

    public HomeViewModel(INavigationService navigationService, IOtpService otpService) : base(navigationService)
    {
        _otpService = otpService;
        Title = "Home";
        AddCodeCommand = new AsyncRelayCommand(AddCodeAsync);
        SelectCodeCommand = new AsyncRelayCommand<string>(SelectCodeAsync);
    }

    private async Task SelectCodeAsync(string arg)
    {
        if (arg == null)
            return;
        await Clipboard.Default.SetTextAsync(arg);
        var toast = Toast.Make("Copy to clipboard", ToastDuration.Long);
        await toast.Show();
    }

    public override Task InitializeAsync()
    {
        SmartOtpModels = new ObservableCollection<SmartOtpModel>(_otpService.GetOtps());

        if (_timer == null || !_timer.IsRunning)
        {
            var myapp = Application.Current;
            if (myapp != null)
            {
                _timer = myapp.Dispatcher.CreateTimer();
                _timer.Interval = TimeSpan.FromSeconds(1);
                _timer.Tick += (s, e) => Timer_Tick(s, e);
                _timer.Start();
            }
        }

        return base.InitializeAsync();
    }

    private Task Timer_Tick(object sender, EventArgs eventArgs)
    {
        if (SmartOtpModels.Any())
        {
            foreach (var smartOtpModel in SmartOtpModels)
            {
                _ = UpdateOtpAsync(smartOtpModel);
            }
        }
        return Task.CompletedTask;
    }

    private Task UpdateOtpAsync(SmartOtpModel smartOtpModel)
    {
        if (smartOtpModel.IsTotp)
        {
            _ = UpdateTotpAsync(smartOtpModel);
        }
        else
        {
            _ = UpdateHotpAsync(smartOtpModel);
        }
        return Task.CompletedTask;
    }

    private Task UpdateHotpAsync(SmartOtpModel smartOtpModel)
    {


        return Task.CompletedTask;
    }

    private Task UpdateTotpAsync(SmartOtpModel smartOtpModel)
    {
        var secret = Encoding.UTF8.GetBytes(smartOtpModel.Secret);
        var totp = new Totp(secretKey: secret, step: smartOtpModel.Period, totpSize: smartOtpModel.Digits, mode: smartOtpModel.HashMode());

        var otp = totp.ComputeTotp(DateTime.Now);

        smartOtpModel.Otp = otp;

        var remainingSeconds = totp.RemainingSeconds(DateTime.Now);

        smartOtpModel.Progress = (float)remainingSeconds / smartOtpModel.Period;

        smartOtpModel.PeriodView = remainingSeconds;

        return Task.CompletedTask;
    }

    private Task AddCodeAsync()
    {
        return NavigationService.NavigateToAsync("//" + Routes.AddCode);
    }
}