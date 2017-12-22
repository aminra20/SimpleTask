using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleTask.Data;

namespace SimpleTask.Controllers
{
    [Produces("application/json")]

    [Route("api/Tasks")]
    public class TasksController : Controller
    {
        private readonly ApiContext _context;

        public TasksController(ApiContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Get()
        {
            var tasks = await _context.Tasks.ToListAsync();

            return Ok(tasks);
        }
        [Route("api/Tasks/{0}")]
        public async Task<IActionResult> Get(string Id)
        {
            var task = await _context.Tasks.FindAsync(Id);
            if (task != null)
                return Ok(task);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]DbModels.TaskModel task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}