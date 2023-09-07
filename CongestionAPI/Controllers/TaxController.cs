using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaxController : ControllerBase
    {
        private readonly ITaxService _taxCalculatorService;

        public TaxController(ITaxService taxCalculatorService)
        {
            _taxCalculatorService = taxCalculatorService;
        }

        [HttpGet("CongestionTax")]
        public async Task<IActionResult> Get([FromQuery] int cityId, [FromQuery] int vehicleId, [FromQuery] DateTime[] dates)
        {
            var result = await _taxCalculatorService.GetCongestionTaxAsync(cityId, vehicleId, dates);

            return Ok(result);
        }
    }
}
