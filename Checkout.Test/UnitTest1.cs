using Checkout.Infrastructure;
using Checkout.Service;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Checkout.Test
{
    public class Tests
    {
        private IUserService _userService;
        [SetUp]
        public void Setup()
        {
            var serviceProvider = Startup.ServiceProvider;
            if (serviceProvider != null)
            {
                _userService = serviceProvider.GetService<IUserService>();
            }
        }

        [Test]
        public async Task UserServiceAdd_TestAsync()
        {
            var user = new UserDetails
            {
                Id = Guid.NewGuid(),
                FirstName = "Ihaab",
                LastName = "Chatharoo",
                PhoneNumber = "59874563",
                Address = "Ollier",
                Country = "Mauritius",
                City = "Quatre Bornes",
                ZipCode = "123456",
                CardNumber = "4568789469325478",
                ExpiryDate = "08/25",
                Cvv = "123",
                Currency = "Rupees",
                Amount = 500
            };
            var actualResult = await _userService.AddAsync(user);
            var expectedResult = true;
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
