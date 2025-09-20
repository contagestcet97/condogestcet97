using AutoMapper;
using condogestcet97.web.Data.Entities.Users;
using condogestcet97.web.Data.Repositories.UserRepositories.Interfaces;
using condogestcet97.web.Data.ViewModels.User;
using condogestcet97.web.Data.ViewModels.UserViewModels;
using condogestcet97.web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace condogestcet97.web.Controllers.UsersControllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IEmailServices _emailServices;

        public UserController(
            IMapper mapper,
            UserManager<User> userManager,
            IUserRepository userRepository,
            ICompanyRepository companyRepository,
            IEmailServices emailServices)
        {
            _mapper = mapper;
            _userManager = userManager;
            _userRepository = userRepository;
            _companyRepository = companyRepository;
            _emailServices = emailServices;
        }

        // GET: User
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllAsync();
            var vmList = _mapper.Map<List<UserListViewModel>>(users);
            return View("~/Views/Users/User/Index.cshtml", vmList);
        }

        [Authorize]
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

            var vm = _mapper.Map<UserDetailsViewModel>(user);
            return View("~/Views/Users/User/Details.cshtml", vm);
        }

        // GET: User/Create
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            var model = new UserCreateViewModel
            {
                AllCompanies = (await _companyRepository.GetAllAsync()).ToList()
            };
            return View("~/Views/Users/User/Create.cshtml", model);
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(UserCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<User>(model);
                user.UserName = model.Email; // UserManager needs the UserName to be set

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // send confirmation email
                    await _emailServices.SendEmailAsync(
                       user.Email,
                       "Welcome to the system",
                       $"Hello {user.Name},<br>Your account has been created successfully."
                   );

                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            model.AllCompanies = (await _companyRepository.GetAllAsync()).ToList();
            return View("~/Views/Users/User/Create.cshtml", model);
        }

        // GET: User/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var user = await _userRepository.GetByIdAsync(id.Value);
            if (user == null)
                return NotFound();

            // using AutoMapper to map User entity to UserEditViewModel
            var vm = _mapper.Map<UserEditViewModel>(user);
            // fetching all companies to populate the dropdown in the view
            vm.AllCompanies = (await _companyRepository.GetAllAsync()).ToList();
            vm.SelectedCompanyIds = user.UserCompanies.Select(uc => uc.CompanyId).ToList();

            return View("~/Views/Users/User/Edit.cshtml", vm);

        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
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
                    await _userRepository.AssignCompaniesAsync(id, vm.SelectedCompanyIds);
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
            vm.AllCompanies = (await _companyRepository.GetAllAsync()).ToList();
            return View("~/Views/Users/User/Edit.cshtml", vm);
        }


        // GET: User/Delete/5
        [Authorize(Roles = "Admin")]
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

            var vm = _mapper.Map<UserDeleteViewModel>(user);
            return View("~/Views/Users/User/Delete.cshtml", vm);
        }

        // POST: User/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                //sending the user an email when deleting their account
                await _emailServices.SendEmailAsync(
                    user.Email,
                    "Account Deletion Notification",
                    $"Hello {user.Name},<br>Your account is deleted. If this was a mistake, please contact support."
                );
                _userRepository.Delete(user);
            }

            await _userRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // GET: User/AssignCompanies/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignCompanies(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return NotFound();

            var allCompanies = await _companyRepository.GetAllAsync();
            var selectedCompanyIds = user.UserCompanies.Select(uc => uc.CompanyId).ToList();

            var vm = new UserCompanyAssignmentViewModel
            {
                UserId = id,
                AllCompanies = allCompanies.ToList(),
                SelectedCompanyIds = selectedCompanyIds
            };

            return View(vm);
        }

        // POST: User/AssignCompanies/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignCompanies(UserCompanyAssignmentViewModel vm)
        {
            await _userRepository.AssignCompaniesAsync(vm.UserId, vm.SelectedCompanyIds);
            return RedirectToAction(nameof(Index));
        }

        // GET: User/AssignManagedCompanies/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignManagedCompanies(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return NotFound();

            var allCompanies = await _companyRepository.GetAllAsync();
            var managedCompanyIds = user.ManagedCompanies.Select(ucm => ucm.CompanyId).ToList();

            var vm = new UserManagedCompanyAssignmentViewModel
            {
                UserId = id,
                AllCompanies = allCompanies.ToList(),
                SelectedManagedCompanyIds = managedCompanyIds
            };

            return View(vm);
        }

        // POST: User/AssignManagedCompanies/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignManagedCompanies(UserManagedCompanyAssignmentViewModel vm)
        {
            await _userRepository.AssignManagedCompaniesAsync(vm.UserId, vm.SelectedManagedCompanyIds);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        private async Task<bool> UserExists(int id)
        {
            var users = await _userRepository.GetAllAsync();
            return users.Any(u => u.Id == id);
        }

        [Authorize]
        public IActionResult MyProfile()
        {
            var userIdString = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                return Unauthorized();
            }
            return RedirectToAction("Details", new { id = userId });
        }
    }
}
