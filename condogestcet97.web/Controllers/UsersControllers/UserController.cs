using AutoMapper;
using condogestcet97.web.Data;
using condogestcet97.web.Data.Entities.Users;
using condogestcet97.web.Data.ViewModels.User;
using condogestcet97.web.Data.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace condogestcet97.web.Controllers.UsersControllers
{
    public class UserController : Controller
    {
        private readonly DataContextUser _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UserController(DataContextUser context, IMapper mapper, UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {

            return View("~/Views/Users/User/Index.cshtml", await _context.Users.ToListAsync());
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View("~/Views/Users/User/Details.cshtml", user);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View("~/Views/Users/User/Create.cshtml");
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Name = model.Name,
                    TwoFAEnabled = model.TwoFAEnabled,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                    FiscalNumber = model.FiscalNumber,
                    EmailConfirmed = false
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View("~/Views/Users/User/Create.cshtml", model);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            // using AutoMapper to map User entity to UserEditViewModel
            var vm = _mapper.Map<UserEditViewModel>(user);

            return View("~/Views/Users/User/Edit.cshtml", vm);

        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserEditViewModel vm)
        {
            if (id != vm.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var userDb = await _context.Users.FindAsync(id);
                if (userDb == null)
                    return NotFound();

                // using AutoMapper to map UserEditViewModel back to User entity
                _mapper.Map(vm, userDb);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(vm.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        return StatusCode(500, "An error occurred while updating the user. Please try again later.");
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View("~/Views/Users/User/Edit.cshtml", vm);
        }


        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View("~/Views/Users/User/Delete.cshtml", user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
