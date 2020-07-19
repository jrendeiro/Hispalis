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
            var tweets =  TweetQueryBuilder.buildQuery(requestType, _context, currentHeaders, srchItem);
            // IQueryable<Tweet> tweets;

            try
            {
            //     if (srchItem == null) {
            //         tweets = _context.Tweets
            //             .Where(x => x.Text.Length > 0
            //                 && x.UserName.Length > 0)
            //             .OrderByDescending(x => x.Time)
            //             .Take(45)
            //             .ToList();
            //     }
            //     else {
            //         tweets = _context.Tweets
            //             .Where(x => x.Text.Contains(srchItem)
            //                 && x.Text.Length > 0
            //                 && x.UserName.Length > 0)
            //             .OrderByDescending(x => x.Time)
            //             .ToList();
            //     }
                    
                List<Tweet> tweetsList = tweets.ToList();

                Response.AddTweetCount(tweetsList.Count.ToString());

                // DateTime date = Convert.ToDateTime(Request.Headers["date"]);

                var tweetsToReturn = tweets.Take(currentHeaders.pageSize);


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
