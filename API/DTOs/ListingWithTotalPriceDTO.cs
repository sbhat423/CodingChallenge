using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class ListingWithTotalPriceDTO
    {
        public string Name { get; set; }
        public decimal PricePerPassenger { get; set; }
        public decimal TotalPrice { get; set; }
        public VehicleTypeDTO VehicleType { get; set; }
    }
}
