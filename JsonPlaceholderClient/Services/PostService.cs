using JsonPlaceholderClient.Models;

namespace JsonPlaceholderClient.Services;

using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

public class PostService
{
    private readonly HttpClient _client;
    private const string BaseUrl = "https://jsonplaceholder.typicode.com/posts";

    public PostService()
    {
        _client = new HttpClient();
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<List<Post>> GetAllPostsAsync()
    {
        var response = await _client.GetStringAsync(BaseUrl);
        return JsonConvert.DeserializeObject<List<Post>>(response);
    }

    public async Task<Post> GetPostByIdAsync(int id)
    {
        var response = await _client.GetStringAsync($"{BaseUrl}/{id}");
        return JsonConvert.DeserializeObject<Post>(response);
    }

    public async Task<Post> CreatePostAsync(Post post)
    {
        var content = new StringContent(JsonConvert.SerializeObject(post), Encoding.UTF8, "application/json");
        var response = await _client.PostAsync(BaseUrl, content);
        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<Post>(json);
    }

    public async Task<Post> UpdatePostAsync(int id, Post post)
    {
        var content = new StringContent(JsonConvert.SerializeObject(post), Encoding.UTF8, "application/json");
        var response = await _client.PutAsync($"{BaseUrl}/{id}", content);
        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<Post>(json);
    }

    public async Task DeletePostAsync(int id)
    {
        await _client.DeleteAsync($"{BaseUrl}/{id}");
    }
}
