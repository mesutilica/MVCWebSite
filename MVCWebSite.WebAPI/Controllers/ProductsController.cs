using Microsoft.AspNetCore.Mvc;
using MVCWebSite.Core.Entities;
using MVCWebSite.Data;


namespace MVCWebSite.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ProductsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _context.Products;
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            var model = _context.Products.Find(id);
            if (model == null)
            {
                return NotFound();
            }
            return model;
        }

        // POST api/<ProductsController>
        [HttpPost]
        public void Post([FromBody] Product value)
        {
            _context.Products.Add(value);
            _context.SaveChanges();
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product value)
        {
            _context.Products.Update(value);
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product is null)
            {
                return NotFound();
            }
            _context.Products.Remove(product);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
