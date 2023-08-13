namespace SmartOtp.Views;

public partial class CreateQrCodeView : ContentPageBase
{
    public CreateQrCodeView(CreateQrCodeViewModel createQrCodeViewModel)
    {
        BindingContext = createQrCodeViewModel;
        InitializeComponent();
    }
}