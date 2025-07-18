using condogestcet97.web.Data.Entities.Users;
using condogestcet97.web.Data.Repositories.UserRepositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace condogestcet97.web.Controllers.UsersControllers
{
    public class RoleController : Controller
    {
        private readonly IRoleRepository _roleRepository;

        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        // GET: Role
        public async Task<IActionResult> Index()
        {
            var role = await _roleRepository.GetAllAsync();
            return View("~/Views/Users/Role/Index.cshtml", role);
        }

        // GET: Role/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _roleRepository.GetByIdAsync(id.Value);
            if (role == null)
            {
                return NotFound();
            }

            return View("~/Views/Users/Role/Details.cshtml", role);
        }

        // GET: Role/Create
        public IActionResult Create()
        {
            return View("~/Views/Users/Role/Create.cshtml");
        }

        // POST: Role/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] Role role)
        {
            if (ModelState.IsValid)
            {
                await _roleRepository.AddAsync(role);
                await _roleRepository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/Users/Role/Create.cshtml", role);
        }

        // GET: Role/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _roleRepository.GetByIdAsync(id.Value);
            if (role == null)
            {
                return NotFound();
            }
            return View("~/Views/Users/Role/Edit.cshtml", role);
        }

        // POST: Role/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name")] Role role)
        {
            var roleToUpdate = await _roleRepository.GetByIdAsync(id);
            if (roleToUpdate == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                roleToUpdate.Name = role.Name;
                await _roleRepository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/Users/Role/Edit.cshtml", roleToUpdate);
        }


        // GET: Role/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _roleRepository.GetByIdAsync(id.Value);
            if (role == null)
            {
                return NotFound();
            }

            return View("~/Views/Users/Role/Delete.cshtml", role);
        }

        // POST: Role/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role != null)
            {
                _roleRepository.Delete(role);
            }

            await _roleRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoleExists(int id)
        {
            return _roleRepository.GetByIdAsync(id) != null;
        }
    }
}
