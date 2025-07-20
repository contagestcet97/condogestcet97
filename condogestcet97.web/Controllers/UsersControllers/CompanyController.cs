using condogestcet97.web.Data.Entities.Users;
using condogestcet97.web.Data.Repositories.UserRepositories.Interfaces;
using condogestcet97.web.Data.ViewModels.CompanyViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace condogestcet97.web.Controllers.UsersControllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyController(ICompanyRepository company)
        {
            _companyRepository = company;
        }

        // GET: Company
        //lists all companies
        public async Task<IActionResult> Index()
        {
            var companies = await _companyRepository.GetAllAsync();

            var vmList = companies.Select(c => new CompanyListViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Address = c.Address,
                Phone = c.Phone,
                FiscalNumber = c.FiscalNumber
            }).ToList();

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

            // Mapping the entity to the view model
            var vm = new CompanyDetailsViewModel
            {
                Id = company.Id,
                Name = company.Name,
                Address = company.Address,
                Phone = company.Phone,
                FiscalNumber = company.FiscalNumber
            };

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
                // mapping the view model to the entity
                var company = new Company
                {
                    Name = model.Name,
                    Address = model.Address,
                    Phone = model.Phone,
                    FiscalNumber = model.FiscalNumber
                };

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

            // Mapping the entity to the view model - TODO use AutoMapper for this to avoid manual mapping
            var vm = new CompanyEditViewModel
            {
                Id = company.Id,
                Name = company.Name,
                Address = company.Address,
                Phone = company.Phone,
                FiscalNumber = company.FiscalNumber
            };

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

                // map viewModel back to entity - TODO use AutoMapper for this to avoid manual mapping
                company.Name = vm.Name;
                company.Address = vm.Address;
                company.Phone = vm.Phone;
                company.FiscalNumber = vm.FiscalNumber;

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

            // using the companydetails view model for deletion confirmation
            var vm = new CompanyDetailsViewModel
            {
                Id = company.Id,
                Name = company.Name,
                Address = company.Address,
                Phone = company.Phone,
                FiscalNumber = company.FiscalNumber
            };

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
            }

            await _companyRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyExists(int id)
        {
            return _companyRepository.GetByIdAsync(id) != null;
        }
    }
}
