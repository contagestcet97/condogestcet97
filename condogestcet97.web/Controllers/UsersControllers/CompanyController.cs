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
            return View("~/Views/Users/Company/Index.cshtml", companies);
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

            return View("~/Views/Users/Company/Details.cshtml", company);
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
            return View("~/Views/Users/Company/Edit.cshtml", company);
        }

        // POST: Company/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address,Phone,FiscalNumber")] Company company)
        {
            if (id != company.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
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
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/Users/Company/Edit.cshtml", company);
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

            return View("~/Views/Users/Company/Delete.cshtml", company);
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
