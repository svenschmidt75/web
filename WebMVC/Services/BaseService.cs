using System.Text;
using System.Text.Json;
using Web.Base;
using WebMVC.Models;
using WebMVC.Services.IServices;

namespace WebMVC.Services;

public class BaseService : IBaseService {
    public BaseService(IHttpClientFactory clientFactory) {
        Response = new();
        HttpClientFactory = clientFactory;
    }

    public IHttpClientFactory HttpClientFactory { get; init; }

    public ApiResponse Response { get; set; }

    public async Task<T> SendAsync<T>(ApiRequest apiRequest) {
        try {
            var client = HttpClientFactory.CreateClient();

            HttpRequestMessage requestMessage = new HttpRequestMessage();
            requestMessage.Method = apiRequest.ApiType switch {
                SD.ApiType.GET => HttpMethod.Get,
                SD.ApiType.POST => HttpMethod.Post,
                SD.ApiType.PUT => HttpMethod.Put,
                SD.ApiType.DELETE => HttpMethod.Delete,
                _ => throw new ArgumentOutOfRangeException()
            };
            requestMessage.Headers.Add("Accept", "application/json");
            requestMessage.RequestUri = new Uri(apiRequest.Url);

            if (apiRequest.Data != null) {
                requestMessage.Content = new StringContent(JsonSerializer.Serialize(apiRequest.Data), Encoding.UTF8,
                    "application/json");
            }

            HttpResponseMessage response = await client.SendAsync(requestMessage);

            var apiContent = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonSerializer.Deserialize<T>(apiContent)!;
            return apiResponse;
        }
        catch (Exception ex) {
            var apiResponse = new ApiResponse {
                IsSuccess = false,
                ErrorMessages = [ex.Message],
            };
            var apiResponseJSON = JsonSerializer.Serialize(apiResponse)!;
            var apiResponseT = JsonSerializer.Deserialize<T>(apiResponseJSON)!;
            return apiResponseT;
        }

    }
}