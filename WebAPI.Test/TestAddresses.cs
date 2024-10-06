using NUnit.Framework;

namespace WebAPI.Test;

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
    public async Task Test1() {
        // Arrange

        // Act
        var response = await _client.GetAsync("/Address");
        response.EnsureSuccessStatusCode();

        // Assert
        var responseString = await response.Content.ReadAsStringAsync();
    }
}