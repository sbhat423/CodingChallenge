using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Services.Interface
{
    public interface ILocationService
    {
        public Task<string> GetCurrentCity(IPAddress ipAddress);
    }
}
