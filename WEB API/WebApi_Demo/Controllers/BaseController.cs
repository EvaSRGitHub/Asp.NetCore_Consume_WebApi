using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApiData;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected BaseController(WebApiDbContext db)
        {
            this.Db = db;

            if (Db.TodoItems.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                Db.TodoItems.Add(new TodoItem { Name = "Item1" });
                Db.SaveChanges();
            }
        }

        protected WebApiDbContext Db { get; set; }
    }
}
