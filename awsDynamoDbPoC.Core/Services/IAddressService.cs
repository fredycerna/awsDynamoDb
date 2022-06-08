using awsDynamoDbPoC.Core.Repositories.Models;

namespace awsDynamoDbPoC.Core.Services;

public interface IAddressService
{
    public Task<AddressLookup?> GetAddressLookup(int postalCode, string countryIso3);

    public Task<bool> SaveAddressLookup(AddressLookup address);
}