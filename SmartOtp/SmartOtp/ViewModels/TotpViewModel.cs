using System;
using OtpNet;
using SmartOtp.Models;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SmartOtp.ViewModels
{
    public class TotpViewModel : ViewModelBase
    {
        private string _otp;
        private string _periodView;
        private TotpModel _totpModel = new TotpModel();
        private TotpModel _saveTotp = null;

        public TotpModel TotpModel
        {
            get => _totpModel;
            set => SetField(ref _totpModel, value);
        }

        public string Otp
        {
            get => _otp;
            set => SetField(ref _otp, value);
        }

        public string PeriodView
        {
            get => _periodView;
            set => SetField(ref _periodView, value);
        }

        public ICommand CopyCommand { get; private set; }
        public ICommand GenerateCommand { get; private set; }

        public TotpViewModel()
        {
            CopyCommand = new AsyncCommand<string>(Copy);
            GenerateCommand = new AsyncCommand(Generate);

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                if (_saveTotp != null)
                {
                    var key = Base32Encoding.ToBytes(_saveTotp.Secret);
                    var totp = new Totp(secretKey: key,
                        step: _saveTotp.Period,
                        mode: _saveTotp.OtpMode(),
                        totpSize: _saveTotp.Digits);
                    Otp = totp.ComputeTotp(DateTime.UtcNow);
                    PeriodView = totp.RemainingSeconds(DateTime.UtcNow) + "";
                }
                return true;
            });
        }

        private async Task Copy(string otp)
        {
            if (string.IsNullOrWhiteSpace(otp))
                return;
            await Application.Current.MainPage.DisplayToastAsync("Copy mã otp: " + otp);
            await Clipboard.SetTextAsync(otp);
        }

        private async Task Generate()
        {
            if (string.IsNullOrWhiteSpace(TotpModel.Secret))
            {
                await Application.Current.MainPage.DisplayAlert("Thông báo", "Khóa bí mật không được để trống !", "OK");
            }
            else
            {
                _saveTotp = new TotpModel
                {
                    IsSha1 = TotpModel.IsSha1,
                    IsSha512 = TotpModel.IsSha512,
                    IsSha256 = TotpModel.IsSha256,
                    Period = TotpModel.Period,
                    Digits = TotpModel.Digits,
                    Secret = TotpModel.Secret,
                };
                TotpModel.Secret = string.Empty;
                await Application.Current.MainPage.DisplayToastAsync("Tạo mã thành công");
            }
        }
    }
}