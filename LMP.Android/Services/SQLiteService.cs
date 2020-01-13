using LMP.Droid.Services;
using LMP.ServiceInterfaces;
using SQLite;

[assembly: Xamarin.Forms.Dependency(typeof(SQLiteService))]
namespace LMP.Droid.Services
{
    public class SQLiteService : ISQLiteService
    {
        public SQLiteConnection GetConnection()
        {
            var localDBFile =
                System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "LMP.db");

            return new SQLiteConnection(localDBFile);
        }
    }
}