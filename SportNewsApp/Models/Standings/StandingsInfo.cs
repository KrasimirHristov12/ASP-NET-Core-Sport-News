namespace SportNewsApp.Models.Standings
{
    public class StandingsInfo
    {
        public int Rank { get; set; }
        public StandingTeamInfo Team { get; set; }
        public int Points { get; set; }
        public int GoalsDiff { get; set; }
        public string Form { get; set; }
        public GamesInfo All { get; set; }
    }
}
