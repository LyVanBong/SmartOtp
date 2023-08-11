using OtpNet;
using SmartOtp.Models;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace SmartOtp;

public class MainViewModel : INotifyPropertyChanged
{
    private Models.SmartOtp _smartOtp = new Models.SmartOtp();
    private SmartOtpCode _smartOtpCode = new SmartOtpCode();
    private Totp _totp;
    private Hotp _hotp;
    public ICommand CreateOtpCommand { get; private set; }

    public Models.SmartOtp SmartOtp
    {
        get => _smartOtp;
        set => SetField(ref _smartOtp, value);
    }

    public SmartOtpCode SmartOtpCode
    {
        get => _smartOtpCode;
        set => SetField(ref _smartOtpCode, value);
    }
    public MainViewModel()
    {
        CreateOtpCommand = new Command(CreateOtp);

        _ = RefreshOtp();
    }

    private Task RefreshOtp()
    {
        var timer = Application.Current.Dispatcher.CreateTimer();
        timer.Interval = TimeSpan.FromSeconds(1);
        timer.Tick += (sender, args) => RefreshSmartOtp(args);
        timer.Start();
        return Task.CompletedTask;
    }

    private int _step = 30;
    private void RefreshSmartOtp(object state)
    {
        Debug.WriteLine("Timer: " + DateTime.Now);
        if (SmartOtpCode.Hotp != null)
        {
            _step--;

            if (_step == 1)
            {
                var hotpCode = SmartOtpCode.Hotp.ComputeHOTP(SmartOtpCode.Counter);
                Debug.WriteLine("hotpCode: " + hotpCode);
                SmartOtpCode.OtpCode = hotpCode;
                SmartOtpCode.Counter = long.Parse(hotpCode);
                _step = 30;
            }

            SmartOtpCode.TimeStep = _step;
        }
        else if (SmartOtpCode.Totp != null)
        {
            var remainingTime = SmartOtpCode.Totp.RemainingSeconds(DateTime.Now);
            SmartOtpCode.TimeStep = remainingTime;
            if (remainingTime == 1)
            {
                var totpCode = SmartOtpCode.Totp.ComputeTotp(DateTime.Now);

                SmartOtpCode.OtpCode = totpCode;

                Debug.WriteLine("Otp: " + totpCode);
            }
        }
    }

    private void CreateOtp()
    {
        if (!string.IsNullOrWhiteSpace(SmartOtp.SecretKey))
        {
            if (SmartOtp.OtpType.Totp)
            {
                var mode = OtpHashMode(SmartOtp.AlgorithmsOtp);

                _totp = new Totp(secretKey: SecretKey(SmartOtp.SecretKey), mode: mode);

                var totpCode = _totp.ComputeTotp(DateTime.Now);

                SmartOtpCode.OtpCode = totpCode;

                var remainingTime = _totp.RemainingSeconds(DateTime.Now);

                SmartOtpCode.TimeStep = remainingTime;

                SmartOtp.SecretKey = SmartOtp.SecretKey;
                SmartOtpCode.Mode = mode;
                SmartOtpCode.Totp = _totp;
                SmartOtpCode.Hotp = null;
            }
            else if (SmartOtp.OtpType.Hotp)
            {
                SmartOtpCode.Counter = 30;

                var mode = OtpHashMode(SmartOtp.AlgorithmsOtp);

                _hotp = new Hotp(secretKey: SecretKey(SmartOtp.SecretKey), mode: mode);

                var hotpCode = _hotp.ComputeHOTP(SmartOtpCode.Counter);

                SmartOtpCode.Counter = long.Parse(hotpCode);
                SmartOtpCode.OtpCode = hotpCode;
                SmartOtp.SecretKey = SmartOtp.SecretKey;
                SmartOtpCode.Mode = mode;
                SmartOtpCode.Totp = null;
                SmartOtpCode.Hotp = _hotp;
            }
        }
        else
        {
            Application.Current.MainPage.DisplayAlert("Thông báo", "khóa bảo mật không được trống !", "OK");
        }
        SmartOtp.SecretKey = string.Empty;
    }

    private byte[] SecretKey(string secretKey)
    {
        return Encoding.ASCII.GetBytes(secretKey);
    }

    private OtpHashMode OtpHashMode(AlgorithmsOtp algorithms)
    {
        OtpHashMode mode = OtpNet.OtpHashMode.Sha1;
        if (algorithms.Sha1)
        {
            mode = OtpNet.OtpHashMode.Sha1;
        }
        else if (algorithms.Sha256)
        {
            mode = OtpNet.OtpHashMode.Sha256;
        }
        else if (algorithms.Sha512)
        {
            mode = OtpNet.OtpHashMode.Sha512;
        }

        return mode;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}