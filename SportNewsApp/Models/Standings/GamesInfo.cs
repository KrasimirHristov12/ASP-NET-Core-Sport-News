namespace SportNewsApp.Models.Standings
{
    public class GamesInfo
    {
        public int Played { get; set; }
        public int Win { get; set; }
        public int Draw { get; set; }
        public int Lose { get; set; }
        public GoalsInfo Goals { get; set; }
    }
}
