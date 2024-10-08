using System.Text;
using Newtonsoft.Json;
using NUnit.Framework;
using WebAPI.Model;

namespace WebAPI.Test;

// https://www.code4it.dev/blog/advanced-integration-tests-webapplicationfactory/
// https://code-maze.com/aspnet-core-integration-testing/
public class TestAddresses : IDisposable {
    private TestingWebAppFactory _factory;
    private HttpClient _client;

    [OneTimeSetUp]
    public void OneTimeSetup() => _factory = new TestingWebAppFactory();

    [SetUp]
    public void Setup() {
        _client = _factory.CreateClient();
    }

    [TearDown]
    public void TearDown() {
        _factory?.Dispose();
    }

    public void Dispose() => _factory?.Dispose();

    [Test]
    public async Task TestGetAddresses() {
        // Arrange

        // Act
        var response = await _client.GetAsync("http://localhost:5032/api/Address");
        response.EnsureSuccessStatusCode();

        // Assert
        var responseString = await response.Content.ReadAsStringAsync();
        List<AddressDTO>? address = JsonConvert.DeserializeObject<List<AddressDTO>>(responseString);
        Assert.That(address?.Count, Is.EqualTo(1));
    }

    [Test]
    public async Task CreateAddress() {
        // Arrange
        AddressDTO address = new AddressDTO {
            City = "City",
            StreetName = "Street Name",
            UnitNumber = 12,
            Code = "OX14 1DX",
            Country = "UK",
            StreetNumber = 13,
            State = "Oxfordshire",
            Suburb = "Oxfordshire",
        };

        // Act
        var requestMessage = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5032/api/Address");
        requestMessage.Content = new StringContent(JsonConvert.SerializeObject(address), Encoding.UTF8, "application/json");
        var response = await _client.SendAsync(requestMessage);
        response.EnsureSuccessStatusCode();

        // Assert
        var responseString = await response.Content.ReadAsStringAsync();
        address = JsonConvert.DeserializeObject<AddressDTO>(responseString)!;
        Assert.That(address.Id, Is.GreaterThan(0));
    }

}