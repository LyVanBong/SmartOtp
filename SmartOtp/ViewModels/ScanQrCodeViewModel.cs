namespace SmartOtp.ViewModels;

public class ScanQrCodeViewModel : ViewModelBase
{
    public ScanQrCodeViewModel(INavigationService navigationService) : base(navigationService)
    {
        Title = "Scan QR Code";
    }
}