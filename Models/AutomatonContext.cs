using Microsoft.EntityFrameworkCore;

namespace FTCRegex.Models
{
    public class AutomatonContext : DbContext
    {
        public string ConnectionString{ get; set; }
        public AutomatonContext(DbContextOptions<AutomatonContext> options) : base(options)
        {
        }

        public DbSet<Automaton> Automata { get; set; }

    }
}