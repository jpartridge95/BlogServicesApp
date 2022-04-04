using Comments.Services;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Comments.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentsServiceController : ControllerBase
    {
        private DataService _access;

        public CommentsServiceController(DataService access)
        {
            _access = access;
        }

        [HttpGet("getbyuser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Comment>>> GetByUser(string userId)
        {
            var response = _access.GetByUser(userId);

            if (response == null)
            {
                return NotFound();
            }

            return response;
        }

        [HttpGet("getbypost")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Comment>>> GetByPost(int postId)
        {
            var response = _access.GetByPost(postId);

            if (response == null)
            {
                return NotFound();
            }

            return response;
        }

        [HttpPost("createcomment")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> CreateComment(Comment comment)
        {
            if (await _access.CreateComment(comment))
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpPatch("decdislikes")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DecrementDislikes(int commentId)
        {
            if (await _access.DecrementDislikes(commentId))
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpPatch("declikes")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DecrementLikes(int commentId)
        {
            if (await _access.DecrementLikes(commentId))
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("bypost")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteByPost(int postId)
        {
            if (await _access.DeleteByPost(postId))
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("byuser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteByUser(string userId)
        {
            if (await _access.DeleteByUser(userId))
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("single")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteComment(int commentId)
        {
            if (await _access.DeleteComment(commentId))
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpPatch("edit")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> EditCommentContent(int commentId, Comment comment)
        {
            if (await _access.EditCommentContent(commentId, comment))
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpPatch("incdislikes")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> IncrementDislikes(int CommentId)
        {
            if (await _access.IncrementDislikes(CommentId))
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpPatch("inclikes")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> IncrementLikes(int commentId)
        {
            if (await _access.IncrementLikes(commentId))
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}