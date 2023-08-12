using SmartOtp.Views;

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
            Routing.RegisterRoute(nameof(HomeView), typeof(HomeView));
        }
    }
}