using API.Models;
using API.Services;
using API.Services.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Services
{
    [TestClass]
    public class ListingServiceTests
    {
        private IListingService _service;

        [TestInitialize]
        public void Setup()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddTransient(s => new HttpClient());
            services.AddTransient<IListingService, ListingService>();
            var serviceProvider = services.BuildServiceProvider();
            _service = serviceProvider.GetService<IListingService>();
        }


        [TestMethod]
        [DataRow(1)]
        [DataRow(5)]
        public async Task Should_Get_Results_For_Given_Number_Of_Passengers(int numberOfPassengers)
        {
            var actual = await _service.GetListingsWithTotalPrice(numberOfPassengers, new PageRequest(1, 10));
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        public async Task Should_Throw_Exception_For_Invalid_Number_Of_Passengers(int numberOfPassengers)
        {
            Exception expectedException = null;
            try
            {
                await _service.GetListingsWithTotalPrice(numberOfPassengers, new PageRequest(1, 10));
            }
            catch (Exception ex) 
            {
                expectedException = ex;
            }
            Assert.IsNotNull(expectedException);
        }
    }
}
