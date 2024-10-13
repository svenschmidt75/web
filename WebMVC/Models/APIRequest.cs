using Web.Base;

namespace WebMVC.Models;

public class APIRequest {
    public SD.ApiType ApiType { get; set; }
    public string Url { get; set; }
    public object Data { get; set; }
}