namespace SmartOtp.Models;

public class AlgorithmsOtp : INotifyPropertyChanged
{
    private bool _sha1 = true;
    private bool _sha256;
    private bool _sha512;

    public bool Sha1
    {
        get => _sha1;
        set
        {
            if (value == _sha1) return;
            _sha1 = value;
            OnPropertyChanged();
        }
    }

    public bool Sha256
    {
        get => _sha256;
        set
        {
            if (value == _sha256) return;
            _sha256 = value;
            OnPropertyChanged();
        }
    }

    public bool Sha512
    {
        get => _sha512;
        set => SetField(ref _sha512, value);
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