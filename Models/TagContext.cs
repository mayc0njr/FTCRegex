using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FTCRegex.Models
{
    public class TagContext : DbContext
    {
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Group> Groups { get; set; }
        public string ConnectionString{ get; set; }
        public TagContext(DbContextOptions<TagContext> options) : base(options)
        {
        }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     base.OnModelCreating(modelBuilder);
        //     System.Console.WriteLine("onModelCreating...");
        //     if(!Groups.Any())
        //     {
        //         Groups.Add(new Group(){ Name = "Default" });
        //     }
        //     // modelBuilder.Entity<Group>()
        //     //     .HasOne(p => p.Tags)
        //     //     .WithMany(b => b.Groups);
        // }


    }
}