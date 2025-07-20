using AutoMapper;
using condogestcet97.web.Data.Entities.Users;
using condogestcet97.web.Data.Repositories.UserRepositories.Interfaces;
using condogestcet97.web.Data.ViewModels.RoleViewModels;
using Microsoft.AspNetCore.Mvc;

namespace condogestcet97.web.Controllers.UsersControllers
{
    public class RoleController : Controller
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleController(
            IRoleRepository roleRepository,
            IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        // GET: Role
        public async Task<IActionResult> Index()
        {
            var role = await _roleRepository.GetAllAsync();
            var vmList = _mapper.Map<List<RoleListViewModel>>(role);
            return View("~/Views/Users/Role/Index.cshtml", vmList);
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

            var vm = _mapper.Map<RoleDetailsViewModel>(role);
            return View("~/Views/Users/Role/Details.cshtml", vm);
        }

        // GET: Role/Create
        public IActionResult Create()
        {
            return View("~/Views/Users/Role/Create.cshtml", new RoleCreateViewModel());
        }

        // POST: Role/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = _mapper.Map<Role>(model);
                await _roleRepository.AddAsync(role);
                await _roleRepository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/Users/Role/Create.cshtml", model);
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

            var vm = _mapper.Map<RoleEditViewModel>(role);
            return View("~/Views/Users/Role/Edit.cshtml", vm);
        }

        // POST: Role/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RoleEditViewModel vm)
        {
            if (id != vm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var role = await _roleRepository.GetByIdAsync(id);
                if (role == null)
                {
                    return NotFound();
                }
                //mapping the view model to the entity
                _mapper.Map(vm, role);
                await _roleRepository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/Users/Role/Edit.cshtml", vm);
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

            var vm = _mapper.Map<RoleDeleteViewModel>(role);
            return View("~/Views/Users/Role/Delete.cshtml", vm);
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
