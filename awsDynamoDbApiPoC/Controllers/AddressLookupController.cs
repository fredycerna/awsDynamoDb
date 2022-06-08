using awsDynamoDbApiPoC.Repositories.Models;
using awsDynamoDbApiPoC.Services;
using Microsoft.AspNetCore.Mvc;

namespace awsDynamoDbApiPoC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressLookupController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressLookupController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet]
        [Route("{countryIso3}/{postalCode}")]
        public async Task<ActionResult<AddressLookup>> GetAddressLookup(int postalCode, string countryIso3)
        {
            var addressInfo = await _addressService.GetAddressLookup(postalCode, countryIso3);
            if (addressInfo == null)
                return NotFound();
            return addressInfo;
        }

        [HttpPost]
        [Route("{countryIso3}/{postalCode}")]
        public async Task<ActionResult> SaveAddressLookup(int postalCode, string countryIso3,
           [FromBody] Dictionary<string, string> address)
        {
            var addressLookup = new AddressLookup
            {
                PostalCode = postalCode,
                CountryIso3 = countryIso3,
                Address = address
            };
            var result = await _addressService.SaveAddressLookup(addressLookup);
            if (result)
                return CreatedAtAction(nameof(GetAddressLookup), 
                    new {postalCode, countryIso3}, 
                    addressLookup);
            else
                return BadRequest();
        }
        
        

    }
}
