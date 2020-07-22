using System;
using System.Linq;
using API.Data;
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

        // public static void AddTweetCount(this HttpResponse response, DataContext context, string srchItem)
        public static void AddTweetCount(this HttpResponse response, int tweetCount, string srchItem)
        {

        //    var tweetCount = context.Tweets.Where(x => x.Text.Length > 0
        //                             && x.UserName.Length > 0);

            // if (srchItem != null)
            // {
            //     tweetCount = tweetCount.Where(x => x.Text.Contains(srchItem));
            // }


            response.Headers.Add("Access-Control-Expose-Headers", "ResultCount");           
            response.Headers.Add("ResultCount", tweetCount.ToString());
        }
    }
}