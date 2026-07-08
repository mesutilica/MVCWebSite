using Microsoft.AspNetCore.Mvc;
using MVCWebSite.Core.Entities;
using MVCWebSite.Data;


namespace MVCWebSite.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public UsersController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _context.Users;
        }

        // GET api/Users/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            var model = _context.Users.Find(id);
            if (model == null)
            {
                return NotFound();
            }
            return model;
        }

        // POST api/Users
        [HttpPost]
        public ActionResult<User> Post([FromBody] User value)
        {
            _context.Users.Add(value);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = value.Id }, value);
        }

        // PUT api/Users/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] User value)
        {
            if (id != value.Id)
            {
                return BadRequest();
            }

            _context.Users.Update(value);
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE api/Users/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user is null)
            {
                return NotFound();
            }
            _context.Users.Remove(user);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
