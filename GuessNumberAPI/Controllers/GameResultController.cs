using GuessNumberAPI.Data;
using GuessNumberAPI.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GuessNumberAPI.Controllers
{
    [Route("api/game/game-result/{gameId}")]
    [ApiController]
    public class GameResultController : ControllerBase
    {

        private readonly GameContext _context;
        public GameResultController(GameContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<GameResultToReturnDto>>> GetGameResult([FromRoute] int gameId)
        {
            var gameResult = await _context.GameResults
                .Include(x => x.Game)
                .Include(u => u.Game.User)
                .FirstOrDefaultAsync(x => x.GameId == gameId);

            if (gameResult == null) { 
                return NotFound();
            }

            return Ok(new GameResultToReturnDto
            {
                Id = gameResult.Id,
                UserId = gameResult.Game.UserId,
                GameId = gameResult.GameId,
                Username = gameResult.Game.User.Name,
                SecretNumber = gameResult.SecretNumber,
                Attempt = gameResult.Attempt,
                IsWon = gameResult.IsWon
            });
        }
    }
}
