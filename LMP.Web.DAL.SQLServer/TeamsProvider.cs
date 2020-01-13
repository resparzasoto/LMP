using LMP.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LMP.Web.DAL.SQLServer
{
    public class TeamsProvider : SQLServerProvider
    {
        public override string ConnectionString { get; set; } =
            ConfigurationManager.ConnectionStrings["LMP"].ConnectionString;

        public async Task<IEnumerable<Team>> GetAllTeamsAsync()
        {
            var result = new List<Team>();

            string query = @"SELECT Id, Name, Color, Logo
                             FROM LMP.dbo.Teams WITH (NOLOCK)";

            using (var reader = await ExecuteReaderAsync(query))
            {
                while (reader.Read())
                {
                    result.Add(GetTeamFromReader(reader));
                }
            }

            return result;
        }

        private Team GetTeamFromReader(SqlDataReader reader)
        {
            return new Team()
            {
                Id = (int)reader[nameof(Team.Id)],
                Name = reader[nameof(Team.Name)].ToString(),
                Color = reader[nameof(Team.Color)].ToString(),
                Logo = reader[nameof(Team.Logo)] is DBNull ? null : (byte[])reader[nameof(Team.Logo)]
            };
        }
    }
}
