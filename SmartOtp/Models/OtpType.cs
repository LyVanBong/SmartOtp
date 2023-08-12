namespace SmartOtp.Models;

public class OtpType : INotifyPropertyChanged
{
    private bool _totp = true;
    private bool _hotp;

    public bool Totp
    {
        get => _totp;
        set
        {
            if (value == _totp) return;
            _totp = value;
            OnPropertyChanged();
        }
    }

    public bool Hotp
    {
        get => _hotp;
        set
        {
            if (value == _hotp) return;
            _hotp = value;
            OnPropertyChanged();
        }
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