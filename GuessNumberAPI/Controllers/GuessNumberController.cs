using GuessNumberAPI.Data;
using GuessNumberAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GuessNumberAPI.Dtos;

namespace GuessNumberAPI.Controllers
{
    [ApiController]
    [Route("api/game/guess-secret/{userId}/{gameId}")]
    public class GuessNumberController : ControllerBase
    {
        private readonly GameContext _context;

        public GuessNumberController(GameContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Guess>> GuessSecret([FromBody] Guess guess, [FromRoute] int userId, [FromRoute] int gameId)
        {
            try
            {
                var USER = _context.Users.FirstOrDefault(x => x.Id == userId);
                var GAME = _context.Games.FirstOrDefault(x => x.Id == gameId);
                var GAME_RESULT = _context.GameResults.FirstOrDefault(x => x.GameId == gameId);
                var GUESS = _context.Guesses.FirstOrDefault(x => x.GameId == gameId);

                var attempt = 1;

                if (USER == null || GAME == null)
                {
                    return NotFound("User or Game not found");
                }

                if (GAME_RESULT != null)
                {
                    return Ok(new GameResultToReturnDto
                    {
                        Id = GAME_RESULT.Id,
                        UserId = GAME_RESULT.Game.UserId,
                        GameId = GAME_RESULT.GameId,
                        Username = GAME_RESULT.Game.User.Name,
                        SecretNumber = GAME_RESULT.SecretNumber,
                        Attempt = GAME_RESULT.Attempt,
                        IsWon = GAME_RESULT.IsWon
                    });
                }

                if (GUESS != null)
                {
                    attempt = (int)(GUESS.Attempt + 1);
                    GUESS.Attempt = attempt;
                    await _context.SaveChangesAsync();
                }

                var secretNumberArray = GAME.SecretNumber.ToString().ToCharArray().Select(c => int.Parse(c.ToString())).ToArray(); ;
                var guessedNumberArray = guess.GuessNumber.ToString().ToCharArray().Select(c => int.Parse(c.ToString())).ToArray(); ;

                var M = 0;
                var P = 0;

                for (var i = 0; i < secretNumberArray.Length; i++)
                {
                    if (secretNumberArray[i] == guessedNumberArray[i])
                    {
                        P++;
                    }
                    else if (secretNumberArray.Contains(guessedNumberArray[i]))
                    {
                        M++;
                    }
                }

                if (P == 4)
                {
                        var createGameResult = new GameResult();
                        createGameResult.GameId = gameId;
                        createGameResult.IsWon = true;
                        createGameResult.Attempt = attempt;
                        createGameResult.SecretNumber = GAME.SecretNumber;

                        _context.GameResults.Add(createGameResult);
                        _context.SaveChanges();
                       
                  
                        var createNewGuess = new Guess();
                        createNewGuess.GameId = gameId;
                        createNewGuess.GuessNumber = guess.GuessNumber;
                        createNewGuess.M = M;
                        createNewGuess.P = P;
                        createNewGuess.Attempt = attempt;

                        _context.Guesses.Add(createNewGuess);
                        _context.SaveChanges();

                        return Ok(new GameResultToReturnDto
                        {
                            Id = createGameResult.Id,
                            UserId = createGameResult.Game.UserId,
                            GameId = createGameResult.GameId,
                            Username = createGameResult.Game.User.Name,
                            SecretNumber = createGameResult.SecretNumber,
                            Attempt = createGameResult.Attempt,
                            IsWon = createGameResult.IsWon
                        });
                }

                if (attempt == 8)
                {
                  
                        var createNewGuess = new Guess();
                        createNewGuess.GameId = gameId;
                        createNewGuess.GuessNumber = guess.GuessNumber;
                        createNewGuess.M = M;
                        createNewGuess.P = P;
                        createNewGuess.Attempt = attempt;

                        _context.Guesses.Add(createNewGuess);
                        _context.SaveChanges();
                  
                        var createGameResult = new GameResult();
                        createGameResult.GameId = gameId;
                        createGameResult.IsWon = false;
                        createGameResult.Attempt = attempt;
                        createGameResult.SecretNumber = GAME.SecretNumber;

                        _context.GameResults.Add(createGameResult);
                        _context.SaveChanges();

                        return Ok(new GameResultToReturnDto
                        {
                            Id = createGameResult.Id,
                            UserId = createGameResult.Game.UserId,
                            GameId = createGameResult.GameId,
                            Username = createGameResult.Game.User.Name,
                            SecretNumber = createGameResult.SecretNumber,
                            Attempt = createGameResult.Attempt,
                            IsWon = createGameResult.IsWon
                        });
                }

                var newGuess = new Guess();
                newGuess.GameId = gameId;
                newGuess.GuessNumber = guess.GuessNumber;
                newGuess.M = M;
                newGuess.P = P;
                newGuess.Attempt = attempt;

                _context.Guesses.Add(newGuess);
                _context.SaveChanges();

                return Ok( new GuessToReturnDto
                {
                    Id = newGuess.Id,
                    GameId = (int)newGuess.GameId,
                    UserId = newGuess.Game.UserId,
                    Username = newGuess.Game.User.Name,
                    GuessNumber = newGuess.GuessNumber,
                    M = (int)newGuess.M,
                    P = (int)newGuess.P,
                    Attempt = (int)newGuess.Attempt
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}