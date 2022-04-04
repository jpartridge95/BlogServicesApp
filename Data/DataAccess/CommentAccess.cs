using Data.Context;
using Models;
using System.Reflection;

namespace Data.DataAccess
{
    public class CommentAccess : ICommentActions
    {

        public SocialContext _context;

        public CommentAccess(SocialContext context)
        {
            _context = context;
        }

        public virtual async Task<bool> CreateComment(Comment comment)
        {
            foreach (PropertyInfo prop in comment.GetType().GetProperties())
            {
                if (prop.GetValue(comment, null) == null)
                {
                    return false;
                }
            }

            _context.Comments.Add(comment);
            _context.SaveChanges();
            return true;
        }

        public virtual async Task<bool> DecrementDislikes(int commentId)
        {
            Comment comment = _context.Comments.Find(commentId);

            if (comment == null)
            {
                return false;
            }

            comment.Dislikes -= 1;
            _context.SaveChanges();
            return true;
        }

        public virtual async Task<bool> DecrementLikes(int commentId)
        {
            Comment comment = _context.Comments.Find(commentId);

            if (comment == null)
            {
                return false;
            }

            comment.Likes -= 1;
            _context.SaveChanges();
            return true;
        }

        public virtual async Task<bool> DeleteByPost(int postId)
        {
            List<Comment> toDelete = _context.Comments.AsQueryable<Comment>()
                .Where(c => c.ForPost == postId)
                .ToList();

            if (toDelete.Count == 0) return false;

            _context.RemoveRange(toDelete);
            _context.SaveChanges();
            return true;
        }

        public virtual async Task<bool> DeleteByUser(string userId)
        {
            List<Comment> toDelete = _context.Comments.AsQueryable<Comment>()
                .Where(c => c.CreatedBy == userId)
                .ToList();

            if (toDelete.Count == 0) return false;

            _context.RemoveRange(toDelete);
            _context.SaveChanges();
            return true;
        }

        public virtual async Task<bool> DeleteComment(int commentId)
        {
            Comment comment = _context.Comments.Find(commentId);

            if (comment == null) return false;

            _context.Comments.Remove(comment);
            _context.SaveChanges();
            return true;
        }

        public virtual async Task<bool> EditCommentContent(int commentId, Comment newData)
        {
            Comment comment = await _context.Comments.FindAsync(commentId);

            if (comment == null)
            {
                return false;
            }

            foreach (PropertyInfo prop in newData.GetType().GetProperties())
            {
                if (prop.GetValue(newData, null) == null)
                {
                    return false;
                }
            }

            comment.Content = newData.Content;
            _context.SaveChanges();
            return true;
        }

        public virtual List<Comment> GetByPost(int postId)
        {
            List<Comment> result = _context.Comments.AsQueryable<Comment>()
                .Where(comment => comment.ForPost == postId)
                .ToList();

            if (result.Count == 0)
            {
                return null;
            }

            return result;
        }

        public virtual List<Comment> GetByUser(string userId)
        {
            List<Comment> result = _context.Comments.AsQueryable<Comment>()
                .Where(comment => comment.CreatedBy == userId)
                .ToList();

            if (result.Count == 0)
            {
                return null;
            }

            return result;
        }

        public virtual async Task<bool> IncrementDislikes(int commentId)
        {
            Comment comment = _context.Comments.Find(commentId);

            if (comment == null)
            {
                return false;
            }

            comment.Dislikes += 1;
            _context.SaveChanges();
            return true;
        }

        public virtual async Task<bool> IncrementLikes(int commentId)
        {
            Comment comment = _context.Comments.Find(commentId);

            if (comment == null)
            {
                return false;
            }

            comment.Likes += 1;
            _context.SaveChanges();
            return true;
        }
    }
}
