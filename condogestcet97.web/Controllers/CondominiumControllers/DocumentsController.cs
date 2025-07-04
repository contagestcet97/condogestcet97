using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using condogestcet97.web.Data;
using condogestcet97.web.Data.Entities.Condominium;
using System.Diagnostics;
using condogestcet97.web.Models;
using condogestcet97.web.Data.CondominiumRepositories.ICondominiumRepositories;
using condogestcet97.web.Helpers.IHelpers;
using condogestcet97.web.Helpers;
using Microsoft.Identity.Client;
using System.Runtime.CompilerServices;

namespace condogestcet97.web.Controllers.CondominiumControllers
{
    public class DocumentsController : Controller
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IMeetingRepository _meetingRepository;
        private readonly IInterventionRepository _interventionRepository;
        private readonly ICondiminiumsConverterHelper _converterHelper;

        public DocumentsController(DataContextCondominium context,
            IDocumentRepository documentRepository,
            ICondiminiumsConverterHelper converterHelper,
            IInterventionRepository interventionRepository,
            IMeetingRepository meetingRepository)
        {
            _documentRepository = documentRepository;
            _converterHelper = converterHelper;
            _interventionRepository = interventionRepository;
            _meetingRepository = meetingRepository; 
        }

        // GET: Documents
        public async Task<IActionResult> Index()
        {
            return View(await _documentRepository.GetAllTrackedAsync());
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
            return View();
        }

        // POST: Documents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DocumentViewModel model)
        {
            if (ModelState.IsValid)
            {

                var document = await GetMeetingOrInterventionDocument(model);

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
        private async Task<Document> GetMeetingOrInterventionDocument(DocumentViewModel model)
        {
            if (model.Type == DocumentType.Meeting)
            {
                var meeting = await _meetingRepository.GetByIdTrackedAsync(model.MeetingId.Value);

                var meetingDocument = _converterHelper.ToDocument(model, true, meeting, null);

                return meetingDocument;
            }

            var intervention = await _interventionRepository.GetByIdTrackedAsync(model.InterventionId.Value);

            var interventionDocument = _converterHelper.ToDocument(model, true, null, intervention);

            return interventionDocument;
        }

        // GET: Documents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("DocumentNotFound");
            }

            var model = await ConvertToModel(id);

            if (model == null)
            {
                return new NotFoundViewResult("DocumentNotFound");
            }

                return View(model);
        }

        private async Task<DocumentViewModel> ConvertToModel(int? id)
        {
            DocumentViewModel model = null;

            var document = await _documentRepository.GetByIdAsync(id.Value);

            if (document is MeetingDocument)
            {
                MeetingDocument meetDocument = await _documentRepository.GetMeetDocTrackedAsync(document.Id);

                if (meetDocument != null)
                {
                    return model =  _converterHelper.ToDocumentViewModelFromMeetingDoc(meetDocument);
                }
            }

            var interventionDocument = await _documentRepository.GetInterventionDocTrackedAsync(document.Id);

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

                var meeting = await GetMeeting(model);
                var intervention = await GetIntervention(model);
              
                    try
                    {
                        var document = _converterHelper.ToDocument(model, false, meeting, intervention);

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

        private async Task<Meeting?> GetMeeting(DocumentViewModel model)
        {
            Meeting? meeting = null;

            if (model.MeetingId != null)
            {
                meeting = await _meetingRepository.GetByIdTrackedAsync(model.MeetingId.Value);
            }

            return meeting;
        }

        private async Task<Intervention?> GetIntervention(DocumentViewModel model)
        {
            Intervention? intervention = null;

            if (model.InterventionId != null)
            {
                intervention = await _interventionRepository.GetByIdTrackedAsync(model.InterventionId.Value);
            }

            return intervention;
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
