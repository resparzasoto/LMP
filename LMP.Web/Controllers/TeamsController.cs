using LMP.Entities;
using LMP.Web.DAL.SQLServer;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace LMP.Web.Controllers
{
    public class TeamsController : ApiController
    {
        private readonly TeamsProvider teamsProvider = new TeamsProvider();

        public async Task<IEnumerable<Team>> Get()
        {
            return await teamsProvider.GetAllTeamsAsync();
        }
    }
}
