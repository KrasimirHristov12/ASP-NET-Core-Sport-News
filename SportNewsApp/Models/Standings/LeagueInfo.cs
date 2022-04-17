using System.Collections.Generic;

namespace SportNewsApp.Models.Standings
{
    public class LeagueInfo
    {
        public string Name { get; set; }
        public string Logo { get; set; }
        public ICollection<ICollection<StandingsInfo>> Standings { get; set; }
    }
}
