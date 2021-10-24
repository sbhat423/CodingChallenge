using API.DTOs;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services.Interface
{
    public interface IListingService
    {
        Task<IEnumerable<ListingWithTotalPriceDTO>> GetListingsWithTotalPrice(int numberOfPassengers, PageRequest pageOptions);
    }
}
