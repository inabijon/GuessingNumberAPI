namespace GuessNumberAPI.Dtos
{
    public class GameResultToReturnDto
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public int SecretNumber { get; set; }
        public bool IsWon { get; set; }
        public int Attempt { get; set; }
    }
}
