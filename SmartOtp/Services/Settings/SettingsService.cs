namespace SmartOtp.Services.Settings;

public class SettingsService : ISettingsService
{
    #region Settings Properties

    public long Counter
    {
        get => Preferences.Get(nameof(Counter), 0L);
        set => Preferences.Set(nameof(Counter), value);
    }

    public bool IsSha1
    {
        get => Preferences.Get(nameof(IsSha1), true);
        set => Preferences.Set(nameof(IsSha1), value);
    }

    public bool IsSha256
    {
        get => Preferences.Get(nameof(IsSha256), false);
        set => Preferences.Set(nameof(IsSha256), value);
    }

    public bool IsSha512
    {
        get => Preferences.Get(nameof(IsSha512), false);
        set => Preferences.Set(nameof(IsSha512), value);
    }

    public int Digits
    {
        get => Preferences.Get(nameof(Digits), 6);
        set => Preferences.Set(nameof(Digits), value);
    }

    public int Period
    {
        get => Preferences.Get(nameof(Period), 30);
        set => Preferences.Set(nameof(Period), value);
    }

    public bool IsTotp
    {
        get => Preferences.Get(nameof(IsTotp), true);
        set => Preferences.Set(nameof(IsTotp), value);
    }

    public bool IsHotp
    {
        get => Preferences.Get(nameof(IsHotp), false);
        set => Preferences.Set(nameof(IsHotp), value);
    }

    #endregion Settings Properties

    #region Settings Methods

    public void SaveSettings(bool isSha1 = true,
        bool isSha256 = false,
        bool isSha512 = false,
        bool isTotp = true,
        bool isHotp = false,
        int period = 30,
        int digits = 6,
        long counter = 0)
    {
        IsSha1 = isSha1;
        IsSha256 = isSha256;
        IsSha512 = isSha512;
        IsTotp = isTotp;
        IsHotp = isHotp;
        Period = period;
        Digits = digits;
        Counter = counter;
    }

    #endregion Settings Methods
}