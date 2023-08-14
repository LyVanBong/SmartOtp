namespace SmartOtp.Views;

public partial class HotpView : ContentPageBase
{
    public HotpView(HotpViewModel hotpViewModel)
    {
        BindingContext = hotpViewModel;
        InitializeComponent();
    }
}