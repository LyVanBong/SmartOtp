using System;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using OtpNet;
using Prism.Mvvm;
using Prism.Navigation;
using SmartOtpApp.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SmartOtpApp.ViewModels
{
    public class TotpPageViewModel : ViewModelBase
    {
        private bool _isSha1 = true;
        private bool _isSha256;
        private bool _isSha512;
        private int _otpLength = 6;
        private int _timeStep = 30;
        private bool _isTotp = true;
        private bool _isHotp;
        private SmartOtpModel _smartOtpModels = new();
        private string _secret;
        private long _counter;

        public long Counter
        {
            get => _counter;
            set => SetProperty(ref _counter, value);
        }

        public bool IsSha1
        {
            get => _isSha1;
            set => SetProperty(ref _isSha1, value);
        }

        public bool IsSha256
        {
            get => _isSha256;
            set => SetProperty(ref _isSha256, value);
        }

        public bool IsSha512
        {
            get => _isSha512;
            set => SetProperty(ref _isSha512, value);
        }

        public int OtpLength
        {
            get => _otpLength;
            set => SetProperty(ref _otpLength, value);
        }

        public int TimeStep
        {
            get => _timeStep;
            set => SetProperty(ref _timeStep, value);
        }

        public bool IsTotp
        {
            get => _isTotp;
            set => SetProperty(ref _isTotp, value);
        }

        public bool IsHotp
        {
            get => _isHotp;
            set => SetProperty(ref _isHotp, value);
        }

        public ICommand SaveSettingsCommand { get; private set; }

        public string Secret
        {
            get => _secret;
            set => SetProperty(ref _secret, value);
        }

        public SmartOtpModel SmartOtpModels
        {
            get => _smartOtpModels;
            set => SetProperty(ref _smartOtpModels, value);
        }
        public ICommand SelectCodeCommand { get; private set; }
        public TotpPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            SaveSettingsCommand = new AsyncRelayCommand(SaveSettings);
            SelectCodeCommand = new AsyncRelayCommand<string>(SelectCodeAsync);
        }
        private async Task SelectCodeAsync(string arg)
        {
            if (arg == null)
                return;
            await Clipboard.SetTextAsync(arg);
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                Timer_Tick();
                return true;
            });
        }

        private void Timer_Tick()
        {
            if (!string.IsNullOrWhiteSpace(SmartOtpModels.Secret))
                _ = UpdateTotpAsync(SmartOtpModels);
        }

        private Task UpdateTotpAsync(SmartOtpModel smartOtpModel)
        {
            var totp = new Totp(secretKey: smartOtpModel.GetSecret(),
                step: smartOtpModel.Period,
                totpSize: smartOtpModel.Digits,
                mode: smartOtpModel.HashMode());

            var otp = totp.ComputeTotp(DateTime.Now);

            smartOtpModel.UpdateTotp(totp.ComputeTotp(DateTime.Now), totp.RemainingSeconds(DateTime.Now));

            return Task.CompletedTask;
        }
        private async Task SaveSettings()
        {
            if (string.IsNullOrEmpty(Secret))
            {
                return;
            }

            SmartOtpModels = new SmartOtpModel
            {
                IsSha1 = IsSha1,
                IsSha256 = IsSha256,
                IsTotp = IsTotp,
                IsSha512 = IsSha512,
                Secret = Secret,
                Period = TimeStep,
                Digits = OtpLength,
                PeriodView = TimeStep,
                Counter = Counter,
            };

            Secret = string.Empty;
        }
    }
}