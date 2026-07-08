using Microsoft.AspNetCore.Mvc;
using MVCWebSite.Core.Entities;
using MVCWebSite.Data;


namespace MVCWebSite.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlidersController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public SlidersController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/<SlidersController>
        [HttpGet]
        public IEnumerable<Slider> Get()
        {
            return _context.Sliders;
        }

        // GET api/<SlidersController>/5
        [HttpGet("{id}")]
        public ActionResult<Slider> Get(int id)
        {
            var model = _context.Sliders.Find(id);
            if (model == null)
            {
                return NotFound();
            }
            return model;
        }

        // POST api/<SlidersController>
        [HttpPost]
        public void Post([FromBody] Slider value)
        {
            _context.Sliders.Add(value);
            _context.SaveChanges();
        }

        // PUT api/<SlidersController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Slider value)
        {
            _context.Sliders.Update(value);
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE api/<SlidersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var slider = _context.Sliders.Find(id);
            if (slider is null)
            {
                return NotFound();
            }
            _context.Sliders.Remove(slider);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
