using condogestcet97.web.Data.CondominiumRepositories.ICondominiumRepositories;
using condogestcet97.web.Data.Entities.Condominium;
using condogestcet97.web.Helpers;
using condogestcet97.web.Helpers.IHelpers;
using condogestcet97.web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace condogestcet97.web.Controllers.CondominiumControllers
{
    public class InterventionsController : Controller
    {
        private readonly ICondominiumsConverterHelper _converterHelper;
        private readonly IInterventionRepository _interventionRepository;
        private readonly IIncidentRepository _incidentRepository;

        public InterventionsController(ICondominiumsConverterHelper condiminiumsConverterHelper,
            IInterventionRepository interventionRepository, IIncidentRepository incidentRepository)
        {
            _interventionRepository = interventionRepository;
            _converterHelper = condiminiumsConverterHelper;
            _incidentRepository = incidentRepository;
        }

        // GET: Interventions
        public IActionResult Index()
        {
            return View(_interventionRepository.GetAll());
        }

        // GET: Interventions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("InterventionNotFound");
            }

            var intervention = await _interventionRepository.GetByIdAsync(id.Value);


            if (intervention == null)
            {
                return new NotFoundViewResult("InterventionNotFound");
            }

            return View(intervention);
        }

        // GET: Interventions/Create
        public IActionResult Create()
        {
            var model = new InterventionViewModel
            {
                Date = DateTime.Now.Date,

                Incidents = _incidentRepository.GetAll()
                .Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = $"{m.Title} {m.Date}"
                })
                .ToList(),
            };

            return View(model);
        }

        // POST: Interventions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InterventionViewModel model)
        {
            if (ModelState.IsValid)
            {

                var intervention = _converterHelper.ToIntervention(model, true);

                try
                {
                    await _interventionRepository.CreateAsync(intervention);


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

        // GET: Interventions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intervention = await _interventionRepository.GetByIdAsync(id.Value);

            if (intervention == null)
            {
                return NotFound();
            }

            var model = _converterHelper.ToInterventionViewModel(intervention);

            model.Incidents = _incidentRepository.GetAll()
                .Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = $"{m.Title} {m.Date}"
                })
                .ToList();

            return View(model);
        }

        // POST: Interventions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(InterventionViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var intervention = _converterHelper.ToIntervention(model, false);

                    await _interventionRepository.UpdateAsync(intervention);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _interventionRepository.ExistAsync(model.Id))
                    {
                        return new NotFoundViewResult("InterventionNotFound");
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

        // GET: Interventions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("InterventionNotFound");
            }

            var intervention = await _interventionRepository.GetByIdAsync(id.Value);


            if (intervention == null)
            {
                return new NotFoundViewResult("InterventionNotFound");
            }

            return View(intervention);
        }

        // POST: Interventions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var intervention = await _interventionRepository.GetByIdAsync(id);

            try
            {
                await _interventionRepository.DeleteAsync(intervention);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {

                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    var errorModel = new ErrorViewModel
                    {
                        ErrorTitle = $"{intervention.Id} provavelmente está a ser usado!!",
                        ErrorMessage = $"{intervention.Id} não pode ser apagado",
                    };

                    return View("Error", errorModel);
                }


                return View("Error", new ErrorViewModel
                {
                    ErrorTitle = "Erro de base de dados",
                    ErrorMessage = "Ocorreu um erro inesperado ao tentar apagar o intervention."
                });
            }
        }


    }
}
