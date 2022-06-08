using System;
using awsDynamoDbApiPoC.Repositories.Models;

namespace awsDynamoDbApiPoC.Repositories
{
	public interface IAddressRepository
	{
		public Task<AddressLookup?> GetAddressLookup(int postalCode, string countryIso3);
	}
}

