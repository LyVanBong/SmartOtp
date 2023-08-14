namespace SmartOtp.Services.Otp;

public class OtpService : IOtpService
{
    private List<SmartOtpModel> _otps = new();

    public bool SaveOtp(SmartOtpModel otp)
    {
        _otps.Add(otp);
        return _otps.Contains(otp);
    }

    public IEnumerable<SmartOtpModel> GetOtps()
    {
        return _otps;
    }
}