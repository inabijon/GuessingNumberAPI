namespace GuessNumberAPI.Dtos
{
    public class GuessToReturnDto
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public int GuessNumber { get; set; }
        public int M { get; set; }
        public int P { get; set; }
        public int Attempt { get; set; }
    }
}
