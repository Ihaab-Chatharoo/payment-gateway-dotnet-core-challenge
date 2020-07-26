using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Checkout.Repository.DB;
using Microsoft.Extensions.Logging;

namespace Checkout.Web.Controllers
{
    // ApiController to be used with Postman to get or post results of the Web Api
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly GatewayDBContext _context;

        private readonly ILogger<ApiController> _logger;

        public ApiController(GatewayDBContext context, ILogger<ApiController> logger)
        {
            _context = context;

            _logger = logger;
        }

        // Displays the list of all previous payments processed through the payment gateway
        // GET: api/Api
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblUser>>> GetTblUser()
        {
            _logger.LogInformation("Lists of all previous payments");
            return await _context.TblUser.ToListAsync();
        }

        // Displays the list of a specific previous payment processed through the payment gateway based on the ID inserted
        // GET: api/Api/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblUser>> GetTblUser(Guid id)
        {
            var tblUser = await _context.TblUser.FindAsync(id);

            if (tblUser == null)
            {
                _logger.LogError("The chosen payment ID does not exist in the database");
                return NotFound();
            }

            _logger.LogInformation("List of chosen payment details");
            return tblUser;
        }

        // Process a payment through the payment gateway
        // POST: api/Api
        [HttpPost]
        public async Task<ActionResult<TblUser>> PostTblUser(TblUser tblUser)
        {
            // If statement to accept only these card details to test the bank part of the API and accepts only Rupees or Dollars as payment currency
            if ((tblUser.CardNumber == "4568789469325478" && tblUser.ExpiryDate == "08/25" && tblUser.Cvv == "123") && (tblUser.Currency == "Rupees" || tblUser.Currency == "Dollars"))
            {
                // Adds a maximum amount that can be processed using the card based on the 2 different currencies used (acts as the amount of money available on the bank account)
                if ((tblUser.Currency == "Rupees" && tblUser.Amount <= 999) || (tblUser.Currency == "Dollars" && tblUser.Amount <= 500))
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
                            _logger.LogWarning("Tried to create duplicate payment as payment ID already exists");
                            return Conflict();
                        }
                        else
                        {
                            throw;
                        }
                    }

                    _logger.LogInformation("New payment has been successfully processed through the payment gateway");
                    return CreatedAtAction("GetTblUser", new { id = tblUser.Id }, tblUser);
                }
                else
                {
                    _logger.LogWarning("Transaction could not be processed! There are insufficient funds in the bank account.");
                    return BadRequest(new { message = "Transaction could not be processed! There are insufficient funds in the bank account." });
                }
            }
            else
            {
                _logger.LogWarning("Card details entered are incorrect");
                return BadRequest(new { message = "Card details entered are incorrect" });
            }
            
        }

        // Checks if a duplicate payment is not being processed based on the ID
        private bool TblUserExists(Guid id)
        {
            return _context.TblUser.Any(e => e.Id == id);
        }
    }
}
