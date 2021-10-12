using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DonateKart
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActiveCompaignsController : ControllerBase
    {
        static string _address = "https://testapi.donatekart.com/api/campaign";

        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            var result = await GetExternalResponse();

            var values = JsonConvert.DeserializeObject<List<ActiveCompaingnList>>(result);

            var creationDateBewtween30days = values.Where(x => x.EndDate > System.DateTime.Now).Select(x => x);

            var results = creationDateBewtween30days.Select(x => x).Where(x => x.created > System.DateTime.Now.AddDays(-30));

            var output = JsonConvert.SerializeObject(results);

            return new string[] { output };
        }



        private async Task<string> GetExternalResponse()
        {
            var client = new HttpClient();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = new HttpResponseMessage();
            response = await client.GetAsync(_address);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}
