using Microsoft.AspNetCore.Mvc;
using MVCWebSite.Core.Entities;

namespace MVCWebSite.WebAPIUsing.Controllers
{
    public class ProductsController : Controller
    {
        string _apiAdres = "https://localhost:7002/api/";
        private readonly HttpClient _httpClient;

        public ProductsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> IndexAsync(string q = "")
        {
            var products = await _httpClient.GetFromJsonAsync<List<Product>>(_apiAdres + "products");
            return View(products.Where(p => p.IsActive && p.Name.Contains(q)));
        }

        public async Task<IActionResult> DetailsAsync(int? id)
        {
            if (id is null)
            {
                return BadRequest("Geçersiz İstek!");
            }
            var model = await _httpClient.GetFromJsonAsync<Product>(_apiAdres + "products/" + id);
            if (model is null)
            {
                return NotFound("Ürün Bulunamadı!");
            }
            return View(model);
        }
    }
}
