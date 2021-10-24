using API.DTOs;
using API.Models;
using API.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ResourceController: ControllerBase
    {
        private readonly ILocationService _locationService;
        private readonly IListingService _listingService;

        public ResourceController(
            ILocationService locationService, 
            IListingService listingService)
        {
            _locationService = locationService;
            _listingService = listingService;
        }

        [HttpGet("Candidate")]
        public IActionResult GetCandidate()
        {
            return Ok(
                new CandidateDTO {
                    Name = "test",
                    Phone = "test"
                });
        }

        [HttpGet("Location")]
        public async Task<IActionResult> GetLocation([FromQuery] string ipAddress)
        {
            bool ValidIP = IPAddress.TryParse(ipAddress, out IPAddress ip);
            if (!ValidIP)
            {
                return BadRequest("Invalid IpAddress");
            }

            var city = await _locationService.GetCurrentCity(ip);
            return Ok(city);
        }

        [HttpGet("Listings")]
        public async Task<IActionResult> GetListings(
            [FromQuery] int numberOfPassengers,
            [FromQuery] int page = 1,
            [FromQuery] int size = 10)
        {
            if (numberOfPassengers <= 0)
            {
                return BadRequest("Invalid query parameter number of passengers");
            }

            var pageOptions = new PageRequest(page, size);
            var listings = await _listingService.GetListingsWithTotalPrice(numberOfPassengers, pageOptions);
            return Ok(listings);
        }
    }
}
