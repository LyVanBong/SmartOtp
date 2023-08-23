using SmartOtp.Views;
using Xamarin.Forms;

namespace SmartOtp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(TotpPage), typeof(TotpPage));
            Routing.RegisterRoute(nameof(HotpPage), typeof(HotpPage));
        }

    }
}
