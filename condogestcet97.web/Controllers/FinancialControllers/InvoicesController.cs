using condogestcet97.web.Data;
using condogestcet97.web.Data.Entities.Financial;
using condogestcet97.web.Data.FinancialRepositories.IFinancialRepositories;
using condogestcet97.web.Helpers;
using condogestcet97.web.Helpers.IHelpers;
using condogestcet97.web.Migrations;
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
    public class InvoicesController : Controller
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IFinancialConverterHelper _converterHelper;
        private readonly IQuotaRepository _quotaRepository;
        private readonly IExpenseRepository _expenseRepository;
        public InvoicesController(IInvoiceRepository InvoiceRepository, IFinancialConverterHelper converterHelper, IQuotaRepository quotaRepository, IExpenseRepository expenseRepository)
        {
            _invoiceRepository = InvoiceRepository;
            _converterHelper = converterHelper;
            _quotaRepository = quotaRepository;
            _expenseRepository = expenseRepository;
        }

        // GET: Invoices
        public async Task<IActionResult> Index()
        {
            return View(await _invoiceRepository.GetAllAsync());
        }

        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("InvoiceNotFound");
            }

            var Invoice = await GetInvoiceAndType(id.Value);


            if (Invoice == null)
            {
                return new NotFoundViewResult("InvoiceNotFound");
            }

            return View(Invoice);
        }

        private async Task<Invoice> GetInvoiceAndType(int? id)
        {
            var invoice = await _invoiceRepository.GetByIdAsync(id.Value);

            if (invoice is IncomingInvoice)
            {
                invoice = await _invoiceRepository.GetInInvoiceAsync(invoice.Id);
                return invoice;
            }

            invoice = await _invoiceRepository.GetOutInvoiceAsync(invoice.Id);

            return invoice;
        }


        // GET: Invoices/Create
        public IActionResult Create()
        {

            var model = new InvoiceViewModel
            {
                EmissionDate = DateTime.Now.Date,
                DueDate = DateTime.Now.Date,
                InvoiceType = InvoiceType.Outgoing,

                Quotas = _quotaRepository.GetAll().Select(q => new SelectListItem
                {
                    Value = q.Id.ToString(),
                    Text = $"Condo {q.CondoId}, DueDate = {q.DueDate}"
                }),

                Expenses = _expenseRepository.GetAll().Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = $"{e.Id} {e.Description}"
                }),

                //TODO: Have this link to the user repository? Also filtered by condos
                //Users = _quotaRepository.GetAll().Select(q => new SelectListItem
                //{
                //    Value = q.Id.ToString(),
                //    Text = $"Condo {q.CondoId}, DueDate = {q.DueDate}"
                //})

                Users = new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Value = "1",
                        Text = "Fake user1"
                    },

                    new SelectListItem
                    {
                        Value = "2",
                        Text = "Fake user2"
                    }
                }

            };

            return View(model);
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InvoiceViewModel model)
        {

            var invoice = ConvertToInOrOutgoing(model);

            try
            {
                await _invoiceRepository.CreateAsync(invoice);


                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            return View(model);
        }

        private Invoice ConvertToInOrOutgoing(InvoiceViewModel model)
        {
            if (model.InvoiceType == InvoiceType.Incoming)
            {
                var incomingInvoice = _converterHelper.ToInvoice(model, true);

                return incomingInvoice;
            }

            var outgoingInvoice = _converterHelper.ToInvoice(model, true);

            return outgoingInvoice;
        }

        private async Task<InvoiceViewModel> ConvertToInOrOutgoingAsync(int? id)
        {
            InvoiceViewModel model = null;

            var invoice = await _invoiceRepository.GetByIdAsync(id.Value);

            if (invoice is IncomingInvoice)
            {
                IncomingInvoice inInvoice = await _invoiceRepository.GetInInvoiceAsync(invoice.Id);

                if (inInvoice != null)
                {
                    return model = _converterHelper.ToInvoiceViewModelFromIncomingInvoice(inInvoice);
                } 
            }

            OutgoingInvoice outInvoice = await _invoiceRepository.GetOutInvoiceAsync(invoice.Id);
            
            if (outInvoice != null)
            {
                return model = _converterHelper.ToInvoiceViewModelFromOutgoingInvoice(outInvoice);
            }

            return model;
        }

        // GET: Invoices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("InvoiceNotFound");
            }

            var model = await ConvertToInOrOutgoingAsync(id);

            if (model == null)
            {
                return new NotFoundViewResult("InvoiceNotFound");
            }

                model.Quotas = _quotaRepository.GetAll().Select(q => new SelectListItem
                {
                    Value = q.Id.ToString(),
                    Text = $"Condo {q.CondoId}, DueDate = {q.DueDate}"
                });

                //TODO: Change users when linked to other database
                model.Users = new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Value = "1",
                        Text = "Fake user1"
                    },

                    new SelectListItem
                    {
                        Value = "2",
                        Text = "Fake user2"
                    }
                };

                model.Expenses = _expenseRepository.GetAll().Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = $"{e.Id} {e.Description}"
                });

            

            return View(model);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(InvoiceViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Invoice = _converterHelper.ToInvoice(model, false);

                    await _invoiceRepository.UpdateAsync(Invoice);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _invoiceRepository.ExistAsync(model.Id))
                    {
                        return new NotFoundViewResult("InvoiceNotFound");
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

        // GET: Invoices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("InvoiceNotFound");
            }

            var Invoice = await _invoiceRepository.GetByIdAsync(id.Value);


            if (Invoice == null)
            {
                return new NotFoundViewResult("InvoiceNotFound");
            }

            return View(Invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Invoice = await _invoiceRepository.GetByIdAsync(id);

            try
            {
                await _invoiceRepository.DeleteAsync(Invoice);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {

                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    var errorModel = new ErrorViewModel
                    {
                        ErrorTitle = $"{Invoice.Id} provavelmente está a ser usado!!",
                        ErrorMessage = $"{Invoice.Id} não pode ser apagado",
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

        public IActionResult InvoiceNotFound()
        {
            return View();
        }
    }
}
