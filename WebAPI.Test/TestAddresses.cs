using System.Text;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;
using WebAPI.Model;

namespace WebAPI.Test;

// https://www.code4it.dev/blog/advanced-integration-tests-webapplicationfactory/
// https://code-maze.com/aspnet-core-integration-testing/
public class TestAddresses {
    private TestingWebAppFactory _factory;
    private HttpClient _client;

    [OneTimeSetUp]
    public void OneTimeSetup() => _factory = new TestingWebAppFactory();

    [OneTimeTearDown]
    public void OneTimeTearDown() => _factory.Dispose();

    [SetUp]
    public void Setup() {
        _client = _factory.CreateClient();

        var context = _factory.Services.CreateScope().ServiceProvider.GetRequiredService<TeacherDbContext>();
        ensureCleanDatabase(context);
    }

    private void ensureCleanDatabase(TeacherDbContext context) {
        // SS: do not remove the seed data
        context.Database.ExecuteSqlRaw("DELETE FROM Addresses WHERE Id != 1");
        context.ChangeTracker.Clear();
        context.SaveChanges();
    }

    [TearDown]
    public void TearDown() {
        _client.Dispose();
    }

    [Test]
    public async Task GetAddresses() {
        // Arrange

        // Act
        var response = await _client.GetAsync("http://localhost:5032/api/Address");
        response.EnsureSuccessStatusCode();

        // Assert
        var responseString = await response.Content.ReadAsStringAsync();
        List<AddressDTO>? addresses = JsonConvert.DeserializeObject<List<AddressDTO>>(responseString);
        Assert.That(addresses?.Count, Is.EqualTo(1));
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
        requestMessage.Content =
            new StringContent(JsonConvert.SerializeObject(address), Encoding.UTF8, "application/json");
        var response = await _client.SendAsync(requestMessage);
        response.EnsureSuccessStatusCode();

        // Assert
        var responseString = await response.Content.ReadAsStringAsync();
        address = JsonConvert.DeserializeObject<AddressDTO>(responseString)!;
        Assert.That(address.Id, Is.GreaterThan(0));
    }

    [Test]
    public async Task UpdateAddress() {
        // Arrange
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5032/api/Address");
        var response = await _client.SendAsync(requestMessage);
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        AddressDTO addressDto = JsonConvert.DeserializeObject<List<AddressDTO>>(responseString).First();

        // Act
        addressDto.City = "This is a test city";

        var uri = $"http://localhost:5032/api/Address/{addressDto.Id}";
        requestMessage = new HttpRequestMessage(HttpMethod.Put, uri);
        requestMessage.Content =
            new StringContent(JsonConvert.SerializeObject(addressDto), Encoding.UTF8, "application/json");
        response = await _client.SendAsync(requestMessage);
        response.EnsureSuccessStatusCode();

        // Assert
        responseString = await response.Content.ReadAsStringAsync();
        var addressDto2 = JsonConvert.DeserializeObject<AddressDTO>(responseString)!;
        Assert.That(addressDto2.City, Is.EqualTo(addressDto.City));
    }
}