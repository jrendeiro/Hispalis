using System;
using System.Collections.Generic;
using System.Linq;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using API.Helpers;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TweetsController : ControllerBase
    {
        private readonly DataContext _context;
        public TweetsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetTweets([FromQuery] string srchItem = null)
        {

            RequestHeaderObject currentHeaders = HeaderHandler.populateHeaderObject(Request);
            var requestType = HeaderHandler.determineRequestType(currentHeaders);

            // two ways of doing the same thing. just comment one out
            // -------------------------------------------------------------------------------
            int tweetCount = Convert.ToInt32(Request.Headers["count"]);
            // int tweetCount = Request.TestExtension("count");
            // -------------------------------------------------------------------------------

            // List<Tweet> tweets;
            var tweets =  TweetQueryBuilder.triageRequestTypes(requestType, _context, currentHeaders, srchItem);
            // IQueryable<Tweet> tweets;

            try
            {
                // List<Tweet> tweetsList = tweets.ToList().OrderByDescending(x => x.Time);
                List<Tweet> tweetsList = tweets.ToList();
                Response.AddTweetCount(tweetsList.Count, srchItem);
                tweetsList = tweetsList.OrderByDescending(x => x.Time).ToList();

                // Response.AddTweetCount(_context, srchItem);

                if (requestType == RequestTypes.Size)
                {
                    tweetsList = tweetsList.Skip(tweetsList.FindIndex(x => x.TweetId.ToString() == currentHeaders.tweetId)).ToList();
                }

                if (requestType == RequestTypes.Prev)
                {
                    tweetsList = tweetsList.Skip(tweetsList.FindIndex(x => x.TweetId.ToString() == currentHeaders.tweetId) - currentHeaders.pageSize).ToList();
                }

                if (requestType == RequestTypes.Next)
                {
                    tweetsList = tweetsList.Skip(tweetsList.FindIndex(x => x.TweetId.ToString() == currentHeaders.tweetId) + 1).ToList();
                }

                // var tweetsToReturn = tweets.Take(currentHeaders.pageSize);
                var tweetsToReturn = tweetsList.Take(currentHeaders.pageSize);


                return Ok(tweetsToReturn);
            }
            catch (Exception Err)
            {
                return BadRequest(Err.Message);
            }
        }

        // [HttpGet("{id}", Name = "GetTweet")]
        [HttpGet("{id}")]
        public IActionResult GetTweet(int id)
        {
            try
            {
                return Ok(_context.Tweets.FirstOrDefault(t => t.TweetId == id));
            }
            catch (Exception Err)
            {
                return BadRequest(Err.Message);
            }
        }
    }
}
