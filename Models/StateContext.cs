using Microsoft.EntityFrameworkCore;

namespace FTCRegex.Models
{
    public class StateContext : DbContext
    {
        public string ConnectionString{ get; set; }
        public StateContext(DbContextOptions<StateContext> options) : base(options)
        {
        }

        public DbSet<State> States { get; set; }

    }
}