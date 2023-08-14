using System.Text;

namespace SmartOtp.Models;

[Table(nameof(SmartOtpModel))]
public class SmartOtpModel : ObservableObject
{
    private string _otp = "";
    private string _issuer;
    private string _user;
    private int _periodView;
    private float _progress = .5f;
    private long _counter;

    [PrimaryKey]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Ignore]
    public float Progress
    {
        get => _progress;
        set => SetProperty(ref _progress, value);
    }

    [Ignore]
    public string Otp
    {
        get => _otp;
        set => SetProperty(ref _otp, value);
    }

    public string Issuer
    {
        get => _issuer;
        set => SetProperty(ref _issuer, value);
    }

    public string User
    {
        get => _user;
        set => SetProperty(ref _user, value);
    }

    public string Secret { get; set; } = string.Empty;
    public int Digits { get; set; }
    public int Period { get; set; }

    [Ignore]
    public int PeriodView
    {
        get => _periodView;
        set => SetProperty(ref _periodView, value);
    }

    public long Counter
    {
        get => _counter;
        set => SetProperty(ref _counter, value);
    }

    public DateTime CreateTime { get; set; } = DateTime.Now;

    // HashType
    public bool IsSha1 { get; set; }

    public bool IsSha256 { get; set; }
    public bool IsSha512 { get; set; }

    // OtpType
    public bool IsTotp { get; set; }

    /// <summary>
    /// OtpHashMode
    /// </summary>
    /// <returns></returns>
    public OtpHashMode HashMode()
    {
        if (IsSha1)
            return OtpHashMode.Sha1;
        if (IsSha256)
            return OtpHashMode.Sha256;
        if (IsSha512)
            return OtpHashMode.Sha512;
        return OtpHashMode.Sha1;
    }
    /// <summary>
    /// Convert string to byte[]
    /// </summary>
    /// <returns></returns>
    public byte[] GetSecret()
    {
        return Encoding.UTF8.GetBytes(Secret);
    }

    public void UpdateHotp(string otp)
    {
        PeriodView--;
        Issuer = "HOTP";
        Progress = (float)PeriodView / Period;
        Otp = otp;
        if (PeriodView == 1)
        {
            Counter++;
            PeriodView = Period;
        }
    }

    public void UpdateTotp(string otp = null, int period = 0)
    {
        PeriodView = period;
        Issuer = "TOTP";
        Otp = otp;
        Progress = (float)PeriodView / Period;
    }
}