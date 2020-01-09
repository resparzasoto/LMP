using SQLite;

namespace LMP.ServiceInterfaces
{
    public interface ISQLiteService
    {
        SQLiteConnection GetConnection();
    }
}
