using Microsoft.AspNetCore.Http;
using System;

namespace API.Helpers
{
    public static class HeaderHandler
    {
        public static RequestHeaderObject populateHeaderObject(HttpRequest request) {

            RequestHeaderObject headerToReturn = new RequestHeaderObject {
                tweetId = request.Headers["tweetId"],
                date = request.Headers["tweetDate"],
                tweetOperator = request.Headers["operator"],
                pageSize = Convert.ToInt32(request.Headers["count"])
            };

            return headerToReturn;
        }
        
        public static RequestTypes determineRequestType(RequestHeaderObject headers) {
            
            if (headers.tweetId == null && headers.date == null && headers.tweetOperator == null)
            {
                return RequestTypes.Initial;
            }

            switch(headers.tweetOperator)
            {
                case "size":
                    return RequestTypes.Size;
                case "search":
                    return RequestTypes.Search;
                case ">":
                    return RequestTypes.Prev;
                case "<":
                    return RequestTypes.Next;
                default:
                    return RequestTypes.Initial;
            }

        }
    }
}