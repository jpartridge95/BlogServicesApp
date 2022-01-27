using Models;

namespace Posts.Service
{
    public class PostsAccessService : IPostActions
    {

        private HttpClient _httpClient;

        public PostsAccessService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(Environment.GetEnvironmentVariable("DATA_BASE_URL"));
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

        public Task<bool> DeleteByUser(int userId)
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

        public Task<List<Post>> GetAllPosts()
        {
            throw new NotImplementedException();
        }

        public Task<List<Post>> GetPostsFromUser(int userId)
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
