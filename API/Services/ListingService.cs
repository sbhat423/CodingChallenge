using API.DTOs;
using API.Models;
using API.Services.Interface;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace API.Services
{
    public class ListingService: IListingService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ListingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _baseUrl = "https://jayridechallengeapi.azurewebsites.net/api/";
            _httpClient.BaseAddress = new Uri(_baseUrl);
        }

        public async Task<IEnumerable<ListingWithTotalPriceDTO>> GetListingsWithTotalPrice(int numberOfPassengers, PageRequest pageOptions)
        {
            if (numberOfPassengers <= 0) 
            {
                throw new ArgumentException("Invalid number of passengers");
            }

            if (pageOptions.Page <= 0 || pageOptions.Size <= 0)
            {
                throw new ArgumentException("Invalid page options");
            }

            IEnumerable<ListingDTO> quoteListings = await GetQuoteListings();
            var listings = quoteListings
                .Where(listing => listing.VehicleType.MaxPassengers >= numberOfPassengers)
                .Select(listing => new ListingWithTotalPriceDTO
                {
                    Name = listing.Name,
                    PricePerPassenger = listing.PricePerPassenger,
                    TotalPrice = listing.PricePerPassenger * listing.VehicleType.MaxPassengers,
                    VehicleType = listing.VehicleType,
                })
                .OrderBy(listings => listings.TotalPrice)
                .Skip(pageOptions.Size * (pageOptions.Page - 1))
                .Take(pageOptions.Size)
                .ToList();

            return listings;
        }

        private async Task<IEnumerable<ListingDTO>> GetQuoteListings()
        {
            QuoteDTO quoteResponse = await GetQuote();
            var quoteListings = quoteResponse.Listings;
            return quoteListings;
        }

        private async Task<QuoteDTO> GetQuote()
        {
            var result = await _httpClient.GetAsync("QuoteRequest");
            var resultContent = await result.Content.ReadAsStringAsync();
            var quote = JsonConvert.DeserializeObject<QuoteDTO>(resultContent);
            return quote;
        }
    }
}
