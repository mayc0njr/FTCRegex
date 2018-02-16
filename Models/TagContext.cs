using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FTCRegex.Models
{
    public class TagContext : DbContext
    {
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<TagHistory> TagHistories { get; set; }
        public DbSet<TagAction> TagActions { get; set; }


        public DbSet<User> Users { get; set; }
        public DbSet<UserFollow> UserFollowers { get; set; }
        public DbSet<UserReminder> UserReminders { get; set; }

        public DbSet<PasswordReminder> PasswordReminders { get; set; }


        public DbSet<Comment> Comments { get; set; }
        
        public string ConnectionString{ get; set; }
        public TagContext(DbContextOptions<TagContext> options) : base(options)
        {
        }



    }
}