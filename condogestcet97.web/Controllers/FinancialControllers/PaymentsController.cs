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
    public class PaymentsController : Controller
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IFinancialConverterHelper _converterHelper;

        public PaymentsController(IPaymentRepository PaymentRepository, IFinancialConverterHelper converterHelper, IInvoiceRepository invoiceRepository)
        {
            _paymentRepository = PaymentRepository;
            _converterHelper = converterHelper;
            _invoiceRepository = invoiceRepository;
        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
            return View(_paymentRepository.GetAll());
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("PaymentNotFound");
            }

            var Payment = await _paymentRepository.GetByIdAsync(id.Value);


            if (Payment == null)
            {
                return new NotFoundViewResult("PaymentNotFound");
            }

            return View(Payment);
        }


        // GET: Payments/Create
        public IActionResult Create()
        {

            var model = new PaymentViewModel
            {
                PaidDate = DateTime.Now.Date,

                Invoices = _invoiceRepository.GetAll().Select(i => new SelectListItem
                {
                    Value = i.Id.ToString(),
                    Text = $"{i.DueDate} + {i.Description}"
                })
            };

            return View(model);
        }

        // POST: Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaymentViewModel model)
        {
            var Payment = _converterHelper.ToPayment(model, true);

            try
            {
                await _paymentRepository.CreateAsync(Payment);


                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            return View(model);
        }

        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("PaymentNotFound");
            }

            var Payment = await _paymentRepository.GetByIdAsync(id.Value);


            if (Payment == null)
            {
                return new NotFoundViewResult("PaymentNotFound");
            }

            var model = _converterHelper.ToPaymentViewModel(Payment);

            model.Invoices = _invoiceRepository.GetAll().Select(i => new SelectListItem
            {
                Value = i.Id.ToString(),
                Text = $"{i.DueDate} + {i.Description}"
            });

            return View(model);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PaymentViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Payment = _converterHelper.ToPayment(model, false);

                    await _paymentRepository.UpdateAsync(Payment);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _paymentRepository.ExistAsync(model.Id))
                    {
                        return new NotFoundViewResult("PaymentNotFound");
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

        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("PaymentNotFound");
            }

            var Payment = await _paymentRepository.GetByIdAsync(id.Value);


            if (Payment == null)
            {
                return new NotFoundViewResult("PaymentNotFound");
            }

            return View(Payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Payment = await _paymentRepository.GetByIdAsync(id);

            try
            {
                await _paymentRepository.DeleteAsync(Payment);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {

                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    var errorModel = new ErrorViewModel
                    {
                        ErrorTitle = $"{Payment.Id} provavelmente está a ser usado!!",
                        ErrorMessage = $"{Payment.Id} não pode ser apagado",
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

        public IActionResult PaymentNotFound()
        {
            return View();
        }
    }
}
