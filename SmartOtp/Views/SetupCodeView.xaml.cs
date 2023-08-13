namespace SmartOtp.Views;

public partial class SetupCodeView : ContentPageBase
{
    public SetupCodeView(SetupCodeViewModel setupCodeViewModel)
    {
        BindingContext = setupCodeViewModel;
        InitializeComponent();
    }
}