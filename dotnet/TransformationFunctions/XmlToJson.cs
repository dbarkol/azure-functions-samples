using System.Xml;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace TransformationFunctions
{
    public static class XmlToJson
    {
        [FunctionName("XmlToJson")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("XmlToJson triggered");

            // Check for content-type
            if (req.ContentType.IndexOf(@"/xml", 0, System.StringComparison.OrdinalIgnoreCase) == -1)
            {
                return new BadRequestObjectResult(@"Content-Type header must be an XML content type");
            }

            var doc = new XmlDocument();
            doc.Load(req.Body);

            return new OkObjectResult(doc);
        }
    }
}
