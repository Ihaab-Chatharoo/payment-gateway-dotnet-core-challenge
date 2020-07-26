using Checkout.Infrastructure;
using Checkout.Repository;
using Checkout.Repository.DB;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Checkout.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> AddAsync(UserDetails userDetails)
        {
            try
            {
                var obj = new TblUser();
                if(obj.CardNumber == "4568789469325478" && obj.ExpiryDate == "08/25" && obj.Cvv == "123")
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
                    return result;
                }
                else
                {
                    return false;
                }
                
            }
            catch (Exception ex)
            {

                return false;
            }
        }

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
            return userList;
        }

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
            return userDetails;
        }
    }
}
