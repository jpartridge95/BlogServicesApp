namespace Posts.Service
{
    public class CommentsInteractionService
    {
        private HttpClient _httpClient;

        public CommentsInteractionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    }
}
