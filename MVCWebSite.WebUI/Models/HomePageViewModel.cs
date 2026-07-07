using MVCWebSite.Core.Entities;

namespace MVCWebSite.WebUI.Models
{
    public class HomePageViewModel
    {
        public IEnumerable<Slider>? Sliders { get; set; }
        public IEnumerable<Product>? Products { get; set; }
    }
}
