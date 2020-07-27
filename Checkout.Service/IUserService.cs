using Checkout.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Checkout.Service
{
    public interface IUserService
    {
        Task<bool> AddAsync(UserDetails userDetails);
        Task<List<UserDetails>> GetAllAsync();
        Task<UserDetails> GetByIdAsync(Guid id);
    }
}
