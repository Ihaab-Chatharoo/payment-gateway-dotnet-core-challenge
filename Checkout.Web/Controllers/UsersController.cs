using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkout.Infrastructure;
using Checkout.Service;
using Microsoft.AspNetCore.Mvc;

namespace Checkout.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _userService.GetAllAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userService.GetByIdAsync(id.Value);
            if (user == null)
            {
                return NotFound();
            }

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

                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
    }
}
