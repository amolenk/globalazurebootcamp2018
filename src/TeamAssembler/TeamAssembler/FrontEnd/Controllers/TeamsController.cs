using System;
using System.Collections.Generic;
using System.Fabric;
using System.Fabric.Query;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FrontEnd.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TeamsController : Controller
    {
        private readonly HttpClient httpClient;
        private readonly FabricClient fabricClient;
        private readonly StatelessServiceContext serviceContext;

        public TeamsController(HttpClient httpClient, StatelessServiceContext serviceContext, FabricClient fabricClient)
        {
            this.fabricClient = fabricClient;
            this.httpClient = httpClient;
            this.serviceContext = serviceContext;
        }

        // GET: api/Teams
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            Uri serviceName = GetBackEndServiceName(serviceContext);

            ServicePartitionList partitions = await fabricClient.QueryManager.GetPartitionListAsync(serviceName);

            List<TeamDto> result = new List<TeamDto>();

            foreach (Partition partition in partitions)
            {
                long partitionKey = ((Int64RangePartitionInformation)partition.PartitionInformation).LowKey;
                string proxyUrl = GetProxyUrl(serviceContext, partitionKey);

                using (HttpResponseMessage response = await httpClient.GetAsync(proxyUrl))
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        continue;
                    }

                    result.AddRange(JsonConvert.DeserializeObject<List<TeamDto>>(await response.Content.ReadAsStringAsync()));
                }
            }

            return Json(result);
        }

        // PUT: api/Teams/name
        [HttpPut("{name}")]
        public async Task<IActionResult> Put(string name, [FromBody]string[] members)
        {
            string proxyUrl = GetProxyUrl(serviceContext, name);

            StringContent putContent = new StringContent(
                JsonConvert.SerializeObject(new
                {
                    members,
                    score = -1
                }),
                Encoding.UTF8,
                "application/json");

            putContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            using (HttpResponseMessage response = await httpClient.PutAsync(proxyUrl, putContent))
            {
                return new ContentResult()
                {
                    StatusCode = (int)response.StatusCode,
                    Content = await response.Content.ReadAsStringAsync()
                };
            }
        }

        // DELETE: api/Teams/name
        [HttpDelete("{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            string proxyUrl = GetProxyUrl(serviceContext, name);

            using (HttpResponseMessage response = await httpClient.DeleteAsync(proxyUrl))
            {
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return this.StatusCode((int)response.StatusCode);
                }
            }

            return new OkResult();
        }

        private static Uri GetBackEndServiceName(ServiceContext context)
        {
            return new Uri($"{context.CodePackageActivationContext.ApplicationName}/BackEnd");
        }

        private static string GetProxyUrl(ServiceContext context, string teamName)
        {
            // Create a partition key from the given name.
            // Use the zero-based numeric position in the alphabet of the first letter of the name (0-25).
            long partitionKey = Char.ToUpper(teamName.First()) - 'A';

            return GetProxyUrl(context, partitionKey);
        }

        private static string GetProxyUrl(ServiceContext context, long partitionKey)
        {
            Uri serviceName = GetBackEndServiceName(context);

            return $"http://localhost:19081{serviceName.AbsolutePath}/api/Teams?PartitionKey={partitionKey}&PartitionKind=Int64Range";
        }
    }

    public class TeamDto
    {
        public string Name { get; set; }

        public string[] Members { get; set; }

        public int Score { get; set; }
    }
}
