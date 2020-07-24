using Checkout.Repository.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkout.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly GatewayDBContext _context;
        public UserRepository(GatewayDBContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(TblUser user)
        {
            try
            {
                await _context.TblUser.AddAsync(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<TblUser>> GetAll()
        {
            var result = await _context.TblUser.ToListAsync();
            return result;
        }

        public async Task<TblUser> GetById(Guid id)
        {
            var result = await _context.TblUser.Where(e => e.Id == id).FirstOrDefaultAsync();
            return result;
        }
    }
}
