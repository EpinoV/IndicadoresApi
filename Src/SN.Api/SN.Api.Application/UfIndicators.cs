using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SN.Api.Application
{
    public class UfIndicators
    {
        private static readonly string svcURL = "https://www.mindicador.cl";
        private static readonly string pathAPI = "api";
        private static readonly string indicator = "uf";

        private DateTime date;

        public UfIndicators(DateTime dateRequest)
        {
            date = dateRequest;
        }

        public string Get()
        {
            var result = Task.Run(() => ConsumeExternalApi()).Result;
            return result.ToString();
        }

        private async Task<Object> ConsumeExternalApi()
        {
            var url = $"{pathAPI}/{indicator}/{date.ToString("dd-MM-yyyy")}";
            var resultRequest = string.Empty;

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(svcURL);
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    resultRequest = response.Content.ReadAsStringAsync().Result;
                }
            }

            return JsonConvert.DeserializeObject(resultRequest);
        }
    }
}
