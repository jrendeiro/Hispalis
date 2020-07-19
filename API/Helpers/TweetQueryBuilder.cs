using System;
using System.Collections.Generic;
using System.Linq;
using API.Data;
using API.Models;

namespace API.Helpers
{
    public static class TweetQueryBuilder
    {
        public static IQueryable<Tweet> buildQuery(RequestTypes requestType, DataContext context, RequestHeaderObject currentHeaders, string srchItem)
        {
            var tweets = context.Tweets.AsQueryable();

            switch (requestType)
            {
                case RequestTypes.Initial:

                    return tweets = tweets.Where(x => x.Text.Length > 0
                           && x.UserName.Length > 0)
                           .OrderByDescending(x => x.Time);                           
                           
                case RequestTypes.Search:

                    tweets = context.Tweets.Where(x => x.Text.Length > 0
                        && x.UserName.Length > 0)
                        .OrderByDescending(x => x.Time);                        

                    if (srchItem != null)
                    {
                        tweets = tweets.Where(x => x.Text.Contains(srchItem));
                    }

                    return tweets;
                           
                case RequestTypes.Size:

                    tweets = context.Tweets.Where(x => x.Text.Length > 0
                        && x.UserName.Length > 0
                        && x.TweetId <= Convert.ToInt32(currentHeaders.tweetId))
                        .OrderByDescending(x => x.Time);                        

                    if (srchItem != null)
                    {
                        tweets = tweets.Where(x => x.Text.Contains(srchItem));
                    }

                    return tweets;

                case RequestTypes.Prev:
                    
                    tweets = context.Tweets.Where(x => x.Text.Length > 0
                        && x.UserName.Length > 0
                        && x.TweetId > Convert.ToInt32(currentHeaders.tweetId))
                        .OrderByDescending(x => x.Time);                        

                    if (srchItem != null)
                    {
                        tweets = tweets.Where(x => x.Text.Contains(srchItem));
                    }

                    return tweets;

                case RequestTypes.Next:
                    
                    tweets = context.Tweets.Where(x => x.Text.Length > 0
                        && x.UserName.Length > 0
                        && x.TweetId < Convert.ToInt32(currentHeaders.tweetId))
                        .OrderByDescending(x => x.Time);                        

                    if (srchItem != null)
                    {
                        tweets = tweets.Where(x => x.Text.Contains(srchItem));
                    }

                    return tweets;

                default:
                    return tweets = context.Tweets.Where(x => x.Text.Length > 0
                           && x.UserName.Length > 0);
            }
    }
}}