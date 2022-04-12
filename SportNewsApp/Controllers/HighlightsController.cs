
namespace SportNewsApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.IO;
    using System.Linq;

    public class HighlightsController : Controller
    {
        public IActionResult Index()
        {
            var videos = Directory.GetFiles("wwwroot/videos");
            videos = videos.Select(d => d.Replace("\\", "/")).Select(d => d.Replace("wwwroot", string.Empty)).ToArray();
            return View(videos);
        }
    }
}
