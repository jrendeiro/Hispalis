using System;
using System.Collections.Generic;
using System.Linq;
using API.Data;
using API.Models;

namespace API.Helpers
{
    public static class TweetQueryBuilder
    {
        public static IQueryable<Tweet> triageRequestTypes(RequestTypes requestType, DataContext context, RequestHeaderObject currentHeaders, string srchItem)
        {
            IQueryable<Tweet> tweets = context.Tweets.Where(x => x.Text.Length > 0
                                    && x.UserName.Length > 0);
                                    // .OrderByDescending(x => x.Time);

            switch (requestType)
            {
                case RequestTypes.Initial:

                    return tweets;

                case RequestTypes.Search:

                    if (srchItem != null)
                    {
                        return tweets = tweets.Where(x => x.Text.Contains(srchItem));
                    }

                    return tweets;

                case RequestTypes.Size:

                    if (srchItem != null)
                    {
                        return tweets = tweets.Where(x => x.Text.Contains(srchItem));
                    }

                    return tweets;
                  

                case RequestTypes.Prev:

                    // tweets = tweets.Where(x => x.Time >= Convert.ToDateTime(currentHeaders.date));

                    if (srchItem != null)
                    {
                        return tweets = tweets.Where(x => x.Text.Contains(srchItem));
                    }

                    return tweets;

                case RequestTypes.Next:

                    // tweets = tweets.Where(x => x.Time <= Convert.ToDateTime(currentHeaders.date));

                    if (srchItem != null)
                    {
                        return tweets = tweets.Where(x => x.Text.Contains(srchItem));
                    }

                    return tweets;

                default:

                    return tweets;
            }
        }
    }
}