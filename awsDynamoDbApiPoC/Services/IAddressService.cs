using System;
using awsDynamoDbApiPoC.Repositories.Models;

namespace awsDynamoDbApiPoC.Services
{
	public interface IAddressService
	{
		public Task<AddressLookup?> GetAddressLookup(int postalCode, string countryIso3);

		public Task<bool> SaveAddressLookup(AddressLookup address);
	}
}

