using LMP.iOS.Services;
using LMP.ServiceInterfaces;
using SQLite;

[assembly: Xamarin.Forms.Dependency(typeof(SQLiteService))]
namespace LMP.iOS.Services
{
    public class SQLiteService : ISQLiteService
    {
        public SQLiteConnection GetConnection()
        {
            var localDBFile =
                System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Surveys.db");

            return new SQLiteConnection(localDBFile);
        }
    }
}