using System;
using awsDynamoDbApiPoC.Repositories;
using awsDynamoDbApiPoC.Repositories.Models;

namespace awsDynamoDbApiPoC.Services
{
	public class AddressService :IAddressService
	{
		private readonly IAddressRepository _addressRepository;

		public AddressService(IAddressRepository addressRepository)
		{
			_addressRepository = addressRepository;
		}
		
		public async Task<AddressLookup?> GetAddressLookup(int postalCode, string countryIso3)
		{
			return  await _addressRepository.GetAddressLookup(postalCode, countryIso3);
		}
	}
}

