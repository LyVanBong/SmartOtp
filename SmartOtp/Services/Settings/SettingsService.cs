namespace SmartOtp.Services.Settings;

public class SettingsService : ISettingsService
{
    #region Settings Constants



    #endregion

    #region Settings Properties

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
    public int OtpLength
    {
        get => Preferences.Get(nameof(OtpLength), 6);
        set => Preferences.Set(nameof(OtpLength), value);
    }
    public int TimeStep
    {
        get => Preferences.Get(nameof(TimeStep), 30);
        set => Preferences.Set(nameof(TimeStep), value);
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

    #endregion

    #region Constructor

    public SettingsService()
    {
        
    }

    #endregion

    #region Settings Methods

    public void SaveSettings(bool isSha1, bool isSha256, bool isSha512, bool isTotp, bool isHotp, int timeStep, int otpLength)
    {
        IsSha1 = isSha1;
        IsSha256 = isSha256;
        IsSha512 = isSha512;
        IsTotp = isTotp;
        IsHotp = isHotp;
        TimeStep = timeStep;
        OtpLength = otpLength;
    }
    public OtpHashMode GetOtpHashModeAsync()
    {
        if (IsSha256)
            return OtpHashMode.Sha256;
        else if (IsSha512)
            return OtpHashMode.Sha512;
        else
            return OtpHashMode.Sha1;
    }

    public bool GetTotpTypeAsync()
    {
        if (IsTotp)
            return true;
        else
            return false;
    }

    #endregion
}