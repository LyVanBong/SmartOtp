namespace SmartOtp.Views;

public partial class AddCodeView : ContentPageBase
{
    public AddCodeView(AddCodeViewModel addCodeViewModel)
    {
        BindingContext = addCodeViewModel;
        InitializeComponent();
    }
}