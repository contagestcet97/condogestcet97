using condogestcet97.web.Data.CondominiumRepositories.ICondominiumRepositories;
using condogestcet97.web.Helpers;
using condogestcet97.web.Helpers.IHelpers;
using condogestcet97.web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace condogestcet97.web.Controllers.CondominiumControllers
{
    public class VotesController : Controller
    {
        private readonly IVoteRepository _voteRepository;
        private readonly IMeetingRepository _meetingRepository;
        private readonly ICondominiumsConverterHelper _converterHelper;

        public VotesController(IVoteRepository voteRepository, ICondominiumsConverterHelper converterHelper, IMeetingRepository meetingRepository)
        {
            _voteRepository = voteRepository;
            _converterHelper = converterHelper;
            _meetingRepository = meetingRepository;
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
            var model = new VoteViewModel
            {
                Meetings = _meetingRepository.GetAll().
                Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = $"{m.Topic} {m.Date}"
                })
                 .ToList(),
            };

            return View(model);
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

                var vote = _converterHelper.ToVote(model, true);

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

            model.Meetings = _meetingRepository.GetAll().
                Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = $"{m.Topic} {m.Date}"
                })
                 .ToList();

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
                try
                {
                    var vote = _converterHelper.ToVote(model, false);

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

    }
}
