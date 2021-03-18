using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using VCubWatcher.Models;
using System.Net.Http;

namespace VCubWatcher.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Accueil()
        { 
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Liste()
        {
            var stations = GetBikeStationListFromApi();
            return View(stations);
        }

        public IActionResult Carte()
        {
            return View();
        }

        public IActionResult Favs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private static readonly HttpClient client = new HttpClient();
        private static List<VCubStation> GetBikeStationListFromApi()
        {
            var stringTask = client.GetStringAsync("https://api.alexandredubois.com/vcub-backend/vcub.php");
            var MyJsonRes = stringTask.Result;
            var result = JsonConvert.DeserializeObject<List<VCubStation>>(MyJsonRes);
            return result;
        }
    }
}
