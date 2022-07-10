using System.Collections.Generic;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FunctionApp1
{
    public class Function1
    {
        private readonly ILogger _logger;

        public Function1(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Function1>();
        }

        [Function("Learningtocode1")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
            var Querystring=Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(req.Url.Query);
            var result = Querystring["name"];


          
            response.WriteString("Welcome to Azure function via Get method!" + result);

            return response;
        }
        [Function("learningtogethttpreq")]
        public HttpResponseData Runpost([HttpTrigger(AuthorizationLevel.Anonymous,"post")] HttpRequestData req)
        {
            string name = null;
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8") ;
            string responsedata = new StreamReader(req.Body).ReadToEnd();
            if(responsedata != null)
            {
                dynamic data = JsonConvert.DeserializeObject(responsedata);
                name = data.name;
            
            }
            response.WriteString("Welcome to Azure function via post method!" + name);


            return response;
        }
    }
}
