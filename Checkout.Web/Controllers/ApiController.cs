using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Checkout.Repository.DB;

namespace Checkout.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly GatewayDBContext _context;

        public ApiController(GatewayDBContext context)
        {
            _context = context;
        }

        // GET: api/Api
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblUser>>> GetTblUser()
        {
            return await _context.TblUser.ToListAsync();
        }

        // GET: api/Api/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblUser>> GetTblUser(Guid id)
        {
            var tblUser = await _context.TblUser.FindAsync(id);

            if (tblUser == null)
            {
                return NotFound();
            }

            return tblUser;
        }

        // POST: api/Api
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TblUser>> PostTblUser(TblUser tblUser)
        {
            if (tblUser.CardNumber == "4568789469325478" && tblUser.ExpiryDate == "08/25" && tblUser.Cvv == "123")
            {
                _context.TblUser.Add(tblUser);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (TblUserExists(tblUser.Id))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }

                return CreatedAtAction("GetTblUser", new { id = tblUser.Id }, tblUser);
            }
            else
            {
                return BadRequest(new { message = "Card details entered are incorrect" });
            }
            
        }

        private bool TblUserExists(Guid id)
        {
            return _context.TblUser.Any(e => e.Id == id);
        }
    }
}
