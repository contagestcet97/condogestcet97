using AutoMapper;
using condogestcet97.web.Data.Entities.Users;
using condogestcet97.web.Data.Repositories.UserRepositories.Interfaces;
using condogestcet97.web.Data.ViewModels.CompanyViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace condogestcet97.web.Controllers.UsersControllers
{
    [Authorize(Roles = "Admin, Employee")]
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyController(
            ICompanyRepository company,
            IMapper mapper)
        {
            _companyRepository = company;
            _mapper = mapper;
        }

        // GET: Company
        //lists all companies
        public async Task<IActionResult> Index()
        {
            var companies = await _companyRepository.GetAllAsync();
            // using AutoMapper to map the list of companies to a list of view models. Refer to Services/CompanyProfile.cs for the mapping configuration.
            var vmList = _mapper.Map<List<CompanyListViewModel>>(companies);
            return View("~/Views/Users/Company/Index.cshtml", vmList);
        }

        // GET: Company/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _companyRepository.GetByIdAsync(id.Value);
            if (company == null)
            {
                return NotFound();
            }

            var vm = _mapper.Map<CompanyDetailsViewModel>(company);

            return View("~/Views/Users/Company/Details.cshtml", vm);
        }

        // GET: Company/Create
        public IActionResult Create()
        {
            return View("~/Views/Users/Company/Create.cshtml", new CompanyCreateViewModel());
        }

        // POST: Company/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompanyCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var company = _mapper.Map<Company>(model);
                await _companyRepository.AddAsync(company);
                await _companyRepository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/Users/Company/Create.cshtml", model);
        }

        // GET: Company/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _companyRepository.GetByIdAsync(id.Value);
            if (company == null)
            {
                return NotFound();
            }

            var vm = _mapper.Map<CompanyEditViewModel>(company);
            return View("~/Views/Users/Company/Edit.cshtml", vm);
        }

        // POST: Company/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CompanyEditViewModel vm)
        {
            if (id != vm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var company = await _companyRepository.GetByIdAsync(id);
                if (company == null)
                {
                    return NotFound();
                }

                _mapper.Map(vm, company);

                try
                {
                    _companyRepository.Update(company);
                    await _companyRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyExists(company.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        return StatusCode(500, "An error occurred while updating the company. Please try again later.");
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/Users/Company/Edit.cshtml", vm);
        }

        // GET: Company/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = await _companyRepository.GetByIdAsync(id.Value);
            if (company == null)
            {
                return NotFound();
            }

            var vm = _mapper.Map<CompanyDeleteViewModel>(company);

            return View("~/Views/Users/Company/Delete.cshtml", vm);
        }

        // POST: Company/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var company = await _companyRepository.GetByIdAsync(id);
            if (company != null)
            {
                _companyRepository.Delete(company);
                await _companyRepository.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyExists(int id)
        {
            return _companyRepository.GetByIdAsync(id) != null;
        }
    }
}
