using Data.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Data.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly ILogger<PostsController> _logger;
        private readonly PostAccess _access;

        public PostsController(ILogger<PostsController> logger, PostAccess access)
        {
            _logger = logger;
            _access = access;
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Post>>> GetAllPosts()
        {
            List<Post> posts = await _access.GetAllPosts();
            
            if (posts.Count == 0)
            {
                return NotFound();
            }

            return posts;
        }

        [HttpGet("byUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Post>>> GetPostsByUser(int userId)
        {
            List<Post> posts = await _access.GetPostsFromUser(userId);
            
            if (posts.Count == 0)
            {
                return NotFound();
            }

            return posts;
        }

        [HttpGet("byPostId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Post>> GetPostById(int postId)
        {
            try
            {
                Post post = await _access.GetSinglePost(postId);
                return post;
            }
            catch
            {
                return NotFound();
            }
            
        }

        [HttpDelete("byId")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> DeleteByPostId(int postId)
        {
            if (await _access.DeletePost(postId))
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("byUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> DeletePostsFromUser(int userId)
        {
            if (await _access.DeleteByUser(userId))
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpPost("CreatePost")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> CreateNewPost(Post post)
        {
            if (await _access.CreatePost(post))
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpPatch("incLikes")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> IncrementLikes(int postId)
        {
            if (await _access.IncrementLikes(postId))
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpPatch("DecLikes")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> DecrementLikes(int postId)
        {
            if (await _access.DecrementLikes(postId))
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpPatch("incDislikes")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> IncrementDislikes(int postId)
        {
            if (await _access.IncrementDislikes(postId))
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpPatch("DecDislikes")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> DecrementDislikes(int postId)
        {
            if (await _access.DecrementDislikes(postId))
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpPatch("EditPost")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> EditPost(int postId, Post post)
        {
            if (await _access.EditPost(postId, post))
            {
                return NoContent();
            }

            return NotFound();
        }


    }
}
