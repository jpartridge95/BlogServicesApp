using Data.Context;
using Models;
using System.Linq;
using System.Reflection;

namespace Data.DataAccess
{
    public class PostAccess : IPostActions
    {

        SocialContext _context;

        public PostAccess(SocialContext context)
        {
            _context = context;
        }

        public virtual async Task<bool> CreatePost(Post post)
        {
            try
            {
                _context.AddAsync(post);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual async Task<bool> DecrementDislikes(int postId)
        {
            Post post = _context.Posts.Find(postId);

            if (post == null)
            {
                return false;
            }

            post.Dislikes -= 1;
            _context.SaveChanges();
            return true;
        }

        public virtual async Task<bool> DecrementLikes(int postId)
        {
            Post post = _context.Posts.Find(postId);

            if (post == null)
            {
                return false;
            }

            post.Likes -= 1;
            _context.SaveChanges();
            return true;
        }

        public virtual async Task<bool> DeleteByUser(int userId)
        {
            List<Post> toDelete = _context.Posts.AsQueryable()
                .Where(x => x.CreatedBy == userId)
                .ToList();

            if (toDelete.Count > 0)
            {
                _context.Posts.RemoveRange(toDelete);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public virtual async Task<bool> DeletePost(int postId)
        {
            Post toDelete = await _context.Posts.FindAsync(postId);
            if (toDelete != null)
            {
                _context.Posts.Remove(toDelete);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public virtual async Task<bool> EditPost(int postId, Post newData)
        {
            Post post = _context.Posts.Find(postId);

            if (post == null)
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

            // CreatedBy is not changed below as it should remain constant and unchanged.

            post.Title = newData.Title;
            post.Content = newData.Content;
            post.Dislikes = newData.Dislikes;
            post.Likes = newData.Likes;

            _context.SaveChanges();

            return true;
        }

        public virtual async Task<List<Post>> GetAllPosts()
        {
            try
            {
                List<Post> posts = _context.Posts.ToList();
                return posts;
            }
            catch
            {
                return new List<Post>();
            }
        }

        public virtual async Task<List<Post>> GetPostsFromUser(int userId)
        {
            try
            {
                IQueryable<Post> posts = _context.Posts.AsQueryable<Post>();
                List<Post> filtered = posts
                    .Where(x => x.CreatedBy == userId)
                    .ToList();
                return filtered;
            }
            catch
            {
                return new List<Post>();
            }
        }

        public virtual async Task<Post> GetSinglePost(int postId)
        {
            try
            {
                IQueryable<Post> posts = _context.Posts.AsQueryable<Post>();
                Post result = posts
                    .Where(x => x.Id == postId)
                    .First();
                return result;
            }
            catch
            {
                throw new IdNotFoundException(postId);
            }
        }

        public virtual async Task<bool> IncrementDislikes(int postId)
        {
            Post post = _context.Posts.Find(postId);

            if (post == null)
            {
                return false;
            }

            post.Dislikes += 1;
            _context.SaveChanges();
            return true;
        }

        public virtual async Task<bool> IncrementLikes(int postId)
        {
            Post post = _context.Posts.Find(postId);

            if (post == null)
            {
                return false;
            }

            post.Likes += 1;
            _context.SaveChanges();
            return true;
        }
    }
}
