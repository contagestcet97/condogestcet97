using condogestcet97.web.Data;
using condogestcet97.web.Data.Entities.Financial;
using condogestcet97.web.Data.FinancialRepositories;
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
    public class ReceiptsController : Controller
    {
        private readonly IReceiptRepository _receiptRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IFinancialConverterHelper _converterHelper;

        public ReceiptsController(IReceiptRepository ReceiptRepository,
            IFinancialConverterHelper converterHelper,
            IPaymentRepository paymentRepository)
        {
            _receiptRepository = ReceiptRepository;
            _converterHelper = converterHelper;
            _paymentRepository = paymentRepository;
        }

        // GET: Receipts
        public async Task<IActionResult> Index()
        {
            return View(_receiptRepository.GetAll());
        }

        // GET: Receipts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ReceiptNotFound");
            }

            var Receipt = await _receiptRepository.GetByIdAsync(id.Value);


            if (Receipt == null)
            {
                return new NotFoundViewResult("ReceiptNotFound");
            }

            return View(Receipt);
        }


        // GET: Receipts/Create
        public IActionResult Create()
        {
            var model = new ReceiptViewModel
            {
                Payments = _paymentRepository.GetAll().Select(i => new SelectListItem
                {
                    Value = i.Id.ToString(),
                    Text = $"{i.Id}: {i.Amount}€ - {i.PaidDate}"
                })
            };
              
            return View(model);
        }

        // POST: Receipts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReceiptViewModel model)
        {
            var Receipt = _converterHelper.ToReceipt(model, true);

            try
            {
                await _receiptRepository.CreateAsync(Receipt);


                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            return View(model);
        }

        // GET: Receipts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ReceiptNotFound");
            }

            var Receipt = await _receiptRepository.GetByIdAsync(id.Value);

            if (Receipt == null)
            {
                return new NotFoundViewResult("ReceiptNotFound");
            }

            var model = _converterHelper.ToReceiptViewModel(Receipt);

            model.Payments = _paymentRepository.GetAll().Select(i => new SelectListItem
            {
                Value = i.Id.ToString(),
                Text = $"{i.Id}: {i.Amount}€ - {i.PaidDate}"
            });

            return View(model);
        }

        // POST: Receipts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ReceiptViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Receipt = _converterHelper.ToReceipt(model, false);

                    await _receiptRepository.UpdateAsync(Receipt);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _receiptRepository.ExistAsync(model.Id))
                    {
                        return new NotFoundViewResult("ReceiptNotFound");
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

        // GET: Receipts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ReceiptNotFound");
            }

            var Receipt = await _receiptRepository.GetByIdAsync(id.Value);


            if (Receipt == null)
            {
                return new NotFoundViewResult("ReceiptNotFound");
            }

            return View(Receipt);
        }

        // POST: Receipts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Receipt = await _receiptRepository.GetByIdAsync(id);

            try
            {
                await _receiptRepository.DeleteAsync(Receipt);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {

                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    var errorModel = new ErrorViewModel
                    {
                        ErrorTitle = $"{Receipt.Id} provavelmente está a ser usado!!",
                        ErrorMessage = $"{Receipt.Id} não pode ser apagado",
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

        public IActionResult ReceiptNotFound()
        {
            return View();
        }
    }
}
