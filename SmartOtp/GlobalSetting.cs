namespace SmartOtp;

public class GlobalSetting
{
}

public class DatabaseSetting
{
    public const string DatabaseName = "dbtotp.db3";
    public static string DatabasePath = Path.Combine(FileSystem.AppDataDirectory, DatabaseName);
    public const SQLiteOpenFlags Flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache;
}

public class Routes
{
    public const string Hotp = "Hotp";
    public const string CreateQrCode = "CreateQrCode";
    public const string SetupCode = "SetupCode";
    public const string ScanQrCode = "ScanQrCode";
    public const string Home = "Home";
    public const string AddCode = "AddCode";
    public const string Settings = "Settings";
}