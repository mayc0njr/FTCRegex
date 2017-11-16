using Microsoft.EntityFrameworkCore;

namespace MayconJr.StringParser.Models
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