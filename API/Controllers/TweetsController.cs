using System;
using System.Collections.Generic;
using System.Linq;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using API.Helpers;
using AutoMapper;

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


            // two ways of doing the same thing. just comment one out
            // -------------------------------------------------------------------------------
            int tweetCount = Convert.ToInt32(Request.Headers["count"]);
            // int tweetCount = Request.TestExtension("count");
            // -------------------------------------------------------------------------------

            List<Tweet> tweets;

            try
            {
                if (srchItem != null) {
                    tweets = _context.Tweets
                        .Where(x => x.Text.Contains(srchItem)
                            && x.Text.Length > 0
                            && x.UserName.Length > 0)
                        .OrderByDescending(x => x.Time)
                        .ToList();
                }
                else {
                    tweets = _context.Tweets
                        .Where(x => x.Text.Length > 0
                            && x.UserName.Length > 0)
                        .OrderByDescending(x => x.Time)
                        .ToList();
                }
                    
                Response.AddTweetCount(tweets.Count.ToString());

                var tweetIndex = 3456;

                // var tweetsToReturn = tweets.Take(tweetCount);
                var tweetsToReturn = tweets.TakeWhile(x => x.TweetId > tweetIndex);



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
