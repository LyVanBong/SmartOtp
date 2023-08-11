using OtpNet;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SmartOtp.Models;

public class SmartOtpCode : INotifyPropertyChanged
{
    private string _otpCode;
    private int _timeStep;

    public Totp Totp { get; set; }

    public Hotp Hotp { get; set; }
    public long Counter { get; set; }
    public OtpHashMode Mode { get; set; }
    public string OtpCode
    {
        get => _otpCode;
        set => SetField(ref _otpCode, value);
    }

    public int TimeStep
    {
        get => _timeStep;
        set => SetField(ref _timeStep, value);
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