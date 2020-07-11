using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) {}   

        public DbSet<Tweet> Tweets { get; set; }

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