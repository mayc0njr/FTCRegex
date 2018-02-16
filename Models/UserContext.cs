using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FTCRegex.Models
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserFollow> UserFollowers { get; set; }
        public DbSet<UserReminder> UserReminders { get; set; }

        public DbSet<PasswordReminder> PasswordReminders { get; set; }
        
        public string ConnectionString{ get; set; }
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
            System.Console.WriteLine("Initializing Users");
            System.Console.WriteLine("Initializing UserFollowers");
            System.Console.WriteLine("Initializing UserReminders");
            System.Console.WriteLine("Initializing PasswordReminders");
        }


    }
}