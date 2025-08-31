using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using TestProject.Dtos;
using Xunit;

namespace Tests;
public class ProductApiTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory = factory;

    [Fact]
    public async Task PostAndGetProduct()
    {
        var client = _factory.CreateClient();

        string name = "UN!1";
        decimal price = 9.9m;

        var createDto = new ProductCreateDto(name, price);

        var postResponse = await client.PostAsJsonAsync("/api/products", createDto);
        Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);

        var created = await postResponse.Content.ReadFromJsonAsync<ProductReadDto>();
        Assert.NotNull(created);
        Assert.Equal(name, created!.Name);
        Assert.Equal(price, created.Price);
        Assert.NotEqual(Guid.Empty, created.Id);

        var location = postResponse.Headers.Location;
        Assert.NotNull(location);

        var getResponse = await client.GetAsync(location);
        getResponse.EnsureSuccessStatusCode();

        var fetched = await getResponse.Content.ReadFromJsonAsync<ProductReadDto>();
        Assert.NotNull(fetched);
        Assert.Equal(created.Id, fetched!.Id);
        Assert.Equal(name, fetched.Name);
        Assert.Equal(price, fetched.Price);
    }

    [Fact]
    public async Task GetIncorrectId()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync($"/products/{Guid.NewGuid()}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
