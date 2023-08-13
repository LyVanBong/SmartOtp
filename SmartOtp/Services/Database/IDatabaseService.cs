namespace SmartOtp.Services.Database;

public interface IDatabaseService
{
    /// <summary>
    /// Add data to database
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    bool AddData(SmartOtpModel data);
    /// <summary>
    /// Update data to database
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    bool UpdateData(SmartOtpModel data);
    /// <summary>
    /// Delete data from database
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    bool DeleteData(SmartOtpModel data);
    /// <summary>
    /// Get data from database
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id"></param>
    /// <returns></returns>
    SmartOtpModel GetData(Guid id);
    /// <summary>
    /// Get all data from database
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    IEnumerable<SmartOtpModel> GetDatas();
    /// <summary>
    /// Delete all data from database
    /// </summary>
    void DeleteAllData();
}