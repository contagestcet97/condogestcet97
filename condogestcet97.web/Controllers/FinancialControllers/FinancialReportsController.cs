using condogestcet97.web.Data;
using condogestcet97.web.Data.Entities.Financial;
using condogestcet97.web.Data.FinancialRepositories.IFinancialRepositories;
using condogestcet97.web.Helpers;
using condogestcet97.web.Helpers.IHelpers;
using condogestcet97.web.Models;
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
    public class FinancialReportsController : Controller
    {
        private readonly IFinancialReportRepository _financialReportRepository;
        private readonly IFinancialConverterHelper _converterHelper;

        public FinancialReportsController(IFinancialReportRepository FinancialReportRepository, IFinancialConverterHelper converterHelper)
        {
            _financialReportRepository = FinancialReportRepository;
            _converterHelper = converterHelper;
        }

        // GET: FinancialReports
        public async Task<IActionResult> Index()
        {
            return View(_financialReportRepository.GetAll());
        }

        // GET: FinancialReports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("FinancialReportNotFound");
            }

            var FinancialReport = await _financialReportRepository.GetByIdAsync(id.Value);


            if (FinancialReport == null)
            {
                return new NotFoundViewResult("FinancialReportNotFound");
            }

            return View(FinancialReport);
        }


        // GET: FinancialReports/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FinancialReports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FinancialReportViewModel model)
        {
            var FinancialReport = _converterHelper.ToFinancialReport(model, true);

            try
            {
                await _financialReportRepository.CreateAsync(FinancialReport);


                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            return View(model);
        }

        // GET: FinancialReports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("FinancialReportNotFound");
            }

            var FinancialReport = await _financialReportRepository.GetByIdAsync(id.Value);

            if (FinancialReport == null)
            {
                return new NotFoundViewResult("FinancialReportNotFound");
            }

            var model = _converterHelper.ToFinancialReportViewModel(FinancialReport);

            return View(model);
        }

        // POST: FinancialReports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(FinancialReportViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var FinancialReport = _converterHelper.ToFinancialReport(model, false);

                    await _financialReportRepository.UpdateAsync(FinancialReport);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _financialReportRepository.ExistAsync(model.Id))
                    {
                        return new NotFoundViewResult("FinancialReportNotFound");
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

        // GET: FinancialReports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("FinancialReportNotFound");
            }

            var FinancialReport = await _financialReportRepository.GetByIdAsync(id.Value);


            if (FinancialReport == null)
            {
                return new NotFoundViewResult("FinancialReportNotFound");
            }

            return View(FinancialReport);
        }

        // POST: FinancialReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var FinancialReport = await _financialReportRepository.GetByIdAsync(id);

            try
            {
                await _financialReportRepository.DeleteAsync(FinancialReport);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {

                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    var errorModel = new ErrorViewModel
                    {
                        ErrorTitle = $"{FinancialReport.Id} provavelmente está a ser usado!!",
                        ErrorMessage = $"{FinancialReport.Id} não pode ser apagado",
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

        public IActionResult FinancialReportNotFound()
        {
            return View();
        }
    }
}
