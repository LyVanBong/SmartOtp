namespace SmartOtp.ViewModels;

public class SettingsViewModel : ViewModelBase
{
    private readonly ISettingsService _settingsService;
    private bool _isSha1;
    private bool _isSha256;
    private bool _isSha512;
    private int _otpLength;
    private int _timeStep;
    private bool _isTotp;
    private bool _isHotp;

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

    public SettingsViewModel(INavigationService navigationService, ISettingsService settingsService) : base(navigationService)
    {
        Title = "Settings";
        _settingsService = settingsService;
        SaveSettingsCommand = new AsyncRelayCommand(SaveSettings);
    }

    public override Task InitializeAsync()
    {
        IsSha1 = _settingsService.IsSha1;
        IsSha256 = _settingsService.IsSha256;
        IsSha512 = _settingsService.IsSha512;
        OtpLength = _settingsService.Digits;
        TimeStep = _settingsService.Period;
        IsTotp = _settingsService.IsTotp;
        IsHotp = _settingsService.IsHotp;
        return base.InitializeAsync();
    }

    private async Task SaveSettings()
    {
        _settingsService.SaveSettings(IsSha1,
        IsSha256,
        IsSha512,
        IsTotp,
        IsHotp,
        TimeStep,
        OtpLength);
        var toast = Toast.Make("Update done", ToastDuration.Long, 14);
        await toast.Show();
    }
}