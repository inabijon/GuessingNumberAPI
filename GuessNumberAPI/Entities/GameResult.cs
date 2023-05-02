using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GuessNumberAPI.Entities
{
    public class GameResult: BaseEntity
    {

        public int GameId { get; set; }
        public Game Game { get; set; }
        public bool IsWon { get; set; }
        public int Attempt { get; set; }
        public int SecretNumber { get; set; }
    }
}
