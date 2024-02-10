using System.Text.Json;
using AuthDemo.Service;
using Bogus;
using RichardSzalay.MockHttp;

namespace UnitTests;

public class UserServiceTests
{
    [Fact]
    public async Task PostService_When_Should_Return()
    {

        var mockHttpMessageHandler = new MockHttpMessageHandler();
        var posts = new Faker<Posts>().Generate(2);
        var jsonString = JsonSerializer.Serialize(posts);

        //setup a respond for endpoint
        mockHttpMessageHandler
            .When("http://mytest/posts")
            .Respond("application/json", jsonString);// return JSON

        // Inject the handler or client into your application code
        var client = mockHttpMessageHandler.ToHttpClient();
        client.BaseAddress = new Uri("http://mytest/");
        //inject the handler
        var cryptoService = new PostService(client);

        var result = await cryptoService.GetPosts();

        // result.Should().HaveCount(9);
    }
}