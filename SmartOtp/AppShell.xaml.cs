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
            Routing.RegisterRoute(Routes.Settings, typeof(SettingsView));
            Routing.RegisterRoute(Routes.AddCode, typeof(AddCodeView));
            Routing.RegisterRoute(Routes.Home, typeof(HomeView));
        }
    }
}