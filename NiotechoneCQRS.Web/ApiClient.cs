using System.Net.Http.Headers;

namespace NiotechoneCQRS.Web;

public class ApiClient
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public string BaseUrl { get; set; } = string.Empty;
    public ApiClient(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
    }
    private void AttachToken()
    {
        var token = _httpContextAccessor.HttpContext?
            .Session.GetString("JwtToken");

        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }
    }
    public async Task<TResponse?> GetAsync<TResponse>(string url)
    {
        AttachToken();
        var response = await _httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode)
            return default;

        string json = await response.Content.ReadAsStringAsync();
        return await response.Content.ReadFromJsonAsync<TResponse>();
    }

    public async Task<TResponse?> PostAsync<TRequest, TResponse>(string url, TRequest data)
    {
        AttachToken();
        var response = await _httpClient.PostAsJsonAsync(url, data);
        if (!response.IsSuccessStatusCode)
            return default;

        return await response.Content.ReadFromJsonAsync<TResponse>();
    }

    public async Task<TResponse?> PutAsync<TRequest, TResponse>(string url, TRequest data)
    {
        AttachToken();
        var response = await _httpClient.PutAsJsonAsync(url, data);
        if (!response.IsSuccessStatusCode)
            return default;

        return await response.Content.ReadFromJsonAsync<TResponse>();
    }

    public async Task<bool> DeleteAsync(string url)
    {
        AttachToken();
        var response = await _httpClient.DeleteAsync(url);
        return response.IsSuccessStatusCode;
    }
}
