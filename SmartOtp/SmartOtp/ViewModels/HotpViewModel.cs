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
    public class HotpViewModel : ViewModelBase
    {
        private HotpModel _hotpModel = new HotpModel();
        private string _otp;
        private string _counter;

        public HotpModel HotpModel
        {
            get => _hotpModel;
            set => SetField(ref _hotpModel, value);
        }

        public string Counter
        {
            get => _counter;
            set => SetField(ref _counter, value);
        }

        public string Otp
        {
            get => _otp;
            set => SetField(ref _otp, value);
        }

        public ICommand CopyCommand { get; private set; }
        public ICommand GenerateCommand { get; private set; }

        public HotpViewModel()
        {
            GenerateCommand = new AsyncCommand(Generate);
            CopyCommand = new AsyncCommand<string>(Copy);
        }

        private async Task Generate()
        {
            if (string.IsNullOrWhiteSpace(HotpModel.Secret))
            {
                await Application.Current.MainPage.DisplayAlert("Thông báo", "Khóa bí mật không được để trống !", "OK");
            }
            else
            {
                var key = Base32Encoding.ToBytes(HotpModel.Secret);
                var hotp = new Hotp(secretKey: key, hotpSize: HotpModel.Digits, mode: HotpModel.OtpMode());
                Otp = hotp.ComputeHOTP(HotpModel.Counter);
                Counter = HotpModel.Counter + "";
                HotpModel.Secret = string.Empty;
                await Application.Current.MainPage.DisplayToastAsync("Tạo mã thành công");
            }
        }

        private async Task Copy(string otp)
        {
            if (string.IsNullOrWhiteSpace(otp))
                return;
            await Application.Current.MainPage.DisplayToastAsync("Copy mã otp: " + otp);
            await Clipboard.SetTextAsync(otp);
        }
    }
}