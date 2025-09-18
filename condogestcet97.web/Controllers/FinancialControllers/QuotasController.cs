using condogestcet97.web.Data;
using condogestcet97.web.Data.CondominiumRepositories.ICondominiumRepositories;
using condogestcet97.web.Data.Entities.Financial;
using condogestcet97.web.Data.FinancialRepositories.IFinancialRepositories;
using condogestcet97.web.Data.Repositories.IRepositories;
using condogestcet97.web.Helpers;
using condogestcet97.web.Helpers.IHelpers;
using condogestcet97.web.Models;
using condogestcet97.web.Services.FinancialServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace condogestcet97.web.Controllers.FinancialControllers
{
    public class QuotasController : Controller
    {
        private readonly IQuotaRepository _quotaRepository;
        private readonly ICondoRepository _condoRepository;
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IQuotaService _quotaService;
        private readonly IFinancialConverterHelper _converterHelper;

        public QuotasController(IQuotaRepository quotaRepository, ICondoRepository condoRepository, IFinancialConverterHelper converterHelper, IQuotaService quotaService, IApartmentRepository apartmentRepository)
        {
            _quotaRepository = quotaRepository;
            _converterHelper = converterHelper;
            _condoRepository = condoRepository;
            _apartmentRepository = apartmentRepository;
            _quotaService = quotaService;
        }

        // GET: Quotas
        public async Task<IActionResult> Index()
        {
            return View(_quotaRepository.GetAll());
        }

        // GET: quotas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("QuotaNotFound");
            }

            var quota = await _quotaRepository.GetByIdAsync(id.Value);


            if (quota == null)
            {
                return new NotFoundViewResult("QuotaNotFound");
            }

            return View(quota);
        }


        // GET: quotas/Create
        public IActionResult Create()
        {
            var model = new QuotaViewModel
            {
                DueDate = DateTime.Now.Date,

                PaidDate = null,

                Condos = _condoRepository.GetAll().Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = $"{c.Address}"
                })
            };
            

            return View(model);
        }

        // POST: quotas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(QuotaViewModel model)
        {
            var quota = _converterHelper.ToQuota(model, true);

            quota.ApartmentsCount = await _apartmentRepository.GetApartmentsCount(quota.CondoId);

            try
            {
                await _quotaRepository.CreateAsync(quota);

                //await _quotaService.CreateQuotaInvoices(quota);

                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            return View(model);
        }

        // GET: quotas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("QuotaNotFound");
            }

            var quota = await _quotaRepository.GetByIdAsync(id.Value);

            if (quota == null)
            {
                return new NotFoundViewResult("QuotaNotFound");
            }

            var model = _converterHelper.ToQuotaViewModel(quota);

            model.Condos = _condoRepository.GetAll().Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = $"{c.Address}"
            });

            return View(model);
        }

        // POST: quotas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(QuotaViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var quota = _converterHelper.ToQuota(model, false);

                    await _quotaRepository.UpdateAsync(quota);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _quotaRepository.ExistAsync(model.Id))
                    {
                        return new NotFoundViewResult("QuotaNotFound");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: quotas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("QuotaNotFound");
            }

            var quota = await _quotaRepository.GetByIdAsync(id.Value);


            if (quota == null)
            {
                return new NotFoundViewResult("QuotaNotFound");
            }

            return View(quota);
        }

        // POST: quotas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quota = await _quotaRepository.GetByIdAsync(id);

            try
            {
                await _quotaRepository.DeleteAsync(quota);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {

                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    var errorModel = new ErrorViewModel
                    {
                        ErrorTitle = $"{quota.Id} provavelmente está a ser usado!!",
                        ErrorMessage = $"{quota.Id} não pode ser apagado",
                    };

                    return View("Error", errorModel);
                }


                return View("Error", new ErrorViewModel
                {
                    ErrorTitle = "Erro de base de dados",
                    ErrorMessage = "Ocorreu um erro inesperado ao tentar apagar o apartamento."
                });

            }
        }

        public async Task<IActionResult> GenerateInvoice(int id)
        {
            var quota = await _quotaRepository.GetByIdAsync(id);

            quota.ApartmentsCount = await _apartmentRepository.GetApartmentsCount(quota.CondoId);

            try
            {
                await _quotaService.CreateQuotaInvoices(quota);
                await _quotaRepository.UpdateAsync(quota);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _quotaRepository.ExistAsync(quota.Id))
                {
                    return new NotFoundViewResult("QuotaNotFound");
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));

        }


        public IActionResult QuotaNotFound()
        {
            return View();
        }
    }
}
