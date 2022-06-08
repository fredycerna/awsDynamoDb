﻿using System;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using awsDynamoDbApiPoC.Repositories.Models;

namespace awsDynamoDbApiPoC.Repositories
{
	public class AddressRepository : IAddressRepository
	{
		private readonly DynamoDBContext _context;
        
		public AddressRepository(IAmazonDynamoDB dynamoDbClient)
		{
			_context = new DynamoDBContext(dynamoDbClient);
		}

		public async Task<AddressLookup?> GetAddressLookup(int postalCode, string countryIso3)
		{
			try
			{
				return await _context.LoadAsync<AddressLookup>(postalCode, countryIso3);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Debugging {ex.Message}");
				return null;
			}
		}



	}
}

