namespace GuessNumberAPI.Entities
{
    public class Guess: BaseEntity
    {
        public int? GameId { get; set; }
        public Game? Game { get; set; }
        public int? Attempt { get; set; } = 0;
        public int GuessNumber { get; set; }
        public int? M { get; set; }
        public int? P { get; set; }
    }
}
