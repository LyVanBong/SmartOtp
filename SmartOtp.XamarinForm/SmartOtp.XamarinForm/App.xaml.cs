using Prism;
using Prism.Ioc;
using SmartOtp.XamarinForm.ViewModels;
using SmartOtp.XamarinForm.Views;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;
using MainPage = SmartOtpApp.Views.MainPage;

namespace SmartOtp.XamarinForm
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<TotpPage, TotpPageViewModel>();
            containerRegistry.RegisterForNavigation<HotpPage, HotpPageViewModel>();
        }
    }
}
