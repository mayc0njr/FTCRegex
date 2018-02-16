using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FTCRegex.Models
{
    public class CommentContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }
        
        public string ConnectionString{ get; set; }
        public CommentContext(DbContextOptions<CommentContext> options) : base(options)
        {
            System.Console.WriteLine("Initializing Comments");
        }


    }
}