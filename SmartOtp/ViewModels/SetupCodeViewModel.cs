namespace SmartOtp.ViewModels;

public class SetupCodeViewModel:ViewModelBase
{
    public SetupCodeViewModel(INavigationService navigationService) : base(navigationService)
    {
        Title = "Setup Code";
    }
}