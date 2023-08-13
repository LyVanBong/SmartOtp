namespace SmartOtp.Services.Settings;

public interface ISettingsService
{
    public bool IsSha1 { get; set; }
    public bool IsSha256 { get; set; }
    public bool IsSha512 { get; set; }
    public int OtpLength { get; set; }
    public int TimeStep { get; set; }
    public bool IsTotp { get; set; }
    public bool IsHotp { get; set; }
    /// <summary>
    /// Save settings
    /// </summary>
    /// <param name="isSha1"></param>
    /// <param name="isSha256"></param>
    /// <param name="isSha512"></param>
    /// <param name="isTotp"></param>
    /// <param name="isHotp"></param>
    /// <param name="timeStep"></param>
    /// <param name="otpLength"></param>
    void SaveSettings(bool isSha1, bool isSha256, bool isSha512, bool isTotp, bool isHotp, int timeStep, int otpLength);
    /// <summary>
    /// Otp hash mode
    /// </summary>
    /// <returns>
    /// sha1, sha256 or sha512
    /// </returns>
    OtpHashMode GetOtpHashModeAsync();
    /// <summary>
    /// Otp type
    /// </summary>
    /// <returns>
    /// True if TOTP, false if HOTP
    /// </returns>
    bool GetTotpTypeAsync();
}