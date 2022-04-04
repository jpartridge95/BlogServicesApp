using Models;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Comments.Services
{
    public class DataService : ICommentActions
    {

        private HttpClient _httpClient;
        
        public DataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(Environment.GetEnvironmentVariable("DATA_BASEURL"));
        }


        public virtual async Task<bool> CreateComment(Comment comment)
        {
            var asJson = JsonSerializer.Serialize<Comment>(comment);
            var httpContent = new StringContent(asJson, Encoding.UTF8, "application/json");

            var query = "createcomment";

            var response = await _httpClient.PostAsync(query, httpContent);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }

            return false;
        }

        public virtual async Task<bool> DecrementDislikes(int commentId)
        {
            var query = String.Format("decdislikes?commentId={0}", commentId);

            var response = await _httpClient.PatchAsync(query, null);

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return true;
            }

            return false;
        }

        public virtual async Task<bool> DecrementLikes(int commentId)
        {
            var query = String.Format("declikes?commentId={0}", commentId);

            var response = await _httpClient.PatchAsync(query, null);

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return true;
            }

            return false;
        }

        public virtual async Task<bool> DeleteByPost(int postId)
        {
            var query = String.Format("bypost?postId={0}", postId);

            var response = await _httpClient.DeleteAsync(query);

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return true;
            }

            return false;
        }

        public virtual async Task<bool> DeleteByUser(string userId)
        {
            var query = String.Format("byuser?userId={0}", userId);

            var response = await _httpClient.DeleteAsync(query);

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return true;
            }

            return false;
        }

        public virtual async Task<bool> DeleteComment(int commentId)
        {
            var query = String.Format("byid?commentid={0}", commentId);

            var response = await _httpClient.DeleteAsync(query);

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return true;
            }

            return false;
        }

        public virtual async Task<bool> EditCommentContent(int commentId, Comment newData)
        {
            var query = String.Format("editcomment?commentid={0}", commentId);

            var asJson = JsonSerializer.Serialize<Comment>(newData);
            var httpContent = new StringContent(asJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(query, httpContent);

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return true;
            }

            return false;
        }

        public virtual List<Comment> GetByPost(int postId)
        {
            var query = String.Format("bypost?postId={0}", postId);

            try
            {
                var response = _httpClient.GetFromJsonAsync<List<Comment>>(query);
                return response.Result;
            }
            catch
            {
                return null;
            }
        }

        public virtual List<Comment> GetByUser(string userId)
        {
            var query = String.Format("byuser?userId={0}", userId);

            try
            {
                var response = _httpClient.GetFromJsonAsync<List<Comment>>(query);
                Console.WriteLine(_httpClient.BaseAddress);

                return response.Result;
            }
            catch
            {
                return null;
            }
        }

        public virtual async Task<bool> IncrementDislikes(int commentId)
        {
            var query = String.Format("incDislikes?commentid={0}", commentId);

            var response = await _httpClient.PatchAsync(query, null);

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return true;
            }

            return false;
        }

        public virtual async Task<bool> IncrementLikes(int commentId)
        {
            var query = String.Format("inclikes?commentid={0}", commentId);

            
            var response = await _httpClient.PatchAsync(query, null);
            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return true;
            }

            return false;
            
        }
    }
}
