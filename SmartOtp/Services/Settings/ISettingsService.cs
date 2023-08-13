namespace SmartOtp.Services.Settings;

public interface ISettingsService
{
    public long Counter { get; set; }
    public bool IsSha1 { get; set; }
    public bool IsSha256 { get; set; }
    public bool IsSha512 { get; set; }
    public int Digits { get; set; }
    public int Period { get; set; }
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
    /// <param name="period"></param>
    /// <param name="digits"></param>
    /// <param name="counter"></param>
    void SaveSettings(bool isSha1 = true,
        bool isSha256 = false,
        bool isSha512 = false,
        bool isTotp = true,
        bool isHotp = false,
        int period = 30,
        int digits = 6,
        long counter = 0);
}