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
    public class ClosedCompaingnsController : ControllerBase
    {
        static string _address = "https://testapi.donatekart.com/api/campaign";

        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            var result = await GetExternalResponse();
            List<ClosedCompaingns> closedCompaingn = new List<ClosedCompaingns>();

            var values = JsonConvert.DeserializeObject<List<ClosedCompaingns>>(result);

            foreach (var item in values)
            {
                if(item.EndDate<System.DateTime.Now || item.TotalAmount<item.TotalProcured)
                {
                    closedCompaingn.Add(item);
                }
            }

            var output = JsonConvert.SerializeObject(closedCompaingn);

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

