namespace SmartOtp.ViewModels;

public class AddCodeViewModel : ViewModelBase
{
    public ICommand AddCodeCommand { get; private set; }
    public AddCodeViewModel(INavigationService navigationService) : base(navigationService)
    {
        Title = "Add Code";
        AddCodeCommand = new AsyncRelayCommand<string>(AddCodeAsync);
    }

    private Task AddCodeAsync(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            return Task.CompletedTask;
        if (key == "0")
            return NavigationService.NavigateToAsync(Routes.SetupCode);
        if (key == "1")
            return NavigationService.NavigateToAsync(Routes.ScanQrCode);
        if (key == "2")
            return NavigationService.NavigateToAsync(Routes.CreateQrCode);
        return Task.CompletedTask;

    }
}