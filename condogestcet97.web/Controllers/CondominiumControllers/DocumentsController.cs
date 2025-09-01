using condogestcet97.web.Data;
using condogestcet97.web.Data.CondominiumRepositories;
using condogestcet97.web.Data.CondominiumRepositories.ICondominiumRepositories;
using condogestcet97.web.Data.Entities.Condominium;
using condogestcet97.web.Helpers;
using condogestcet97.web.Helpers.IHelpers;
using condogestcet97.web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using NuGet.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace condogestcet97.web.Controllers.CondominiumControllers
{
    public class DocumentsController : Controller
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IMeetingRepository _meetingRepository;
        private readonly IInterventionRepository _interventionRepository;
        private readonly ICondominiumsConverterHelper _converterHelper;

        public DocumentsController(IDocumentRepository documentRepository,
            ICondominiumsConverterHelper converterHelper,
            IMeetingRepository meetingRepository,
            IInterventionRepository interventionRepository)
        {
            _documentRepository = documentRepository;
            _converterHelper = converterHelper;
            _meetingRepository = meetingRepository;
            _interventionRepository = interventionRepository;
        }

        // GET: Documents
        public async Task<IActionResult> Index()
        {
            return View(await _documentRepository.GetAllAsync());
        }

        // GET: Documents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("DocumentNotFound");
            }

            var document = await GetDocumentAndType(id);

            if (document == null)
            {
                return new NotFoundViewResult("DocumentNotFound");
            }

            return View(document);
        }

        private async Task<Document> GetDocumentAndType(int? id)
        {
            var document = await _documentRepository.GetByIdAsync(id.Value);

            if (document is MeetingDocument)
            {
                document = await _documentRepository.GetMeetDocAsync(document.Id);
                return document;
            }

            document = await _documentRepository.GetInterventionDocAsync(document.Id);
           
            return document;
        }

        // GET: Documents/Create
        public IActionResult Create()
        {
            var model = new DocumentViewModel
            {
                EmissionDate = DateTime.Now,
                Type = null,

                Meetings = _meetingRepository.GetAll()
                             .Select(m => new SelectListItem
                             {
                                 Value = m.Id.ToString(),
                                 Text = $"{m.Topic} ({m.Date:dd/MM/yyyy})"
                             })
                             .ToList(),

                Interventions = _interventionRepository.GetAll()
                             .Select(i => new SelectListItem
                             {
                                 Value = i.Id.ToString(),
                                 Text = $"{i.Title} ({i.Date:dd/MM/yyyy})"
                             })
                             .ToList()
            };

            return View(model);
        }

        // POST: Documents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DocumentViewModel model)
        {

            if (model.Type == DocumentType.Meeting)
            {
                model.InterventionId = null;
            }
            else if (model.Type == DocumentType.Intervention)
            {
                model.MeetingId = null;
            }


            if (ModelState.IsValid)
            {

                var document = GetMeetingOrInterventionDocument(model);

                    try
                    {
                        await _documentRepository.CreateAsync(document);


                        return RedirectToAction(nameof(Index));

                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.ToString());
                    }

                }

            return View(model);
        }
        private Document GetMeetingOrInterventionDocument(DocumentViewModel model)
        {
            if (model.Type == DocumentType.Meeting)
            {

                var meetingDocument = _converterHelper.ToDocument(model, true);

                return meetingDocument;
            }

            var interventionDocument = _converterHelper.ToDocument(model, true);

            return interventionDocument;
        }

        // GET: Documents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("DocumentNotFound");
            }

            var model = await ConvertToSpecificModel(id);

            if (model == null)
            {
                return new NotFoundViewResult("DocumentNotFound");
            }

            model.Meetings = _meetingRepository.GetAll()
                             .Select(m => new SelectListItem
                             {
                                 Value = m.Id.ToString(),
                                 Text = $"{m.Topic} ({m.Date:dd/MM/yyyy})"
                             })
                             .ToList();

            model.Interventions = _interventionRepository.GetAll()
                            .Select(i => new SelectListItem
                            {
                                Value = i.Id.ToString(),
                                Text = $"{i.Title} ({i.Date:dd/MM/yyyy})"
                            })
                            .ToList();

            return View(model);
        }

        private async Task<DocumentViewModel> ConvertToSpecificModel(int? id)
        {
            DocumentViewModel model = null;

            var document = await _documentRepository.GetByIdAsync(id.Value);

            if (document is MeetingDocument)
            {
                MeetingDocument meetDocument = await _documentRepository.GetMeetDocAsync(document.Id);

                if (meetDocument != null)
                {
                    return model =  _converterHelper.ToDocumentViewModelFromMeetingDoc(meetDocument);
                }
            }

            var interventionDocument = await _documentRepository.GetInterventionDocAsync(document.Id);

            if (interventionDocument != null)
            {
                return model = _converterHelper.ToDocumentViewModelFromInterventionDoc(interventionDocument);

            }

            return model;
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DocumentViewModel model)
        {
    
            if (ModelState.IsValid)
            {
             
                    try
                    {
                        var document = _converterHelper.ToDocument(model, false);

                        await _documentRepository.UpdateAsync(document);
                    }

                    catch (DbUpdateConcurrencyException)
                    {
                            if (!await _documentRepository.ExistAsync(model.Id))
                            {
                                return new NotFoundViewResult("DocumentNotFound");
                            }
                            else
                            {
                                throw;
                            }
                    }
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Documents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("DocumentNotFound");
            }

            var document = await _documentRepository.GetByIdAsync(id.Value);


            if (document == null)
            {
                return new NotFoundViewResult("DocumentNotFound");
            }

            return View(document);

        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var document = await _documentRepository.GetByIdAsync(id);

            try
            {
                await _documentRepository.DeleteAsync(document);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {

                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    var errorModel = new ErrorViewModel
                    {
                        ErrorTitle = $"{document.Id} provavelmente está a ser usado!!",
                        ErrorMessage = $"{document.Id} não pode ser apagado",
                    };

                    return View("Error", errorModel);
                }


                return View("Error", new ErrorViewModel
                {
                    ErrorTitle = "Erro de base de dados",
                    ErrorMessage = "Ocorreu um erro inesperado ao tentar apagar o documento."
                });
            }
        }


    }
}
