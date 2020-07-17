using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
// using Newtonsoft.Json;
// using Newtonsoft.Json.Serialization;

namespace API.Helpers
{
    public static class Extensions
    {
        // public static void ReadHeaders(this HttpRequestMessage request)
        // {
        //     var id = request.Headers.GetValues("count").FirstOrDefault();
        // }

        public static int TestExtension(this HttpRequest request, string message)
        {

            StringValues value = "";
            request.Headers.TryGetValue(message, out value);
            return Convert.ToInt32(value);
        }        

        public static void AddTweetCount(this HttpResponse response, string tweetCount)
        {
            // var camelCaseFormatter = new JsonSerializerSettings();
            // camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver(); 
            response.Headers.Add("Access-Control-Expose-Headers", "ResultCount");           
            response.Headers.Add("ResultCount", tweetCount);
        }
    }
}