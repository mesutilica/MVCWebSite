using Microsoft.AspNetCore.Mvc;
using MVCWebSite.Data;

namespace MVCWebSite.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly DatabaseContext _context;

        public ProductsController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index(string q = "")
        {
            return View(_context.Products.Where(p => p.IsActive && p.Name.Contains(q)));
        }

        public IActionResult Details(int? id)
        {
            if (id is null)
            {
                return BadRequest("Geçersiz İstek!");
            }
            var model = _context.Products.Find(id);
            if (model is null)
            {
                return NotFound("Ürün Bulunamadı!");
            }
            return View(model);
        }
    }
}
