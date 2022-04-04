using Models;
using Serilog;

namespace BlazorFrontend.Services
{
    public class PostsService : IPostActions
    {
        HttpClient _client;

        public PostsService(HttpClient client)
        {
            _client = client;
            
        }

        public Task<bool> CreatePost(Post post)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DecrementDislikes(int postId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DecrementLikes(int postId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByUser(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePost(int postId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditPost(int postId, Post newData)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Post>> GetAllPosts()
        {
            List<Post> posts;

            string query = "all";
            Log.Debug($"request sent to: {_client.BaseAddress}{query}");

            try
            {
                posts = await _client.GetFromJsonAsync<List<Post>>(query);
                
                return posts;
            }
            catch (Exception ex)
            {
                Log.Debug(ex.Message);
                return null;
            }
            
        }

        public Task<List<Post>> GetPostsFromUser(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<Post> GetSinglePost(int postId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IncrementDislikes(int postId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IncrementLikes(int postId)
        {
            throw new NotImplementedException();
        }
    }
}
