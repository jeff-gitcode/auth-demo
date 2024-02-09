using Newtonsoft.Json;
using System.Net;

namespace AuthDemo.Service
{
    public interface IPostService
    {
        Task<IEnumerable<Posts>> GetPosts();
    }

    public class PostService : IPostService
    {
        private readonly HttpClient _httpClient;

        public PostService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Posts>> GetPosts()
        {
            var response = await _httpClient.GetAsync("posts");

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                // need to refresh the request security token!
            }

            if (response.IsSuccessStatusCode)
            {
                // the status code was 2xx
                if (response.Content.Headers.ContentType.MediaType == "application/json")
                {
                    var json = await response.Content.ReadAsStringAsync();

                    var posts = JsonConvert.DeserializeObject<IEnumerable<Posts>>(json);

                    return posts;
                }
            }


            return await _httpClient.GetFromJsonAsync<IEnumerable<Posts>>("posts");
        }
    }
}
