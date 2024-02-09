namespace AuthDemo.Service
{
    public interface IPostService
    {
        Task<dynamic> GetPosts();
    }

    public class PostService : IPostService
    {
        private readonly HttpClient _httpClient;

        public PostService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<dynamic> GetPosts()
        {
            return await _httpClient.GetFromJsonAsync<dynamic>("posts");
        }
    }
}
