namespace SmartOtp.Models;
[Table(nameof(SmartOtpModel))]
public class SmartOtpModel : ObservableObject
{
    private string _otp;
    private string _issuer;
    private string _user;

    [PrimaryKey]
    public Guid Id { get; set; } = Guid.NewGuid();

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

    public string Secret { get; set; }
    public int Digits { get; set; }
    public int Period { get; set; }
    [Ignore]
    public int PeriodView { get; set; }
    public long Counter { get; set; }
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
            return OtpNet.OtpHashMode.Sha1;
        if (IsSha256)
            return OtpHashMode.Sha256;
        if (IsSha512)
            return OtpHashMode.Sha512;
        return OtpNet.OtpHashMode.Sha1;
    }
}