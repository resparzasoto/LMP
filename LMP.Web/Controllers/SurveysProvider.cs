using LMP.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LMP.Web.DAL.SQLServer
{
    public class SurveysProvider : SQLServerProvider
    {
        public override string ConnectionString { get; set; } =
            ConfigurationManager.ConnectionStrings["LMP"].ConnectionString;

        public async Task<IEnumerable<Survey>> GetAllSurveysAsync()
        {
            var result = new List<Survey>();

            string query = @"SELECT Id, Name, Birthdate, TeamId, Lat, Lon
                             FROM LMP.dbo.Surveys WITH (NOLOCK)";

            using (var reader = await ExecuteReaderAsync(query))
            {
                while (reader.Read())
                {
                    result.Add(GetSurveyFromReader(reader));
                }
            }

            return result;
        }

        public async Task<int> InsertSurveyAsync(Survey survey)
        {
            if (survey is null)
            {
                return 0;
            }

            string query = @"INSERT INTO LMP.dbo.Surveys (Id, Name, BirthDate, TeamId, Lat, Lon)
                             VALUES (@Id, @Name, @BirthDate, @TeamId, @Lat, @Lon)";

            var parameters = new List<SqlParameter>()
            {
                new SqlParameter("@Id", survey.Id),
                new SqlParameter("@Name", survey.Name),
                new SqlParameter("@BirthDate", survey.Birthdate),
                new SqlParameter("@TeamId", survey.TeamId),
                new SqlParameter("@Lat", survey.Lat),
                new SqlParameter("@Lon", survey.Lon)
            };

            return await ExecuteNonQueryAsync(query, parameters.ToArray()); ;
        }

        private Survey GetSurveyFromReader(SqlDataReader reader)
        {
            return new Survey()
            {
                Id = reader[nameof(Survey.Id)].ToString(),
                Name = reader[nameof(Survey.Name)].ToString(),
                Birthdate = (DateTime)reader[nameof(Survey.Birthdate)],
                TeamId = (int)reader[nameof(Survey.TeamId)],
                Lat = Convert.ToDouble(reader[nameof(Survey.Lat)]),
                Lon = Convert.ToDouble(reader[nameof(Survey.Lon)]),
            };
        }
    }
}
