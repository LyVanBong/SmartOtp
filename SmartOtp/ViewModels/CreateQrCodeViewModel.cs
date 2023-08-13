namespace SmartOtp.ViewModels;

public class CreateQrCodeViewModel:ViewModelBase
{
    public CreateQrCodeViewModel(INavigationService navigationService) : base(navigationService)
    {
        Title = "Create QR Code";
    }
}