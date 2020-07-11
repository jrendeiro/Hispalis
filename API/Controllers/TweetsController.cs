using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
        public IActionResult GetTweet()
        {
            try
            {
                List<Tweet> tweets = _context.Tweets
                    .Where(x => x.Text.Length > 0
                         && x.UserName.Length > 0)
                    .OrderByDescending(x => x.TweetId)
                    .ToList()
                    ;
                // return Ok(_context.Tweets.Take(50));
                return Ok(tweets);
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
