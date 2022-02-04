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


    }
}
