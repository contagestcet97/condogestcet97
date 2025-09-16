using condogestcet97.web.Data;
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
    [Authorize(Roles = "Admin")]
    public class CondosController : Controller
    {
        private readonly ICondoRepository _condoRepository;
        private readonly ICondominiumsConverterHelper _converterHelper;
        

        public CondosController(ICondoRepository condoRepository,
            ICondominiumsConverterHelper condiminiumsConverterHelper)
        {
            _condoRepository = condoRepository;
            _converterHelper = condiminiumsConverterHelper;
        }

        // GET: Condos
        public IActionResult Index()
        {
            return View(_condoRepository.GetAll());
        }

        // GET: Condos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            
                if (id == null)
                {
                    return new NotFoundViewResult("CondoNotFound");
                }

                var condo = await _condoRepository.GetByIdAsync(id.Value);


                if (condo == null)
                {
                    return new NotFoundViewResult("CondoNotFound");
                }

                return View(condo);
            
        }

        // GET: Condos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Condos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CondoViewModel model)
        {

            if (ModelState.IsValid)
            {
                var condo = _converterHelper.ToCondo(model, true);

                try
                {
                    await _condoRepository.CreateAsync(condo);


                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
            }

            return View(model);
        }

        // GET: Condos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var condo = await _condoRepository.GetByIdTrackedAsync(id.Value);

            if (condo == null)
            {
                return NotFound();
            }

            var model = _converterHelper.ToCondoViewModel(condo);

            return View(model);
        }

        // POST: Condos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CondoViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var condo = _converterHelper.ToCondo(model, false);

                    await _condoRepository.UpdateAsync(condo);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _condoRepository.ExistAsync(model.CondoId))
                    {
                        return new NotFoundViewResult("CondoNotFound");
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

        // GET: Condos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("CondoNotFound");
            }

            var condo = await _condoRepository.GetByIdTrackedAsync(id.Value);  


            if (condo == null)
            {
                return new NotFoundViewResult("CondoNotFound");
            }

            return View(condo);
        }

        // POST: Condos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var condo = await _condoRepository.GetByIdTrackedAsync(id);
                
            try
            {
                await _condoRepository.DeleteAsync(condo);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {

                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    var errorModel = new ErrorViewModel
                    {
                        ErrorTitle = $"{condo.Id} provavelmente está a ser usado!!",
                        ErrorMessage = $"{condo.Id} não pode ser apagado",
                    };

                    return View("Error", errorModel);
                }


                return View("Error", new ErrorViewModel
                {
                    ErrorTitle = "Erro de base de dados",
                    ErrorMessage = "Ocorreu um erro inesperado ao tentar apagar o condo."
                });
            }
        }

        public IActionResult CondoNotFound()
        {
            return View();
        }

    }
}
