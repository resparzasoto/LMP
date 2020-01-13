using LMP.Entities;
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
            connection.CreateTable<Team>();
        }

        public Task<IEnumerable<Survey>> GetAllSurveysAsync()
        {
            return Task.Run(() => (IEnumerable<Survey>)connection.Table<Survey>().ToArray());
        }

        public Task InsertSurveyAsync(Survey survey)
        {
            return Task.Run(() => connection.Insert(survey));
        }

        public Task DeleteSurveyAsync(Survey survey)
        {
            return Task.Run(() =>
            {
                var query = @"DELETE FROM Survey WHERE Id = ?";

                var command = connection.CreateCommand(query, new object[] { survey.Id });

                var result = command.ExecuteNonQuery();

                return result > 0;
            });
        }

        public Task DeleteAllSurveysAsync()
        {
            return Task.Run(() => connection.DeleteAll<Survey>() > 0);
        }

        public Task DeleteAllTeamsAsync()
        {
            return Task.Run(() => connection.DeleteAll<Team>() > 0);
        }

        public Task InsertTeamsAsync(IEnumerable<Team> teams)
        {
            return Task.Run(() => connection.InsertAll(teams));
        }

        public Task<IEnumerable<Team>> GetAllTeamsAsync()
        {
            return Task.Run(() => (IEnumerable<Team>)connection.Table<Team>().ToArray());
        }
    }
}
