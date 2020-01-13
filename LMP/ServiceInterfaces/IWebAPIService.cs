using LMP.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMP.ServiceInterfaces
{
    public interface IWebAPIService
    {
        Task<IEnumerable<Team>> GetTeamsAsync();

        Task<bool> SaveSurveysAsync(IEnumerable<Survey> surveys);
    }
}
