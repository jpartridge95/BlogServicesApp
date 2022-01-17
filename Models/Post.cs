using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public interface IPostActions
    {
        // Read Actions
        Task<List<Post>> GetPostsFromUser(int userId);
        Task<Post> GetSinglePost(int postId);
        Task<List<Post>> GetAllPosts();

        // Update Actions
        Task<bool> IncrementLikes(int postId);
        Task<bool> IncrementDislikes(int postId);
        Task<bool> DecrementLikes(int postId);
        Task<bool> DecrementDislikes(int postId);
        Task<bool> EditPost(int postId, Post newData);

        // Create Actions
        Task<bool> CreatePost (Post post);

        // Delete Actions
        Task<bool> DeletePost(int postId);
        Task<bool> DeleteByUser(int userId);
    }

    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public DateTime Created { get; set; }
        public int CreatedBy { get; set; }
    }
}
