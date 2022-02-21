using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Posts.Service;

namespace Posts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private PostsAccessService _posts;

        public PostsController(PostsAccessService posts)
        {
            _posts = posts;
        }

        [HttpDelete("byuser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteByUser(int userId)
        {
            if (await _posts.DeleteByUser(userId)) return NoContent();

            return NotFound();
        }

        [HttpDelete("bypost")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteByPost(int postId)
        {
            if (await _posts.DeletePost(postId)) return NoContent();

            return NotFound();
        }

        [HttpPost("createpost")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> CreatePost(Post post)
        {
            if (await _posts.CreatePost(post)) return NoContent();

            return NotFound();
        }

        [HttpPatch("decdislikes")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DecrementDislikes(int postId)
        {
            if (await _posts.DecrementDislikes(postId)) return NoContent();

            return NotFound();
        }

        [HttpPatch("declikes")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DecrementLikes(int postId)
        {
            if (await _posts.DecrementLikes(postId)) return NoContent();

            return NotFound();
        }

        [HttpPatch("editpost")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> EditPost(int postId, Post post)
        {
            if (await _posts.EditPost(postId, post)) return NoContent();

            return NotFound();
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Post>>> GetAllPosts()
        {
            var result = await _posts.GetAllPosts();

            if (result != null)
            {
                return result;
            }

            return NotFound();
        }

        [HttpGet("byUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Post>>> GetPostsFromUser(int userId)
        {
            var result = await _posts.GetPostsFromUser(userId);

            if (result != null)
            {
                return result;
            }

            return NotFound();
        }

        [HttpGet("byid")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Post>> GetSinglePost(int postId)
        {
            var result = await _posts.GetSinglePost(postId);

            if (result != null)
            {
                return result;
            }

            return NotFound();
        }

        [HttpPatch("incdislikes")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> IncrementDislikes(int postId)
        {
            if (await _posts.IncrementDislikes(postId)) return NoContent();

            return NotFound();
        }

        [HttpPatch("inclikes")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> IncrementLikes(int postId)
        {
            if (await _posts.IncrementLikes(postId)) return NoContent();

            return NotFound();
        }
    }
}
