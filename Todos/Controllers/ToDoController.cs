using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todos.Data;
using Todos.Models;

namespace Todos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ToDoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ToDo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoModel>>> GetToDoModel()
        {
            return await _context.ToDoModel.ToListAsync();
        }

        // GET: api/ToDo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoModel>> GetToDoModel(int id)
        {
            var toDoModel = await _context.ToDoModel.FindAsync(id);

            if (toDoModel == null)
            {
                return NotFound();
            }

            return toDoModel;
        }

        // PUT: api/ToDo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDoModel(int id, ToDoModel toDoModel)
        {
            if (id != toDoModel.ItemId)
            {
                return BadRequest();
            }

            _context.Entry(toDoModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDoModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ToDo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ToDoModel>> PostToDoModel(ToDoModel toDoModel)
        {
            _context.ToDoModel.Add(toDoModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetToDoModel", new { id = toDoModel.ItemId }, toDoModel);
        }

        // DELETE: api/ToDo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDoModel(int id)
        {
            var toDoModel = await _context.ToDoModel.FindAsync(id);
            if (toDoModel == null)
            {
                return NotFound();
            }

            _context.ToDoModel.Remove(toDoModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ToDoModelExists(int id)
        {
            return _context.ToDoModel.Any(e => e.ItemId == id);
        }
    }
}
