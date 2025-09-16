using condogestcet97.web.Data.CondominiumRepositories.ICondominiumRepositories;
using condogestcet97.web.Data.Entities.Condominium;
using condogestcet97.web.Data.Repositories.IRepositories;
using condogestcet97.web.Helpers;
using condogestcet97.web.Helpers.IHelpers;
using condogestcet97.web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace condogestcet97.web.Controllers.CondominiumControllers
{
    public class MeetingsController : Controller
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly ICondoRepository _condoRepository;
        private readonly ICondominiumsConverterHelper _converterHelper;

        public MeetingsController(IMeetingRepository meetingRepository,
            ICondominiumsConverterHelper condiminiumsConverterHelper, ICondoRepository condoRepository)
        {
            _meetingRepository = meetingRepository;
            _converterHelper = condiminiumsConverterHelper;
            _condoRepository = condoRepository;
        }

        // GET: Meetings
        public IActionResult Index()
        {
            return View(_meetingRepository.GetAll());
        }

        // GET: Meetings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("MeetingnNotFound");
            }

            var meeting = await _meetingRepository.GetByIdAsync(id.Value);


            if (meeting == null)
            {
                return new NotFoundViewResult("MeetingnNotFound");
            }

            return View(meeting);
        }

        // GET: Meetings/Create
        public IActionResult Create()
        {
            var model = new MeetingViewModel
            {
                Date = DateTime.Now,

                Condos = _condoRepository.GetAll()
                 .Select(m => new SelectListItem
                 {
                     Value = m.Id.ToString(),
                     Text = m.Address
                 })
                 .ToList(),
            };

            return View(model);
        }

        // POST: Meetings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MeetingViewModel model)
        {

            if (ModelState.IsValid)
            {

                var meeting = _converterHelper.ToMeeting(model, true);

                try
                {
                    await _meetingRepository.CreateAsync(meeting);


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

        // GET: Meetings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meeting = await _meetingRepository.GetByIdAsync(id.Value);

            if (meeting == null)
            {
                return NotFound();
            }

            var model = _converterHelper.ToMeetingViewModel(meeting);

            model.Condos = _condoRepository.GetAll()
             .Select(m => new SelectListItem
             {
                 Value = m.Id.ToString(),
                 Text = m.Address
             })
             .ToList();

            return View(model);
        }

        // POST: Meetings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MeetingViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var meeting = _converterHelper.ToMeeting(model, false);

                    await _meetingRepository.UpdateAsync(meeting);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _meetingRepository.ExistAsync(model.Id))
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

        // GET: Meetings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("MeetingNotFound");
            }

            var meeting = await _meetingRepository.GetByIdAsync(id.Value);


            if (meeting == null)
            {
                return new NotFoundViewResult("MeetingNotFound");
            }

            return View(meeting);
        }

        // POST: Meetings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var meeting = await _meetingRepository.GetByIdAsync(id);

            try
            {
                await _meetingRepository.DeleteAsync(meeting);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {

                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    var errorModel = new ErrorViewModel
                    {
                        ErrorTitle = $"{meeting.Id} provavelmente está a ser usado!!",
                        ErrorMessage = $"{meeting.Id} não pode ser apagado",
                    };

                    return View("Error", errorModel);
                }


                return View("Error", new ErrorViewModel
                {
                    ErrorTitle = "Erro de base de dados",
                    ErrorMessage = "Ocorreu um erro inesperado ao tentar apagar o meeting."
                });
            }
        }

    }
}
