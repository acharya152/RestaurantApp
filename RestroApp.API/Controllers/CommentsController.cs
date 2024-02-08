using ClassLibraryForRestro.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RestroApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IComments _comments;
        public CommentsController(IComments comments)
        {
                _comments = comments;
        }

        [HttpGet("Get")]

        public async Task<IActionResult> Get(int id)
        {
           var comments= _comments.GetAll(id);
            return Ok(comments);
        }
        
    }
}
