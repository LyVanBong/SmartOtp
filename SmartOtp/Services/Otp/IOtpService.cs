namespace SmartOtp.Services.Otp;

public interface IOtpService
{
    bool SaveOtp(SmartOtpModel otp);

    IEnumerable<SmartOtpModel> GetOtps();
}