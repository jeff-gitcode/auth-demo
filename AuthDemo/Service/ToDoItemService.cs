using Microsoft.Extensions.Options;

namespace AuthDemo.Service
{
    public interface ITodoItemService
    {
        Task<IEnumerable<TodoItem>> GetTodoItems();
        Task<TodoItem> SaveTodoItem(TodoItem user);
    }

    public class TodoItemService : ITodoItemService
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<TodoItemOptions> _options;

        public TodoItemService(HttpClient httpClient, IOptions<TodoItemOptions> options)
        {
            _httpClient = httpClient;
            _options = options;
            _httpClient.BaseAddress = new Uri(_options.Value.BaseUri);
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }
        public async Task<IEnumerable<TodoItem>> GetTodoItems()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<TodoItem>>($"{_options.Value.BaseUri + _options.Value.UserEndpoint}");
        }

        public async Task<TodoItem> SaveTodoItem(TodoItem user)
        {
            using HttpResponseMessage response = await _httpClient.PostAsJsonAsync<TodoItem>($"{_options.Value.BaseUri + _options.Value.UserEndpoint}", user);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TodoItem>();
        }
    }

    public class TodoItemOptions
    {
        public const string Options = "TodoItemOptions";
        public string BaseUri { get; set; } = string.Empty;
        public string UserEndpoint { get; set; } = string.Empty;
    }
}
