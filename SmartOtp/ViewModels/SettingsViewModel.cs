using System.Collections.ObjectModel;

namespace SmartOtp.ViewModels;

public class SettingsViewModel : ViewModelBase
{
    private bool _isSha1 = true;
    private bool _isSha256;
    private bool _isSha512;
    private int _otpLength = 6;
    private int _timeStep = 30;
    private bool _isTotp = true;
    private bool _isHotp;
    private ObservableCollection<SmartOtpModel> _smartOtpModels = new();
    private IDispatcherTimer _timer;
    private string _secret;
    private long _counter;

    public long Counter
    {
        get => _counter;
        set => SetProperty(ref _counter, value);
    }

    public bool IsSha1
    {
        get => _isSha1;
        set => SetProperty(ref _isSha1, value);
    }

    public bool IsSha256
    {
        get => _isSha256;
        set => SetProperty(ref _isSha256, value);
    }

    public bool IsSha512
    {
        get => _isSha512;
        set => SetProperty(ref _isSha512, value);
    }

    public int OtpLength
    {
        get => _otpLength;
        set => SetProperty(ref _otpLength, value);
    }

    public int TimeStep
    {
        get => _timeStep;
        set => SetProperty(ref _timeStep, value);
    }

    public bool IsTotp
    {
        get => _isTotp;
        set => SetProperty(ref _isTotp, value);
    }

    public bool IsHotp
    {
        get => _isHotp;
        set => SetProperty(ref _isHotp, value);
    }

    public ICommand SaveSettingsCommand { get; private set; }

    public string Secret
    {
        get => _secret;
        set => SetProperty(ref _secret, value);
    }

    public ObservableCollection<SmartOtpModel> SmartOtpModels
    {
        get => _smartOtpModels;
        set => SetProperty(ref _smartOtpModels, value);
    }
    public ICommand SelectCodeCommand { get; private set; }
    public SettingsViewModel(INavigationService navigationService) : base(navigationService)
    {
        Title = "SmartOTP";
        SaveSettingsCommand = new AsyncRelayCommand(SaveSettings);
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
        var hotp = new Hotp(secretKey: smartOtpModel.GetSecret(),
            mode: smartOtpModel.HashMode(),
            hotpSize: smartOtpModel.Digits);

        smartOtpModel.UpdateHotp(hotp.ComputeHOTP(smartOtpModel.Counter));

        return Task.CompletedTask;
    }

    private Task UpdateTotpAsync(SmartOtpModel smartOtpModel)
    {
        var totp = new Totp(secretKey: smartOtpModel.GetSecret(),
            step: smartOtpModel.Period,
            totpSize: smartOtpModel.Digits,
            mode: smartOtpModel.HashMode());

        var otp = totp.ComputeTotp(DateTime.Now);

        smartOtpModel.UpdateTotp(totp.ComputeTotp(DateTime.Now), totp.RemainingSeconds(DateTime.Now));

        return Task.CompletedTask;
    }
    private async Task SaveSettings()
    {
        if (string.IsNullOrEmpty(Secret))
        {
            await ToastMessage("Secret is required");
            return;
        }

        var otp = new SmartOtpModel
        {
            IsSha1 = IsSha1,
            IsSha256 = IsSha256,
            IsTotp = IsTotp,
            IsSha512 = IsSha512,
            Secret = Secret,
            Period = TimeStep,
            Digits = OtpLength,
            PeriodView = TimeStep,
            Counter = Counter,
        };

        SmartOtpModels.Add(otp);

        await ToastMessage("Add Code Done");

        Secret = string.Empty;
    }

    private Task ToastMessage(string message)
    {
        var toast = Toast.Make(message, ToastDuration.Short, 14);
        return toast.Show();
    }
}