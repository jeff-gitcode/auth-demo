using AuthDemo.Service;
using Microsoft.AspNetCore.Mvc;

namespace AuthDemo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly TodoContext _context;
        private readonly IPostService _service;

        public PostController(TodoContext context, IPostService service)
        {
            _context = context;
            _service = service;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItem()
        {
            var result = await _service.GetPosts();

            return Ok(result);
            // return await _context.TodoItem.ToListAsync();
        }
    }
}
