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
    public class ServicesController : Controller
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IFinancialConverterHelper _converterHelper;

        public ServicesController(IServiceRepository serviceRepository, IFinancialConverterHelper converterHelper)
        {
            _serviceRepository = serviceRepository;
            _converterHelper = converterHelper;
        }

        // GET: Services
        public async Task<IActionResult> Index()
        {
            return View(_serviceRepository.GetAll());
        }

        // GET: Services/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ServiceNotFound");
            }

            var service = await _serviceRepository.GetByIdAsync(id.Value);


            if (service == null)
            {
                return new NotFoundViewResult("ServiceNotFound");
            }

            return View(service);
        }


        // GET: Services/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceViewModel model)
        {
            var service = _converterHelper.ToService(model, true);

            try
            {
                await _serviceRepository.CreateAsync(service);


                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            return View(model);
        }

        // GET: Services/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ServiceNotFound");
            }

            var service = await _serviceRepository.GetByIdAsync(id.Value);

            if (service == null)
            {
                return new NotFoundViewResult("ServiceNotFound");
            }

            var model = _converterHelper.ToServiceViewModel(service);

            return View(model);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ServiceViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var service = _converterHelper.ToService(model, false);

                    await _serviceRepository.UpdateAsync(service);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _serviceRepository.ExistAsync(model.Id))
                    {
                        return new NotFoundViewResult("ServiceNotFound");
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

        // GET: Services/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ServiceNotFound");
            }

            var service = await _serviceRepository.GetByIdAsync(id.Value);


            if (service == null)
            {
                return new NotFoundViewResult("ServiceNotFound");
            }

            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var service = await _serviceRepository.GetByIdAsync(id);

            try
            {
                await _serviceRepository.DeleteAsync(service);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {

                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    var errorModel = new ErrorViewModel
                    {
                        ErrorTitle = $"{service.Id} provavelmente está a ser usado!!",
                        ErrorMessage = $"{service.Id} não pode ser apagado",
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

        public IActionResult serviceNotFound()
        {
            return View();
        }
    }
}
