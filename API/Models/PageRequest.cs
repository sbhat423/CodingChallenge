using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class PageRequest
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public PageRequest(int page, int size)
        {
            Page = page;
            Size = size;
        }
    }
}
