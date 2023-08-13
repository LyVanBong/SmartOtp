namespace SmartOtp.ViewModels;

public class SetupCodeViewModel : ViewModelBase
{
    private string _issuer;
    private string _secret;
    private string _user;
    private readonly ISettingsService _settingsService;
    private readonly IOtpService _otpService;
    public ICommand AddCodeCommnad { get; private set; }

    public string Issuer
    {
        get => _issuer;
        set => SetProperty(ref _issuer, value);
    }

    public string User
    {
        get => _user;
        set => SetProperty(ref _user, value);
    }

    public string Secret
    {
        get => _secret;
        set => SetProperty(ref _secret, value);
    }

    public SetupCodeViewModel(INavigationService navigationService, ISettingsService settingsService, IOtpService databaseService) : base(navigationService)
    {
        _settingsService = settingsService;
        _otpService = databaseService;
        Title = "Setup Code";
        AddCodeCommnad = new AsyncRelayCommand(AddCode);
    }

    private async Task AddCode()
    {
        if (string.IsNullOrWhiteSpace(Issuer))
        {
            await ShowMessageAsync("Issuer is required");
            return;
        }
        if (string.IsNullOrWhiteSpace(User))
        {
            await ShowMessageAsync("User is required");
            return;
        }
        if (string.IsNullOrWhiteSpace(Secret))
        {
            await ShowMessageAsync("Secret is required");
            return;
        }

        var otp = new SmartOtpModel()
        {
            Issuer = Issuer,
            User = User,
            Secret = Secret,
            IsSha1 = _settingsService.IsSha1,
            IsSha256 = _settingsService.IsSha256,
            IsSha512 = _settingsService.IsSha512,
            Period = _settingsService.Period,
            Digits = _settingsService.Digits,
            Counter = _settingsService.Counter,
            IsTotp = _settingsService.IsTotp,
        };
        var add = _otpService.SaveOtp(otp);
        if (add)
        {
            // Clear input
            Issuer = string.Empty;
            User = string.Empty;
            Secret = string.Empty;
            await ShowMessageAsync("Add code done");
        }
        else
        {
            await ShowMessageAsync("Add code failed");
        }
    }

    private static Task ShowMessageAsync(string messsage)
    {
        var toast = Toast.Make(messsage, ToastDuration.Long, 14);
        return toast.Show();
    }
}