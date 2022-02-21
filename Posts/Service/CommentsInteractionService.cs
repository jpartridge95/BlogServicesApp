using System.Net;

namespace Posts.Service
{
    public class CommentsInteractionService
    {
        private HttpClient _httpClient;

        public CommentsInteractionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(Environment.GetEnvironmentVariable("COMMENTS_BASE_URL"));
        }

        public virtual async Task<bool> DeleteCommentsById(int userId)
        {
            var query = String.Format("byuser?userId={0}", userId);

            var response = await _httpClient.DeleteAsync(query);

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return true;
            }

            return false;
        }

        public virtual async Task<bool> DeleteCommentsByPost(int postId)
        {
            var query = String.Format("bypost?postId={0}", postId);

            var response = await _httpClient.DeleteAsync(query);

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return true;
            }

            return false;
        }
    }
}
