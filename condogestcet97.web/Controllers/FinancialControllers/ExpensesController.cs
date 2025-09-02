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
    public class ExpensesController : Controller
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IFinancialConverterHelper _converterHelper;

        public ExpensesController(IExpenseRepository ExpenseRepository, IFinancialConverterHelper converterHelper)
        {
            _expenseRepository = ExpenseRepository;
            _converterHelper = converterHelper;
        }

        // GET: Expenses
        public async Task<IActionResult> Index()
        {
            return View(_expenseRepository.GetAll());
        }

        // GET: Expenses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ExpenseNotFound");
            }

            var Expense = await _expenseRepository.GetByIdAsync(id.Value);


            if (Expense == null)
            {
                return new NotFoundViewResult("ExpenseNotFound");
            }

            return View(Expense);
        }


        // GET: Expenses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Expenses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExpenseViewModel model)
        {
            var Expense = _converterHelper.ToExpense(model, true);

            try
            {
                await _expenseRepository.CreateAsync(Expense);


                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            return View(model);
        }

        // GET: Expenses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ExpenseNotFound");
            }

            var Expense = await _expenseRepository.GetByIdAsync(id.Value);

            if (Expense == null)
            {
                return new NotFoundViewResult("ExpenseNotFound");
            }

            var model = _converterHelper.ToExpenseViewModel(Expense);

            return View(model);
        }

        // POST: Expenses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ExpenseViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Expense = _converterHelper.ToExpense(model, false);

                    await _expenseRepository.UpdateAsync(Expense);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _expenseRepository.ExistAsync(model.Id))
                    {
                        return new NotFoundViewResult("ExpenseNotFound");
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

        // GET: Expenses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ExpenseNotFound");
            }

            var Expense = await _expenseRepository.GetByIdAsync(id.Value);


            if (Expense == null)
            {
                return new NotFoundViewResult("ExpenseNotFound");
            }

            return View(Expense);
        }

        // POST: Expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Expense = await _expenseRepository.GetByIdAsync(id);

            try
            {
                await _expenseRepository.DeleteAsync(Expense);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {

                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    var errorModel = new ErrorViewModel
                    {
                        ErrorTitle = $"{Expense.Id} provavelmente está a ser usado!!",
                        ErrorMessage = $"{Expense.Id} não pode ser apagado",
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

        public IActionResult ExpenseNotFound()
        {
            return View();
        }
    }
}
