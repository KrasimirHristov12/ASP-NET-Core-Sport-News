using System.Collections.Generic;

namespace SportNewsApp.Models.Fixtures
{
    public class FixturesViewModel
    {
        public IEnumerable<ResponseResult> Response { get; set; }
        public string LeagueName { get; set; }
    }
}
