namespace Models
{
    public interface ICommentActions
    {
        // Read actions
        List<Comment> GetByPost(int postId);
        List<Comment> GetByUser(int userId);

        // Update actions
        Task<bool> IncrementLikes(int commentId);
        Task<bool> IncrementDislikes(int commentId);
        Task<bool> DecrementLikes(int commentId);
        Task<bool> DecrementDislikes(int commentId);
        Task<bool> EditCommentContent (int commentId, Comment newData);

        // Create actions
        Task<bool> CreateComment (Comment comment);

        // Delete Actions
        Task<bool> DeleteComment (int commentId);
        Task<bool> DeleteByPost (int postId);
        Task<bool> DeleteByUser (int userId);
    }

    public class Comment
    {
        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public int ForPost { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
    }
}
