using OtpNet;
using SmartOtp.Models;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace SmartOtp;

public class MainViewModel : INotifyPropertyChanged
{
    private Models.SmartOtpParameters _smartOtpParameters = new Models.SmartOtpParameters();
    private SmartOtpCode _smartOtpCode = new SmartOtpCode();
    private int _step = 30;
    public ICommand CreateOtpCommand { get; private set; }

    public Models.SmartOtpParameters SmartOtpParameters
    {
        get => _smartOtpParameters;
        set => SetField(ref _smartOtpParameters, value);
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

    private void RefreshSmartOtp(object state)
    {
        Debug.WriteLine("Timer: " + DateTime.Now);

        if (SmartOtpParameters.SecretKey == null) return;

        if (SmartOtpCode.OtpType)
        {
            var totp = new Totp(secretKey: SecretKey(SmartOtpCode.SecretKey), mode: SmartOtpCode.Mode);

            var remainingTime = totp.RemainingSeconds(DateTime.Now);
            SmartOtpCode.TimeStep = remainingTime;
            if (remainingTime == 1)
            {
                var totpCode = totp.ComputeTotp(DateTime.Now);

                SmartOtpCode.OtpCode = totpCode;

                Debug.WriteLine("totpCode: " + totpCode);
            }
        }
        else
        {
            _step--;
            SmartOtpCode.TimeStep = _step;
            if (_step == 1)
            {
                var hotp = new Hotp(secretKey: SecretKey(SmartOtpCode.SecretKey), mode: SmartOtpCode.Mode);
                var hotpCode = hotp.ComputeHOTP(SmartOtpCode.Counter);
                Debug.WriteLine("hotpCode: " + hotpCode);
                SmartOtpCode.OtpCode = hotpCode;
                SmartOtpCode.Counter = long.Parse(hotpCode);
                _step = 30;
            }
        }
    }

    private void CreateOtp()
    {
        if (!string.IsNullOrWhiteSpace(SmartOtpParameters.SecretKey))
        {
            if (SmartOtpParameters.OtpType.Totp)
            {
                var mode = OtpHashMode(SmartOtpParameters.AlgorithmsOtp);

                var totp = new Totp(secretKey: SecretKey(SmartOtpParameters.SecretKey), mode: mode);

                var totpCode = totp.ComputeTotp(DateTime.Now);

                SmartOtpCode.OtpCode = totpCode;

                var remainingTime = totp.RemainingSeconds(DateTime.Now);

                SmartOtpCode.TimeStep = remainingTime;

                SmartOtpCode.SecretKey = SmartOtpParameters.SecretKey;
                SmartOtpCode.Mode = mode;
                SmartOtpCode.OtpType = true;
            }
            else if (SmartOtpParameters.OtpType.Hotp)
            {
                SmartOtpCode.Counter = 30;

                var mode = OtpHashMode(SmartOtpParameters.AlgorithmsOtp);

                var hotp = new Hotp(secretKey: SecretKey(SmartOtpParameters.SecretKey), mode: mode);

                var hotpCode = hotp.ComputeHOTP(SmartOtpCode.Counter);

                SmartOtpCode.Counter = long.Parse(hotpCode);
                SmartOtpCode.OtpCode = hotpCode;
                SmartOtpCode.SecretKey = SmartOtpParameters.SecretKey;
                SmartOtpCode.Mode = mode;
                SmartOtpCode.OtpType = false;
            }
        }
        else
        {
            Application.Current.MainPage.DisplayAlert("Thông báo", "khóa bảo mật không được trống !", "OK");
        }
        SmartOtpParameters.SecretKey = string.Empty;
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