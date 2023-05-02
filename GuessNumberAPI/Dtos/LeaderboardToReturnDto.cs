namespace GuessNumberAPI.Dtos
{
    public class LeaderboardToReturnDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public int Won { get; set; }
        public int Lost { get; set; }
        public int Total { get; set; }
        public int Winrate { get; set; }
    }
}
