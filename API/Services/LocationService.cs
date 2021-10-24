using API.DTOs;
using API.Services.Interface;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace API.Services
{
    public class LocationService : ILocationService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly string _accessKey;

        public LocationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _baseUrl = "http://api.ipstack.com/";
            
            // provide your Access key
            _accessKey = "";
            _httpClient.BaseAddress = new Uri(_baseUrl);
        }

        public async Task<string> GetCurrentCity(IPAddress ipAddress)
        {
            var location = await GetCurrentLocation(ipAddress);
            var city = location.City;
            return city;
        }

        private async Task<LocationDTO> GetCurrentLocation(IPAddress ipAddress)
        {
            var result = await _httpClient.GetAsync($"{ipAddress}?access_key={_accessKey}");
            string jsonString = await result.Content.ReadAsStringAsync();
            var location = JsonConvert.DeserializeObject<LocationDTO>(jsonString);
            return location;
        }
    }
}
