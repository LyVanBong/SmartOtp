namespace SmartOtp.Models;

public class SmartOtpParameters : INotifyPropertyChanged
{
    private OtpType _otpType = new();
    private AlgorithmsOtp _algorithmsOtp = new();
    private string _secretKey;

    public string SecretKey
    {
        get => _secretKey;
        set => SetField(ref _secretKey, value);
    }

    public AlgorithmsOtp AlgorithmsOtp
    {
        get => _algorithmsOtp;
        set => SetField(ref _algorithmsOtp, value);
    }

    public OtpType OtpType
    {
        get => _otpType;
        set => SetField(ref _otpType, value);
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