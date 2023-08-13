namespace SmartOtp.Views;

public partial class SettingsView : ContentPageBase
{
    public SettingsView(SettingsViewModel settingsViewModel)
    {
        BindingContext = settingsViewModel;
        InitializeComponent();
    }
}