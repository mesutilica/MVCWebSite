using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCWebSite.Data;

namespace MVCWebSite.WebUI.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly DatabaseContext _context;

        public CategoriesController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }
            var model = _context.Categories.Where(p => p.IsActive && p.Id == id).Include(c => c.Products).FirstOrDefault();
            if (model is null)
            {
                return NotFound();
            }
            return View(model);
        }
    }
}
