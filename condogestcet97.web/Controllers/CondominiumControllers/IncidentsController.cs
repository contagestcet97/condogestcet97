using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using condogestcet97.web.Data;
using condogestcet97.web.Data.Entities.Condominium;
using condogestcet97.web.Data.Repositories.IRepositories;
using condogestcet97.web.Helpers.IHelpers;
using condogestcet97.web.Data.CondominiumRepositories.ICondominiumRepositories;
using condogestcet97.web.Data.Repositories;
using condogestcet97.web.Helpers;
using System.Diagnostics;
using condogestcet97.web.Models;

namespace incidentgestcet97.web.Controllers.incidentminiumControllers
{
    public class IncidentsController : Controller
    {
        private readonly IIncidentRepository _incidentRepository;
        private readonly ICondominiumsConverterHelper _converterHelper;

        public IncidentsController(IIncidentRepository incidentRepository,
            ICondominiumsConverterHelper converterHelper)
        {
            _converterHelper = converterHelper;
            _incidentRepository = incidentRepository;

        }

        // GET: Incidents
        public IActionResult Index()
        {
            return View(_incidentRepository.GetAll());
        }

        // GET: Incidents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("IncidentNotFound");
            }

            var incident = await _incidentRepository.GetByIdAsync(id.Value);


            if (incident == null)
            {
                return new NotFoundViewResult("IncidentNotFound");
            }

            return View(incident);
        }

        // GET: Incidents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Incidents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IncidentViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                        var incident = _converterHelper.ToIncident(model, true);

                        try
                        {
                            await _incidentRepository.CreateAsync(incident);


                            return RedirectToAction(nameof(Index));

                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex.ToString());
                        }
                        return RedirectToAction(nameof(Index));
                    
            }
            
            return View(model); 
        }
            

        // GET: Incidents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incident = await _incidentRepository.GetByIdAsync(id.Value);

            if (incident == null)
            {
                return NotFound();
            }

            var model = _converterHelper.ToIncidentViewModel(incident);

            return View(model);
        }

        // POST: Incidents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IncidentViewModel model)
        {
            if (ModelState.IsValid)
            {
                    try
                    {
                        var incident = _converterHelper.ToIncident(model, false);

                        await _incidentRepository.UpdateAsync(incident);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!await _incidentRepository.ExistAsync(model.Id))
                        {
                            return new NotFoundViewResult("IncidentNotFound");
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

        // GET: Incidents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("IncidentNotFound");
            }

            var incident = await _incidentRepository.GetByIdAsync(id.Value);


            if (incident == null)
            {
                return new NotFoundViewResult("IncidentNotFound");
            }

            return View(incident);
        }

        // POST: Incidents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var incident = await _incidentRepository.GetByIdAsync(id);

            try
            {
                await _incidentRepository.DeleteAsync(incident);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {

                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    var errorModel = new ErrorViewModel
                    {
                        ErrorTitle = $"{incident.Id} provavelmente está a ser usado!!",
                        ErrorMessage = $"{incident.Id} não pode ser apagado",
                    };

                    return View("Error", errorModel);
                }


                return View("Error", new ErrorViewModel
                {
                    ErrorTitle = "Erro de base de dados",
                    ErrorMessage = "Ocorreu um erro inesperado ao tentar apagar o incident."
                });
            }
        }

    }
}
