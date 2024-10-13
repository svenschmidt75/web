using WebMVC.Models;

namespace WebMVC.Services.IServices;

public interface IBaseService {
    ApiResponse Response { get; set; }
    Task<T> SendAsync<T>(ApiRequest apiRequest);
}