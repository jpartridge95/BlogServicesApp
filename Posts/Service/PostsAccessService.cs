using Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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


        public virtual async Task<bool> CreatePost(Post post)
        {
            var asJson = JsonSerializer.Serialize<Post>(post);
            var content = new StringContent(asJson, Encoding.UTF8, "application/json");

            var query = "createpost";

            var response = await _httpClient.PostAsync(query, content);

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return true;
            }
            
            return false;
        }

        public virtual async Task<bool> DecrementDislikes(int postId)
        {
            var query = String.Format("decdislikes?postid={0}", postId);

            var response = await _httpClient.PatchAsync(query, null);

            if (response.StatusCode == HttpStatusCode.NoContent) return true;

            return false;
        }

        public virtual async Task<bool> DecrementLikes(int postId)
        {
            var query = String.Format("declikes?postid={0}", postId);

            var response = await _httpClient.PatchAsync(query, null);

            if (response.StatusCode == HttpStatusCode.NoContent) return true;

            return false;
        }

        public virtual async Task<bool> DeleteByUser(int userId)
        {
            var query = String.Format("byuser?userid={0}", userId);

            var response = await _httpClient.DeleteAsync(query);

            if (response.StatusCode == HttpStatusCode.NoContent) return true;

            return false;
        }

        public virtual async Task<bool> DeletePost(int postId)
        {
            var query = String.Format("byId?postid={0}", postId);

            var response = await _httpClient.DeleteAsync(query);

            if (response.StatusCode == HttpStatusCode.NoContent) return true;

            return false;
        }

        public virtual async Task<bool> EditPost(int postId, Post newData)
        {
            var asJson = JsonSerializer.Serialize<Post>(newData);
            var content = new StringContent(asJson, Encoding.UTF8, "application/json");

            var query = String.Format("editpost?postId={0}", postId);

            var response = await _httpClient.PatchAsync(query, content);

            if (response.StatusCode == HttpStatusCode.NoContent) return true;

            return false;
        }

        public virtual async Task<List<Post>> GetAllPosts()
        {
            var query = "all";

            List<Post> response;

            try
            {
                response = await _httpClient.GetFromJsonAsync<List<Post>>(query);

                if (response.Count == 0)
                {
                    return null;
                }
            }
            catch
            {
                response = null;
            }

            return response;
        }

        public virtual async Task<List<Post>> GetPostsFromUser(int userId)
        {
            var query = String.Format("byUser?userId={0}", userId);

            List<Post> response;

            try
            {
                response = await _httpClient.GetFromJsonAsync<List<Post>>(query);

                if (response.Count == 0)
                {
                    return null;
                }
            }
            catch
            {
                response = null;
            }

            return response;
        }

        public virtual async Task<Post> GetSinglePost(int postId)
        {
            var query = String.Format("byPostId?postId={0}", postId);

            Post response;

            try
            {
                response = await _httpClient.GetFromJsonAsync<Post>(query);
            }
            catch
            {
                response = null;
            }

            return response;
        }

        public virtual async Task<bool> IncrementDislikes(int postId)
        {
            var query = String.Format("incDislikes?postId={0}", postId);

            var response = await _httpClient.PatchAsync(query, null);

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return true;
            }

            return false;
        }

        public virtual async Task<bool> IncrementLikes(int postId)
        {
            var query = String.Format("incLikes?postId={0}", postId);

            var response = await _httpClient.PatchAsync(query, null);

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return true;
            }

            return false;
        }
    }
}
