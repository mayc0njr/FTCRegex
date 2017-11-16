using Microsoft.EntityFrameworkCore;

namespace FTCRegex.Models
{
    public class TransitionContext : DbContext
    {
        public string ConnectionString{ get; set; }
        public TransitionContext(DbContextOptions<TransitionContext> options) : base(options)
        {
        }

        public DbSet<Transition> Transitions { get; set; }

    }
}