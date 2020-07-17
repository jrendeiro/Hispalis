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
        public IActionResult GetTweets([FromQuery] string srchItem)
        {


            // two ways of doing the same thing. just comment one out
            // -------------------------------------------------------------------------------
            // int tweetCount = Math.Max(25, Convert.ToInt32(Request.Headers["count"]));
            int tweetCount = Convert.ToInt32(Request.Headers["count"]);
            // int tweetCount = Request.TestExtension("count");
            // -------------------------------------------------------------------------------

            try
            {
                List<Tweet> tweets = _context.Tweets
                    .Where(x => x.Text.Length > 0
                         && x.UserName.Length > 0
                         && x.Text.Contains(srchItem))
                    .OrderByDescending(x => x.Time)
                    .ToList()
                    ;
                    
                Response.AddTweetCount(tweets.Count.ToString());

                var tweetsToReturn = tweets.Take(tweetCount);


                // var tweetz = _mapper.Map<IEnumerable<TweetToReturnDto>>(tweets);



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
