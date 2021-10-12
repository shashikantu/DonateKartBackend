using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DonateKart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaingnListController : ControllerBase
    {
        static string _address = "https://testapi.donatekart.com/api/campaign";

        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            var result = await GetExternalResponse();
            
            var values = JsonConvert.DeserializeObject<List<CompaingnList>>(result);

            var results = values.Select(x => x).OrderByDescending(x => x.TotalAmount);

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
