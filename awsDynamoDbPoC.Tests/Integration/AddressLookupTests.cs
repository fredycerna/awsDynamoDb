
using System.Net;
using System.Text;
using System.Text.Json.Nodes;
using awsDynamoDbPoC.Core.Extensions;
using awsDynamoDbPoC.Tests.Setup;
using awsDynamoDbPoC.Web;
using awsDynamoDbPoC.Core.Repositories.Models;
using Newtonsoft.Json;

namespace awsDynamoDbPoC.Tests.Integration;

[Collection("api")]
public class AddressLookupTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient client;
    public AddressLookupTests(CustomWebApplicationFactory<Program> factory)
    {
        client = factory.CreateClient();
    }

    [Fact]
    public async Task SavingAddressLookup()
    {
        string countryIso3 = "MEX";
        int postalCode = 12345;
        var address= new Dictionary<string, string>
            {
                { "CountryName", "Mexico"},
                {"State" , "Aguascalientes"},
                {"City" , "Ciudad 1"},
                {"Municipality" , "Municipality 1"}   
            };
        var json = JsonConvert.SerializeObject(address);
        var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
        client.Timeout = TimeSpan.FromMinutes(10);
        var response = await client.PostAsync($"/api/AddressLookup/{countryIso3}/{postalCode}", stringContent);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
    
}