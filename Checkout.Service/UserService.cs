using Checkout.Infrastructure;
using Checkout.Repository;
using Checkout.Repository.DB;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Checkout.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        private readonly ILogger<UserService> _logger;
        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository;

            _logger = logger;
        }

        // Creating a new payment through the payment gateway
        public async Task<bool> AddAsync(UserDetails userDetails)
        {
            try
            {
                var obj = new TblUser();
                // If statement to accept only these card details to test the bank part of the API and accepts only Rupees or Dollars as payment currency
                if ((obj.CardNumber == "4568789469325478" && obj.ExpiryDate == "08/25" && obj.Cvv == "123") && (obj.Currency == "Rupees" || obj.Currency == "Dollars"))
                {
                    // Adds a maximum amount that can be processed using the card based on the 2 different currencies used (acts as the amount of money available on the bank account)
                    if ((obj.Currency == "Rupees" && obj.Amount <= 999) || (obj.Currency == "Dollars" && obj.Amount <= 500))
                    {
                        obj.Id = Guid.NewGuid();
                        obj.FirstName = userDetails.FirstName;
                        obj.LastName = userDetails.LastName;
                        obj.PhoneNumber = userDetails.PhoneNumber;
                        obj.Address = userDetails.Address;
                        obj.Country = userDetails.Country;
                        obj.City = userDetails.City;
                        obj.ZipCode = userDetails.ZipCode;
                        obj.CardNumber = userDetails.CardNumber;
                        obj.ExpiryDate = userDetails.ExpiryDate;
                        obj.Cvv = userDetails.Cvv;
                        obj.Currency = userDetails.Currency;
                        obj.Amount = userDetails.Amount;
                        var result = await _userRepository.Add(obj);

                        _logger.LogInformation("New payment has been successfully processed through the payment gateway");
                        return result;
                    }
                    else
                    {
                        _logger.LogWarning("Transaction could not be processed! There are insufficient funds in the bank account.");
                        return false;
                    }
                }
                else
                {
                    _logger.LogWarning("Card details entered are incorrect");
                    return false;
                }

            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.ToString());
                return false;
            }
        }

        // Displays all the payment processed through the payment gateway in a list
        public async Task<List<UserDetails>> GetAllAsync()
        {
            var userList = new List<UserDetails>();
            var result = await _userRepository.GetAll();
            foreach (var item in result)
            {
                userList.Add(new UserDetails
                {
                    Id = item.Id,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    PhoneNumber = item.PhoneNumber,
                    Address = item.Address,
                    Country = item.Country,
                    City = item.City,
                    ZipCode = item.ZipCode,
                    CardNumber = item.CardNumber,
                    ExpiryDate = item.ExpiryDate,
                    Cvv = item.Cvv,
                    Currency = item.Currency,
                    Amount = item.Amount
                });
            }

            _logger.LogInformation("Lists of all previous payments");
            return userList;
        }

        // Displays the result of the chosen payment details
        public async Task<UserDetails> GetByIdAsync(Guid id)
        {
            var result = await _userRepository.GetById(id);
            var userDetails = new UserDetails();
            userDetails.Id = result.Id;
            userDetails.FirstName = result.FirstName;
            userDetails.LastName = result.LastName;
            userDetails.PhoneNumber = result.PhoneNumber;
            userDetails.Address = result.Address;
            userDetails.Country = result.Country;
            userDetails.City = result.City;
            userDetails.ZipCode = result.ZipCode;
            userDetails.CardNumber = result.CardNumber;
            userDetails.ExpiryDate = result.ExpiryDate;
            userDetails.Cvv = result.Cvv;
            userDetails.Currency = result.Currency;
            userDetails.Amount = result.Amount;

            _logger.LogInformation("List of chosen payment details");
            return userDetails;
        }
    }
}
