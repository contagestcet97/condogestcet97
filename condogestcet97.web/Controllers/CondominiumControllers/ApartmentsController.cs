using condogestcet97.web.Data;
using condogestcet97.web.Data.CondominiumRepositories.ICondominiumRepositories;
using condogestcet97.web.Data.Entities.Condominium;
using condogestcet97.web.Data.Repositories.IRepositories;
using condogestcet97.web.Helpers;
using condogestcet97.web.Helpers.IHelpers;
using condogestcet97.web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace condogestcet97.web.Controllers.CondominiumControllers
{
    [Authorize]
    public class ApartmentsController : Controller
    {
        private readonly IApartmentRepository _apartmentRepository;
        private readonly ICondoRepository _condoRepository;
        private readonly ICondominiumsConverterHelper _converterHelper;


        public ApartmentsController(IApartmentRepository apartmentRepository, ICondoRepository condoRepository,
            ICondominiumsConverterHelper converterHelper)
        {
            _apartmentRepository = apartmentRepository;
            _condoRepository = condoRepository;
            _converterHelper = converterHelper;
        }

        // GET: Apartments
        public IActionResult Index()
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
            ViewBag.Condos = _condoRepository.GetComboCondos();

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

                var apartment = _converterHelper.ToApartment(model, true);

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

                    try
                    {
                        var apartment = _converterHelper.ToApartment(model, false);

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
