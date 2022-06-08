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

    }
}
