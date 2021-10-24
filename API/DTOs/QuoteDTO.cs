using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class QuoteDTO
    {
        public string From { get; set; }
        public string To { get; set; }
        public IEnumerable<ListingDTO> Listings { get; set; }
    }
}
