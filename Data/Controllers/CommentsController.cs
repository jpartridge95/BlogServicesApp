using Data.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Data.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ILogger<PostsController>? _logger;
        private readonly CommentAccess _access;

        public CommentsController(ILogger<PostsController>? logger, CommentAccess access)
        {
            _logger = logger;
            _access = access;
        }

        [HttpGet("/comments/bypost")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Comment>>> GetCommentsByPost(int postId)
        {
            List<Comment> comments = _access.GetByPost(postId);

            if (comments == null)
            {
                return NotFound();
            }

            return comments;
        }

        [HttpGet("/comments/byuser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Comment>>> GetCommentsByUser(int userId)
        {
            List<Comment> comments = _access.GetByUser(userId);

            if (comments == null)
            {
                return NotFound();
            }
            
            return comments;
        }

        [HttpDelete("/comments/byid")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> DeleteSingleComment(int id)
        {
            var success = await _access.DeleteComment(id);

            if (success)
            {
                return NoContent();
            }

            return NotFound();
 
        }

        [HttpDelete("/comments/byuser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> DeleteByUser(int userId)
        {
            if (await _access.DeleteByUser(userId))
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("/comments/bypost")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> DeleteByPost(int postId)
        {
            if (await _access.DeleteByPost(postId))
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpPatch("/comments/inclikes")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> IncrementLikes(int commentId)
        {
            if (await _access.IncrementLikes(commentId))
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpPatch("/comments/declikes")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> DecrementLikes(int commentId)
        {
            if (await _access.DecrementLikes(commentId))
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpPatch("/comments/incdislikes")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> IncrementDislikes(int commentId)
        {
            if (await _access.IncrementDislikes(commentId))
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpPatch("/comments/decdislikes")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> DecrementDislikes(int commentId)
        {
            if (await _access.DecrementDislikes(commentId))
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpPut("/comments/editComment")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> EditComment(int commentId, Comment comment)
        {
            if (await _access.EditCommentContent(commentId, comment))
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpPost("/comments/createcomment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> CreateComment(Comment comment)
        {
            if (await _access.CreateComment(comment))
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
