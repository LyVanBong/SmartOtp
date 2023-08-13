namespace SmartOtp.Services.Database;

public class DatabaseService : IDatabaseService
{
    private SQLiteConnection _db;
    public DatabaseService()
    {
        Initialize();
    }

    private void Initialize()
    {
        try
        {
            if (_db is not null)
                return;
            _db = new SQLiteConnection(DatabaseSetting.DatabasePath, DatabaseSetting.Flags);
            _db.CreateTable<SmartOtpModel>();
        }
        catch (Exception e)
        {
            Debug.WriteLine("Error: " + e);
        }
    }

    public bool AddData(SmartOtpModel data)
    {
        return _db.Insert(data) > 0;
    }

    public bool UpdateData(SmartOtpModel data)
    {
        return _db.Update(data) > 0;
    }

    public bool DeleteData(SmartOtpModel data)
    {
        return _db.Delete(data) > 0;
    }

    public SmartOtpModel GetData(Guid id)
    {
        return _db.Find<SmartOtpModel>(id);
    }

    public IEnumerable<SmartOtpModel> GetDatas()
    {
        return _db.Table<SmartOtpModel>().ToList();
    }

    public void DeleteAllData()
    {
        _db.DeleteAll<SmartOtpModel>();
    }
}