using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportNewsApp.Models.Teams
{
    using SportNewsApp.Models.Fixtures;
    public class TeamResponseResult
    {
        public TeamInfo Team { get; set; }
        public VenueInfo Venue { get; set; }
    }
}
