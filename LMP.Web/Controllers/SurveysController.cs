using LMP.Entities;
using LMP.Web.DAL.SQLServer;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace LMP.Web.Controllers
{
    public class SurveysController : ApiController
    {
        private readonly SurveysProvider surveysProvider = new SurveysProvider();

        public async Task<IEnumerable<Survey>> Get()
        {
            return await surveysProvider.GetAllSurveysAsync();
        }

        public async Task Post([FromBody] IEnumerable<Survey> surveys)
        {
            if (surveys is null)
            {
                return;
            }

            foreach (var survey in surveys)
            {
                await surveysProvider.InsertSurveyAsync(survey);
            }
        }
    }
}
