using Microsoft.AspNetCore.Mvc;
using MVCWebSite.Core.Entities;
using MVCWebSite.WebAPIUsing.Models;
using System.Diagnostics;

namespace MVCWebSite.WebAPIUsing.Controllers
{
    public class HomeController : Controller
    {
        string _apiAdres = "https://localhost:7002/api/";
        private readonly HttpClient _httpClient;

        public HomeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var products = await _httpClient.GetFromJsonAsync<List<Product>>(_apiAdres + "products");
            var sliders = await _httpClient.GetFromJsonAsync<List<Slider>>(_apiAdres + "Sliders");
            var model = new HomePageViewModel
            {
                Sliders = sliders,
                Products = products.Where(p => p.IsActive && p.IsHome).ToList(),
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
