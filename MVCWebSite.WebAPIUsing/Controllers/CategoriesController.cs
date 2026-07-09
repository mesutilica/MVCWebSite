using Microsoft.AspNetCore.Mvc;
using MVCWebSite.Core.Entities;

namespace MVCWebSite.WebAPIUsing.Controllers
{
    public class CategoriesController : Controller
    {
        string _apiAdres = "https://localhost:7002/api/";
        private readonly HttpClient _httpClient;

        public CategoriesController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IActionResult> IndexAsync(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }
            var model = await _httpClient.GetFromJsonAsync<Category>(_apiAdres + "categories/" + id);
            if (model is null)
            {
                return NotFound();
            }
            var products = await _httpClient.GetFromJsonAsync<List<Product>>(_apiAdres + "products");
            model.Products = products.Where(p => p.IsActive && p.CategoryId == id).ToList();
            return View(model);
        }
    }
}
