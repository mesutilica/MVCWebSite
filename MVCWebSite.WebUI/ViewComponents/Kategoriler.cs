using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCWebSite.Data;

namespace MVCWebSite.WebUI.ViewComponents
{
    public class Kategoriler : ViewComponent
    {
        private readonly DatabaseContext _databaseContext;

        public Kategoriler(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IViewComponentResult Invoke()
        {
            return View(_databaseContext.Categories.Where(c => c.IsActive));
        }
    }
}
