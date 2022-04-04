namespace SportNewsApp.Models.Fixtures
{
    using System;

    public class FixtureInfo
    {
        public int Id { get; set; }
        public string Referee { get; set; }
        public string Timezone { get; set; }
        public DateTime Date { get; set; }
        public VenueInfo Venue { get; set; }
        public StatusInfo Status { get; set; }
    }
}