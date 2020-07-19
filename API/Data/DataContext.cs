using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Tweet> Tweets { get; set; }

        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder
        .UseLoggerFactory(MyLoggerFactory) // Warning: Do not create a new ILoggerFactory instance each time
        .UseSqlServer("Server=tcp:da-sql-1882.database.windows.net,1433;Initial Catalog=datingAppDb;Persist Security Info=False;User ID=appuser;Password=QweAsd11!!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

            // @"Server=(localdb)\mssqllocaldb;Database=EFLogging;Trusted_Connection=True;ConnectRetryCount=0");

        // protected override void OnModelCreating(ModelBuilder builder)
        // {

        // builder.Entity<Tweet>()  
        // .HasOne(u => u.Likee)
        // .WithMany(u => u.Likers)
        // .HasForeignKey(u => u.LikeeId)
        // .OnDelete(DeleteBehavior.Restrict);

        // }

    }
}