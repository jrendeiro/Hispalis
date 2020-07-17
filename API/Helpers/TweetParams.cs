namespace API.Controllers
{
    public class TweetParams
    {
        // private const int MaxPageSize = 50;

        public int PageNumber { get; set; } = 1;
        private int pageSize = 10;
        // public int PageSize
        // {
        //     get { return pageSize;}
        //     set { pageSize = (value > MaxPageSize) ? MaxPageSize : value ;}
        // }

        public int count { get; set; }
        public string MessageContainer { get; set; } = "Unread";
    }
}