using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MVCWebSite.Core.Entities;
using System.Security.Claims;

namespace MVCWebSite.WebAPIUsing.Controllers
{
    public class AccountController : Controller
    {
        string _apiAdres = "https://localhost:7002/api/";
        private readonly HttpClient _httpClient;

        public AccountController(HttpClient httpClient)
        {
            _httpClient = httpClient;
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
        public async Task<IActionResult> LoginAsync(User user)
        {
            var kullanicilar = await _httpClient.GetFromJsonAsync<List<User>>(_apiAdres + "users/");
            var kullanici = kullanicilar.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password && u.IsActive);
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
