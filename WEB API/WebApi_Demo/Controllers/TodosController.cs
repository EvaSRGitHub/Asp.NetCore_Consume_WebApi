using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiData;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi_Demo.Controllers
{
    public class TodosController : BaseController
    {
        public TodosController(WebApiDbContext db) : base(db)
        {
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<TodoItem> Get()
        {
            var items = this.Db.TodoItems.ToList();

            return items;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> Get(int id)
        {
            var item = await this.Db.TodoItems.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem([FromBody] TodoItem item)
        {
            await this.Db.TodoItems.AddAsync(item);
            await this.Db.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = item.Id}, item);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody]TodoItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            this.Db.Entry(item).State = EntityState.Detached;

            this.Db.TodoItems.Update(item);

            await this.Db.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoItem>> Delete(int id)
        {
            var item = await this.Db.TodoItems.SingleOrDefaultAsync(i => i.Id == id);

            if(item == null)
            {
                return NotFound();
            }

            this.Db.TodoItems.Remove(item);

            await this.Db.SaveChangesAsync();

            return item;
        }
    }
}
