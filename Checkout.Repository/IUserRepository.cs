using Checkout.Repository.DB;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Repository
{
    public interface IUserRepository
    {
        Task<bool> Add(TblUser user);
        Task<List<TblUser>> GetAll();
        Task<TblUser> GetById(Guid id);
    }
}
