using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GuessNumberAPI.Entities
{
    public class Game: BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int SecretNumber { get; set; }
    }
}
