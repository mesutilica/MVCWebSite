using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCWebSite.WebAPI.Tools;

namespace MVCWebSite.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        [HttpPost] // Slider controller da resim yükleme actionu
        public async Task<IActionResult> Upload([FromForm] IFormFile formFile, string path = "") // Metot ismi Upload, parametre olarak Iformfile ile bir formdan gelecek dosyayı alıyor
        {
            var result = FileHelper.FileLoader(formFile, path);
            if (string.IsNullOrEmpty(result))
            {
                return Problem("Dosya Yüklenemedi!");
            }
            return Created(string.Empty, new { imageName = result }); // Geriye dosyanın eklendiğine dair response döndük
        }
    }
}
