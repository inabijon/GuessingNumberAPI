using GuessNumberAPI.Data;
using GuessNumberAPI.Dtos;
using GuessNumberAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GuessNumberAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaderboardController : ControllerBase
    {
        private readonly GameContext _context;
        public LeaderboardController(GameContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<LeaderboardToReturnDto>>> Leaderboard()
        {
            var gameResults = await _context.GameResults
                .Include(x => x.Game)
                .Include(x => x.Game.User)
                .ToListAsync();

            var users = await _context.Users.ToListAsync();

            var sortedGameResults = gameResults.OrderBy(gr => gr.Attempt);

            var groupedGameResults = sortedGameResults
                .GroupBy(gr => gr.Game.User.Id)
                .ToDictionary(g => g.Key, g => g.ToList());

            var leaderboard = users.Select(user =>
            {
                var userGameResults = groupedGameResults.ContainsKey(user.Id) ? groupedGameResults[user.Id] : new List<GameResult>();
                var won = userGameResults.Count(gr => gr.IsWon);
                var lost = userGameResults.Count(gr => !gr.IsWon);
                var total = won + lost;
                var winRate = total > 0 ? ((decimal)won / total) * 100 : 0;

                return new LeaderboardToReturnDto
                {
                    UserId = user.Id,
                    Username = user.Name,
                    Won = won,
                    Lost = lost,
                    Winrate = (int)winRate,
                    Total = total
                };
            }).ToList();

            var sortedLeaderboard = leaderboard.OrderByDescending(l => l.Winrate);

            return Ok(sortedLeaderboard);
        }
    }
}
