using System;

namespace API.Models
{
    public class Tweet
    {
        public int TweetId { get; set; }
        public string Language { get; set; }
        public string Location { get; set; }
        public string Text { get; set; }
        public DateTime? Time { get; set; }
        public string UserName { get; set; }
    }
}