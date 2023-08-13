using SmartOtp.Services.Otp;

namespace SmartOtp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Awesome5ProSolid.otf", "Awesome5ProSolid");
                })
                .RegisterAppServices()
                .RegisterViewModels()
                .RegisterViews();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        /// <summary>
        /// Register all services
        /// </summary>
        /// <param name="mauiAppBuilder"></param>
        /// <returns></returns>
        public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<IOtpService, OtpService>();
            mauiAppBuilder.Services.AddSingleton<ISettingsService, SettingsService>();
            mauiAppBuilder.Services.AddSingleton<INavigationService, NavigationService>();

            mauiAppBuilder.Services.AddSingleton<IDatabaseService, DatabaseService>();
            return mauiAppBuilder;
        }

        /// <summary>
        /// Register all view models
        /// </summary>
        /// <param name="mauiAppBuilder"></param>
        /// <returns></returns>
        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddTransient<CreateQrCodeViewModel>();
            mauiAppBuilder.Services.AddTransient<ScanQrCodeViewModel>();
            mauiAppBuilder.Services.AddTransient<SetupCodeViewModel>();
            mauiAppBuilder.Services.AddTransient<SettingsViewModel>();
            mauiAppBuilder.Services.AddTransient<AddCodeViewModel>();
            mauiAppBuilder.Services.AddTransient<HomeViewModel>();
            return mauiAppBuilder;
        }

        /// <summary>
        /// Register all views
        /// </summary>
        /// <param name="mauiAppBuilder"></param>
        /// <returns></returns>
        public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddTransient<CreateQrCodeView>();
            mauiAppBuilder.Services.AddTransient<SetupCodeView>();
            mauiAppBuilder.Services.AddTransient<ScanQrCodeView>();
            mauiAppBuilder.Services.AddTransient<AddCodeView>();
            mauiAppBuilder.Services.AddTransient<SettingsView>();
            mauiAppBuilder.Services.AddTransient<HomeView>();
            return mauiAppBuilder;
        }
    }
}