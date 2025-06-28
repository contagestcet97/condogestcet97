using condogestcet97.web.Data;
using condogestcet97.web.Data.Entities.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace condogestcet97.web.Controllers.UserControllers
{
    public class CompanyController : Controller
    {
        private readonly DataContextUser _context;

        public CompanyController(DataContextUser context)
        {
            _context = context;
        }

        // GET: Company
        public async Task<IActionResult> Index()
        {
            var dataContextUser = _context.UserCompanies.Include(u => u.Company).Include(u => u.User);
            return View("~/Views/Users/Company/Index.cshtml", await dataContextUser.ToListAsync());
        }

        // GET: Company/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userCompany = await _context.UserCompanies
                .Include(u => u.Company)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userCompany == null)
            {
                return NotFound();
            }

            return View("~/Views/Users/Company/Details.cshtml", userCompany);

        }

        // GET: Company/Create
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View("~/Views/Users/Company/Create.cshtml");
        }

        // POST: Company/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,CompanyId")] UserCompany userCompany)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userCompany);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", userCompany.CompanyId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userCompany.UserId);
            return View("~/Views/Users/Company/Create.cshtml", userCompany);
        }

        // GET: Company/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userCompany = await _context.UserCompanies.FindAsync(id);
            if (userCompany == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", userCompany.CompanyId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userCompany.UserId);
            return View("~/Views/Users/Company/Edit.cshtml", userCompany);
        }

        // POST: Company/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,CompanyId")] UserCompany userCompany)
        {
            if (id != userCompany.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userCompany);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserCompanyExists(userCompany.UserId))
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
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Id", userCompany.CompanyId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userCompany.UserId);
            return View("~/Views/Users/Company/Edit.cshtml", userCompany);
        }

        // GET: Company/Delete?userId=1&companyId=2
        public async Task<IActionResult> Delete(int? userId, int? companyId)
        {
            if (userId == null || companyId == null)
            {
                return NotFound();
            }

            var userCompany = await _context.UserCompanies
                .Include(u => u.Company)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserId == userId && m.CompanyId == companyId);
            if (userCompany == null)
            {
                return NotFound();
            }

            return View("~/Views/Users/Company/Delete.cshtml", userCompany);
        }

        // POST: Company/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int userId, int companyId)
        {
            var userCompany = await _context.UserCompanies.FindAsync(userId, companyId);
            if (userCompany != null)
            {
                _context.UserCompanies.Remove(userCompany);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool UserCompanyExists(int id)
        {
            return _context.UserCompanies.Any(e => e.UserId == id);
        }
    }
}
