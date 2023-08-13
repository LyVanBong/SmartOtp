namespace SmartOtp.Views;

public partial class ScanQrCodeView : ContentPageBase
{
    public ScanQrCodeView(ScanQrCodeViewModel scanQrCodeViewModel)
    {
        BindingContext = scanQrCodeViewModel;
        InitializeComponent();
    }
}