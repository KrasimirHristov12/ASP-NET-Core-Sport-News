using System.Collections.Generic;

namespace SportNewsApp.Models.Squads
{
    public class SquadResponseInfo
    {
        public SquadTeamInfo Team { get; set; }
        public ICollection<SquadPlayersInfo> Players { get; set; }
    }
}
