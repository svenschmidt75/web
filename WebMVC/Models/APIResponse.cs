using System.Net;

namespace WebMVC.Models;

public class ApiResponse {
    public HttpStatusCode StatusCode { get; set; }
    public bool IsSuccess { get; set; }
    public List<string> ErrorMessages { get; set; } = new();
    public object? Result { get; set; }
}