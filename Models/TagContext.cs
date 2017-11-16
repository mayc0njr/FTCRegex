using Microsoft.EntityFrameworkCore;

namespace MayconJr.StringParser.Models
{
    public class TagContext : DbContext
    {
        public string ConnectionString{ get; set; }
        public TagContext(DbContextOptions<TagContext> options) : base(options)
        {
        }

        public DbSet<Tag> Tags { get; set; }

    }
}