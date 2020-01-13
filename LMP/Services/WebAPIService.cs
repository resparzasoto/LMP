using LMP.Entities;
using LMP.ServiceInterfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace LMP.Services
{
    public class WebAPIService : IWebAPIService
    {
        private readonly HttpClient client;

        public WebAPIService()
        {
            client = new HttpClient() { BaseAddress = new Uri(Literals.WebAPIServiceBaseAddress) };
        }

        public async Task<IEnumerable<Team>> GetTeamsAsync()
        {
            IEnumerable<Team> result = null;
            var teams = await client.GetStringAsync("api/teams");

            if (!string.IsNullOrWhiteSpace(teams))
            {
                result = JsonConvert.DeserializeObject<IEnumerable<Team>>(teams);
            }

            return result;
        }

        public async Task<bool> SaveSurveysAsync(IEnumerable<Survey> surveys)
        {
            var content = new StringContent(JsonConvert.SerializeObject(surveys), System.Text.Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/surveys", content);

            return response.IsSuccessStatusCode;
        }
    }
}
