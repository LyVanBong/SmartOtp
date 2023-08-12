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


            return mauiAppBuilder;
        }
        /// <summary>
        /// Register all view models
        /// </summary>
        /// <param name="mauiAppBuilder"></param>
        /// <returns></returns>
        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
        {

            return mauiAppBuilder;
        }
        /// <summary>
        /// Register all views
        /// </summary>
        /// <param name="mauiAppBuilder"></param>
        /// <returns></returns>
        public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
        {

            return mauiAppBuilder;
        }

    }
}