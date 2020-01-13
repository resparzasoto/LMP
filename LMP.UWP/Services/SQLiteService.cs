using LMP.ServiceInterfaces;
using LMP.UWP.Services;
using SQLite;
using Windows.Storage;

[assembly: Xamarin.Forms.Dependency(typeof(SQLiteService))]
namespace LMP.UWP.Services
{
    public class SQLiteService : ISQLiteService
    {
        public SQLiteConnection GetConnection()
        {
            var localDBFile =
                System.IO.Path.Combine(ApplicationData.Current.LocalFolder.Path, "LMP.db");

            return new SQLiteConnection(localDBFile);
        }
    }
}
