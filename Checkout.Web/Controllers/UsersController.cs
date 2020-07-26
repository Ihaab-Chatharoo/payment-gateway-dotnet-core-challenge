using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkout.Infrastructure;
using Checkout.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Checkout.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;

            _logger = logger;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Lists of all previous payments");
            return View(await _userService.GetAllAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                _logger.LogError("The chosen payment ID does not exist in the database");
                return NotFound();
            }

            var user = await _userService.GetByIdAsync(id.Value);
            if (user == null)
            {
                _logger.LogError("The chosen payment ID does not exist in the database");
                return NotFound();
            }

            _logger.LogInformation("List of chosen payment details");
            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserDetails user)
        {
            if (ModelState.IsValid)
            {
                await _userService.AddAsync(user);
                _logger.LogInformation("New payment has been successfully processed through the payment gateway");
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
    }
}
