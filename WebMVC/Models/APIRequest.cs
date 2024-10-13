using Web.Base;

namespace WebMVC.Models;

public class ApiRequest {
    public SD.ApiType ApiType { get; set; }
    public required string Url { get; set; }
    public object? Data { get; set; }
}