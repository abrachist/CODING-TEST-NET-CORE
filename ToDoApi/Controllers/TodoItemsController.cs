using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Models;

namespace ToDoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoItemsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }

        // GET: api/TodoItems/Today
        [HttpGet("Today")]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetToday()
        {
            
            return await _context.TodoItems.Where(i => i
            .ExpiredDate.ToShortDateString().Equals(DateTime.Now.ToShortDateString()))
            .Where(j => j.Status.Contains("undone"))
            .ToListAsync();
        }

        // GET: api/TodoItems/Tomorow
        [HttpGet("Tomorow")]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTomorow()
        {

            return await _context.TodoItems.Where(i => i
            .ExpiredDate.ToShortDateString().Equals(DateTime.Now.AddDays(1).ToShortDateString()))
            .Where(j => j.Status.Contains("undone"))
            .ToListAsync();
        }

        // GET: api/TodoItems/ThisWeek
        [HttpGet("ThisWeek")]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetThisWeek()
        {

            return await _context.TodoItems.Where(i => i
            .ExpiredDate >= DateTime.Now)
            .Where(i => i.ExpiredDate <= DateTime.Now.AddDays(7))
            .Where(j => j.Status.Contains("undone"))
            .ToListAsync();
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<TodoItem>> PutTodoItem(long id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(todoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return todoItem;
        }

        
        // POST: api/TodoItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            if (todoItem.CompletePercentage >= 100)
            {
                todoItem.Status = "done";
            }
            else
            {
                todoItem.Status = "undone";
            }

            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();
                        
            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
        }

        // POST: api/TodoItems/5/Done
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("{id}/Done")]
        public async Task<ActionResult<TodoItem>> PostDone(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            todoItem.Status = "done";

            _context.Entry(todoItem).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return todoItem;
        }
        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoItem>> DeleteTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return todoItem;
        }

        private bool TodoItemExists(long id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }
    }
}
