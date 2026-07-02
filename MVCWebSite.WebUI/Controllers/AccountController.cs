using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using MVCWebSite.Core.Entities;
using MVCWebSite.Data;
using System.Security.Claims;

namespace MVCWebSite.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly DatabaseContext _databaseContext;

        public AccountController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(User user)
        {
            var kullanici = _databaseContext.Users.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password && u.IsActive);
            if (kullanici != null)
            {
                var haklar = new List<Claim>() // kullanıcı hakları tanımladık
                    {
                        new(ClaimTypes.Email, kullanici.Email), // claim = hak(kullanıcıya tanımlalan haklar)
                        new(ClaimTypes.Role, kullanici.IsAdmin ? "Admin" : "User") // giriş yapan kullanıcı admin ise admin yetkisiyle değilse user yetkisiyle giriş yasın.
                    };
                var kullaniciKimligi = new ClaimsIdentity(haklar, "Login"); // kullanıcı için bir kimlik oluşturduk
                ClaimsPrincipal claimsPrincipal = new(kullaniciKimligi);
                HttpContext.SignInAsync(claimsPrincipal); // yukardaki yetkilerle sisteme giriş yaptık
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Giriş Başarısız!");
            return View(user);
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(); // oturumu kapat
            return RedirectToAction("Index", "Home");
        }
    }
}
