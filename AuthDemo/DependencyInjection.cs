using AuthDemo.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Polly;
using Polly.Extensions.Http;

public static class DependencyInjection
{
    public static IServiceCollection AddTodoService(this IServiceCollection services)
    {
        services.AddHttpClient<ITodoItemService, TodoItemService>()
            .SetHandlerLifetime(TimeSpan.FromMinutes(1))
            .AddPolicyHandler(GetRetryPolicy());
        return services;
    }

    public static IServiceCollection AddPostService(this IServiceCollection services)
    {
        services.AddHttpClient<IPostService, PostService>(httpClient =>
        {
            httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");

            httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            httpClient.Timeout = TimeSpan.FromMinutes(1);
        });

        return services;
    }

    static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }


}
