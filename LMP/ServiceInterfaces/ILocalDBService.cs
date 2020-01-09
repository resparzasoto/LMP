using LMP.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMP.ServiceInterfaces
{
    public interface ILocalDBService
    {
        Task<IEnumerable<Survey>> GetAllSurveysAsync();

        Task InsertSurveyAsync(Survey survey);

        Task DeleteSurveyAysnc(Survey survey);
    }
}
