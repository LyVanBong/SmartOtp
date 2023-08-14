namespace SmartOtp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            AppShell.InitializeRouting();
            InitializeComponent();
        }

        /// <summary>
        /// Register all routes
        /// </summary>
        private static void InitializeRouting()
        {
            Routing.RegisterRoute(Routes.Hotp, typeof(HotpView));
            Routing.RegisterRoute(Routes.CreateQrCode, typeof(CreateQrCodeView));
            Routing.RegisterRoute(Routes.SetupCode, typeof(SetupCodeView));
            Routing.RegisterRoute(Routes.ScanQrCode, typeof(ScanQrCodeView));
            Routing.RegisterRoute(Routes.Settings, typeof(SettingsView));
            Routing.RegisterRoute(Routes.AddCode, typeof(AddCodeView));
            Routing.RegisterRoute(Routes.Home, typeof(HomeView));
        }
    }
}