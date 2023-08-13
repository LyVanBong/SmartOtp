using System.Collections.ObjectModel;

namespace SmartOtp.ViewModels;

public class HomeViewModel : ViewModelBase
{
    private string _searchText;
    private IOtpService _otpService;
    private ObservableCollection<SmartOtpModel> _smartOtpModels;

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

        return base.InitializeAsync();
    }

    private Task AddCodeAsync()
    {
        return NavigationService.NavigateToAsync("//" + Routes.AddCode);
    }
}