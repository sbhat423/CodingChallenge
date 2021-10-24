using API.Services;
using API.Services.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Services
{
    [TestClass]
    public class LocationServiceTests
    {
        private ILocationService _service;

        [TestInitialize]
        public void Setup()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddTransient(s => new HttpClient());
            services.AddTransient<ILocationService, LocationService>();
            var serviceProvider = services.BuildServiceProvider();
            _service = serviceProvider.GetService<ILocationService>();
        }

        [TestMethod]
        [DataRow("131.207.146.24", "Helsinki")]
        [DataRow("168.57.139.14", "Austin")]
        public async Task Should_Get_Current_City_Given_Ip(string ipString, string expected)
        {
            var ipAddress = IPAddress.Parse(ipString);
            var actual = await _service.GetCurrentCity(ipAddress);
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        [DataRow("251.203.98.243")]
        public async Task Should_Throw_Exception_For_Not_SUpported_Ip_Class(string ipString)
        {
            Exception expectedException = null;
            try
            {
                var ipAddress = IPAddress.Parse(ipString);
                await _service.GetCurrentCity(ipAddress);
            }
            catch (Exception ex)
            {
                expectedException = ex;
            }
            Assert.IsNotNull(expectedException);
        }
    }
}
