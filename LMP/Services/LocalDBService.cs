using LMP.Models;
using LMP.ServiceInterfaces;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMP.Services
{
    public class LocalDBService : ILocalDBService
    {
        private readonly SQLiteConnection connection;

        public LocalDBService()
        {
            connection = Xamarin.Forms.DependencyService.Get<ISQLiteService>().GetConnection();

            CreateDatabase();
        }

        private void CreateDatabase()
        {
            connection.CreateTable<Survey>();
        }

        public Task<IEnumerable<Survey>> GetAllSurveysAsync()
        {
            return Task.Run(() => (IEnumerable<Survey>)connection.Table<Survey>().ToArray());
        }

        public Task InsertSurveyAsync(Survey survey)
        {
            return Task.Run(() => connection.Insert(survey));
        }

        public Task DeleteSurveyAysnc(Survey survey)
        {
            return Task.Run(() =>
            {
                var query = $"DELETE " +
                            $"FROM Survey " +
                            $"WHERE Id = '{survey.Id}'";

                var command = connection.CreateCommand(query);

                var result = command.ExecuteNonQuery();

                return result > 0;
            });
        }
    }
}
