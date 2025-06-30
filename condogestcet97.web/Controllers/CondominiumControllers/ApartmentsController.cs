using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using condogestcet97.web.Data;
using condogestcet97.web.Data.Entities.Condominium;
using condogestcet97.web.Data.CondominiumRepositories.ICondominiumRepositories;
using condogestcet97.web.Helpers;
using System.Diagnostics;
using condogestcet97.web.Models;
using condogestcet97.web.Helpers.IHelpers;
using condogestcet97.web.Data.Repositories.IRepositories;

namespace condogestcet97.web.Controllers.CondominiumControllers
{
    public class ApartmentsController : Controller
    {
        private readonly DataContextCondominium _context;
        private readonly IApartmentRepository _apartmentRepository;
        private readonly ICondiminiumsConverterHelper _converterHelper;
        private readonly ICondoRepository _condoRepository;

        public ApartmentsController(DataContextCondominium context,
            IApartmentRepository apartmentRepository,
            ICondiminiumsConverterHelper converterHelper,
            ICondoRepository condoRepository)
        {
            _context = context;
            _apartmentRepository = apartmentRepository;
            _converterHelper = converterHelper;
            _condoRepository = condoRepository;
        }

        // GET: Apartments
        public async Task<IActionResult> Index()
        {
            return View(_apartmentRepository.GetAll());
        }

        // GET: Apartments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ApartmentNotFound");
            }

            var apartment = await _apartmentRepository.GetByIdAsync(id.Value);


            if (apartment == null)
            {
                return new NotFoundViewResult("ApartmentNotFound");
            }

            return View(apartment);
        }
        

        // GET: Apartments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Apartments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApartmentViewModel model)
        {

            if (ModelState.IsValid)
            {
                var condo = await _condoRepository.GetByIdAsync(model.CondoId);

                if (condo != null)
                {

                    var apartment = _converterHelper.ToApartment(model, true, condo);

                    try
                    {
                        await _apartmentRepository.CreateAsync(apartment);


                        return RedirectToAction(nameof(Index));

                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.ToString());
                    }
                }
            }

            return View(model);
        }

        // GET: Apartments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ApartmentNotFound");
            }

            var apartment = await _apartmentRepository.GetByIdAsync(id.Value);

            if (apartment == null)
            {
                return new NotFoundViewResult("ApartmentNotFound");
            }

            var model = _converterHelper.ToApartmentViewModel(apartment);

            return View(model);
        }

        // POST: Apartments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ApartmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var condo = await _condoRepository.GetByIdAsync(model.CondoId);

                if (condo != null)
                {
                    try
                    {
                        var apartment = _converterHelper.ToApartment(model, false, condo);

                        await _apartmentRepository.UpdateAsync(apartment);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!await _apartmentRepository.ExistAsync(model.Id))
                        {
                            return new NotFoundViewResult("ApartmentNotFound");
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

        // GET: Apartments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ApartmentNotFound");
            }

            var apartment = await _apartmentRepository.GetByIdAsync(id.Value);


            if (apartment == null)
            {
                return new NotFoundViewResult("ApartmentNotFound");
            }

            return View(apartment);
        }

        // POST: Apartments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var apartment = await _apartmentRepository.GetByIdAsync(id);

            try
            {
                await _apartmentRepository.DeleteAsync(apartment);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {

                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    var errorModel = new ErrorViewModel
                    {
                        ErrorTitle = $"{apartment.Id} provavelmente está a ser usado!!",
                        ErrorMessage = $"{apartment.Id} não pode ser apagado",
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

        public IActionResult ApartmentNotFound()
        {
            return View();
        }


    }
}
