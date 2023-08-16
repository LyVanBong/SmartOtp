﻿using OtpNet;
using Prism.Navigation;
using SmartOtp.XamarinForm.Models;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SmartOtp.XamarinForm.ViewModels
{
    public class HotpPageViewModel : ViewModelBase
    {
        private bool _isSha1 = true;
        private bool _isSha256;
        private bool _isSha512;
        private int _otpLength = 6;
        private int _timeStep = 30;
        private bool _isTotp;
        private bool _isHotp = true;
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
        public HotpPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            SaveSettingsCommand = new Command(SaveSettings);
            SelectCodeCommand = new Command<string>(SelectCodeAsync);
        }
        private async void SelectCodeAsync(string arg)
        {
            if (arg == null)
                return;
            await Clipboard.SetTextAsync(arg);
        }

        private Task UpdateHotpAsync(SmartOtpModel smartOtpModel)
        {
            var hotp = new Hotp(secretKey: smartOtpModel.GetSecret(),
                mode: smartOtpModel.HashMode(),
                hotpSize: smartOtpModel.Digits);

            smartOtpModel.UpdateHotp(hotp.ComputeHOTP(smartOtpModel.Counter));

            return Task.CompletedTask;
        }

        private async void SaveSettings()
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

            _ = UpdateHotpAsync(SmartOtpModels);
            
            Secret = string.Empty;
        }
    }
}