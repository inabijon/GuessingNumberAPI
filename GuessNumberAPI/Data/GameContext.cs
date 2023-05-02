using GuessNumberAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace GuessNumberAPI.Data
{
    public class GameContext: DbContext
    {
        public GameContext(DbContextOptions<GameContext> options) : base(options) { }

        public DbSet<Game> Games { get; set; }
        public DbSet<GameResult> GameResults { get; set; }
        public DbSet<Guess> Guesses { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
