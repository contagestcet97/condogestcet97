using AutoMapper;
using condogestcet97.web.Data.Entities.Users;
using condogestcet97.web.Data.Repositories.UserRepositories.Interfaces;
using condogestcet97.web.Data.ViewModels.User;
using condogestcet97.web.Data.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace condogestcet97.web.Controllers.UsersControllers
{
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;

        public UserController(
            IMapper mapper,
            UserManager<User> userManager,
            IUserRepository userRepository)
        {
            _mapper = mapper;
            _userManager = userManager;
            _userRepository = userRepository;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllAsync();
            return View("~/Views/Users/User/Index.cshtml", users);
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userRepository.GetByIdAsync(id.Value);
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

            var user = await _userRepository.GetByIdAsync(id.Value);
            if (user == null)
                return NotFound();

            // using AutoMapper to map User entity to UserEditViewModel
            var vm = _mapper.Map<UserEditViewModel>(user);

            return View("~/Views/Users/User/Edit.cshtml", vm);

        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserEditViewModel vm)
        {
            if (id != vm.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var userDb = await _userRepository.GetByIdAsync(id);
                if (userDb == null)
                    return NotFound();

                // using AutoMapper to map UserEditViewModel back to User entity
                _mapper.Map(vm, userDb);

                try
                {
                    await _userRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await UserExists(vm.Id))
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

            var user = await _userRepository.GetByIdAsync(id.Value);
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
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                _userRepository.Delete(user);
            }

            await _userRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> UserExists(int id)
        {
            var users = await _userRepository.GetAllAsync();
            return users.Any(u => u.Id == id);
        }
    }
}
