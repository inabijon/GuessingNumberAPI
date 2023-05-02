using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GuessNumberAPI.Data;
using GuessNumberAPI.Entities;
using GuessNumberAPI.Helpers;
using GuessNumberAPI.Dtos;

namespace GuessNumberAPI.Controllers
{
    [Route("api/game/{userId}")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly GameContext _context;
        public GamesController(GameContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<GameReturnDto>>> Games()
        {
            var games = await _context.Games
                .Include(x => x.User)
                .ToListAsync();

            return games.Select(game => new GameReturnDto
            {
                Id = game.Id,
                UserId = game.UserId,
                Username = game.User.Name
            }).ToList();
        }

        [HttpPost]
        public async Task<ActionResult<Game>> CreateGame( [FromRoute] int userId)
        {
            try
            {
                var game = new Game();
                var secret = SecretNumberGenerator.SecretNumberForGameGenerator();

                game.SecretNumber = secret;
                game.UserId = userId;

                _context.Games.Add(game);
                _context.SaveChanges();

                return Ok(new GameReturnDto
                {
                    Id = game.Id,
                    UserId = game.UserId
                } );

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
