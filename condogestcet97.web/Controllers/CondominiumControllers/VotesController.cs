using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using condogestcet97.web.Data;
using condogestcet97.web.Data.Entities.Condominium;
using condogestcet97.web.Data.CondominiumRepositories.ICondominiumRepositories;
using condogestcet97.web.Helpers.IHelpers;
using condogestcet97.web.Helpers;
using System.Diagnostics;
using condogestcet97.web.Models;
using condogestcet97.web.Data.CondominiumRepositories;

namespace condogestcet97.web.Controllers.CondominiumControllers
{
    public class VotesController : Controller
    {
        private readonly IVoteRepository _voteRepository;
        private readonly IMeetingRepository _meetingRepository;
        private readonly ICondominiumsConverterHelper _converterHelper;

        public VotesController(IMeetingRepository meetingRepository, IVoteRepository voteRepository, ICondominiumsConverterHelper converterHelper)
        {           
            _meetingRepository = meetingRepository;
            _voteRepository = voteRepository;
            _converterHelper = converterHelper;
        }

        // GET: Votes
        public IActionResult Index()
        {
            return View(_voteRepository.GetAll());
        }

        // GET: Votes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("VoteNotFound");
            }

            var vote = await _voteRepository.GetByIdAsync(id.Value);


            if (vote == null)
            {
                return new NotFoundViewResult("VoteNotFound");
            }

            return View(vote);
        }

        // GET: Votes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Votes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VoteViewModel model)
        {
            if (ModelState.IsValid)
            {

                var meeting = await _meetingRepository.GetByIdTrackedAsync(model.MeetingId);


                if (meeting != null)
                {
                    var vote = _converterHelper.ToVote(model, false, meeting);

                    try
                    {
                        await _voteRepository.CreateAsync(vote);


                        return RedirectToAction(nameof(Index));

                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.ToString());
                    }
                    return RedirectToAction(nameof(Index));

                }

            }
            return View(model);
        }

        // GET: Votes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("VoteNotFound");
            }

            var vote = await _voteRepository.GetByIdAsync(id.Value);

            if (vote == null)
            {
                return new NotFoundViewResult("VoteNotFound");
            }

            var model = _converterHelper.ToVoteViewModel(vote);

            return View(model);
        }

        // POST: Votes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(VoteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var meeting = await _meetingRepository.GetByIdTrackedAsync(model.MeetingId);

                if (meeting != null)
                {
                    try
                    {
                        var vote = _converterHelper.ToVote(model, false, meeting);

                        await _voteRepository.UpdateAsync(vote);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!await _voteRepository.ExistAsync(model.Id))
                        {
                            return new NotFoundViewResult("VoteNotFound");
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }

        // GET: Votes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("VoteNotFound");
            }

            var vote = await _voteRepository.GetByIdAsync(id.Value);


            if (vote == null)
            {
                return new NotFoundViewResult("VoteNotFound");
            }

            return View(vote);
        }

        // POST: Votes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vote = await _voteRepository.GetByIdAsync(id);

            try
            {
                await _voteRepository.DeleteAsync(vote);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {

                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    var errorModel = new ErrorViewModel
                    {
                        ErrorTitle = $"{vote.Id} provavelmente está a ser usado!!",
                        ErrorMessage = $"{vote.Id} não pode ser apagado",
                    };

                    return View("Error", errorModel);
                }


                return View("Error", new ErrorViewModel
                {
                    ErrorTitle = "Erro de base de dados",
                    ErrorMessage = "Ocorreu um erro inesperado ao tentar apagar o vote."
                });

            }
        }

        //public IActionResult VoteNotFound()
        //{
        //    return View();
        //}

    }
}
