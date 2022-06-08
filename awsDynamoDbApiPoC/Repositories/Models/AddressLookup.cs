using Amazon.DynamoDBv2.DataModel;

namespace awsDynamoDbApiPoC.Repositories.Models;

[DynamoDBTable("address-service-lookup")]
public class AddressLookup
{
    public AddressLookup()
    {
        CountryIso3 = string.Empty;
        Address = new Dictionary<string, string>();
    }
    
    [DynamoDBHashKey]
    public int PostalCode { get; set; }

    [DynamoDBRangeKey]
    public string CountryIso3 { get; set; }
    
    public Dictionary<string,string> Address { get; set; }
    
    public long Expires { get; set; }
    
}