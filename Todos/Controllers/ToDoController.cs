using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todos.Data;
using Todos.Models;
using ToDos.Utility;

namespace Todos.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ToDoController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/ToDo
        [HttpGet]
        [Authorize(Roles = Strings.User)]
        public async Task<ActionResult<IEnumerable<ToDoModel>>> GetToDoModel()
        {
            var username = User.Claims.Where(u => u.Type == "name").FirstOrDefault();
            ApplicationUser user = await _userManager.FindByNameAsync(username.Value);
            return await _context.ToDoModel.Where(u => u.UserId == user.Id).ToListAsync();
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
        public async Task<IActionResult> PutToDoModel(int id, ToDoViewModel toDoViewModel)
        {
            if (id != toDoViewModel.ItemId)
            {
                return BadRequest();
            }

            try
            {
                // Replace the username to userid
                var username = User.Claims.Where(u => u.Type == "name").FirstOrDefault();
                ApplicationUser user = await _userManager.FindByNameAsync(username.Value);
                ToDoModel toDoItem = await _context.ToDoModel.Where(u => u.ItemId == id).FirstOrDefaultAsync();
                toDoItem.ItemDescription = toDoViewModel.ItemDescription;
                toDoItem.ItemName = toDoViewModel.ItemName;
                toDoItem.ItemStatus = toDoViewModel.ItemStatus;
            }
            catch (Exception)
            {
                return BadRequest();
            }

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
            return Ok();
        }

        // POST: api/ToDo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostToDoModel(ToDoViewModel toDoViewModel)
        {
            try
            {
                var username = User.Claims.Where(u => u.Type == "name").FirstOrDefault();
                ApplicationUser user = await _userManager.FindByNameAsync(username.Value);
                ToDoModel toDoItem = new ToDoModel();
                toDoItem.ItemDescription = toDoViewModel.ItemDescription;
                toDoItem.ItemName = toDoViewModel.ItemName;
                toDoItem.ItemStatus = toDoViewModel.ItemStatus;
                toDoItem.UserId = user.Id;
                _context.ToDoModel.Add(toDoItem);
                await _context.SaveChangesAsync();
                return Created("GetToDoModel", "ToDoCreated successfully");
            }
            catch
            {
                return BadRequest();
            }
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
