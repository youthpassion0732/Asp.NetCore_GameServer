using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using DomainEntities;
using StartPoint.GameServer.Helpers;
using StartPoint.GameServer.Areas.BackOffice.Models;

namespace StartPoint.GameServer.Areas.BackOffice.Controllers
{
    [Area("BackOffice")]
    public class QuizOfferController : Controller
    {
        private IQuizOfferService _quizOfferService;
        private Helper helper;

        public QuizOfferController(IQuizOfferService quizOfferService)
        {
            _quizOfferService = quizOfferService;
            helper = new Helper();
        }

        // GET: BackOffice/QuizOffer
        public IActionResult Index(int id)
        {
            var quizOffer = _quizOfferService.Get(x => x.GameId == id);
            if (quizOffer == null)
                return View(new QuizOffer { GameId = id });
            return View(quizOffer);
        }

        // GET: BackOffice/QuizOffer/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quizOffer = _quizOfferService.Get(m => m.Id == id);
            if (quizOffer == null)
            {
                return NotFound();
            }

            return View(quizOffer);
        }

        // GET: BackOffice/QuizOffer/Create
        public IActionResult Create(int id)
        {
            var quizOffer = _quizOfferService.Get(x => x.GameId == id);
            return View(helper.GetQuizOfferViewModel(quizOffer, true) == null ? new QuizOfferViewModel { GameId = id } : helper.GetQuizOfferViewModel(quizOffer, true));
        }

        // POST: BackOffice/QuizOffer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,MinimumScore,VideoFile,PhotoFile,GameId")] QuizOfferViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _quizOfferService.BeginTransaction();
                    var quizOffer = helper.GetQuizOffer(model);
                    quizOffer.Id = _quizOfferService.Add(quizOffer);
                    await helper.FileUploadAsync(model.PhotoFile, quizOffer.PhotoName, "QuizOffer");
                    await helper.FileUploadAsync(model.VideoFile, quizOffer.VideoName, "QuizOffer");
                    _quizOfferService.CommitTransaction();
                    return RedirectToAction(nameof(Index), new { id = model.GameId });
                }
                catch
                {
                    _quizOfferService.RollbackTransaction();
                }
            }
            return View(model);
        }

        // GET: BackOffice/QuizOffer/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quizOffer = _quizOfferService.Get(x => x.Id == id);
            if (quizOffer == null)
            {
                return NotFound();
            }
            return View(helper.GetQuizOfferViewModel(quizOffer, true));
        }

        // POST: BackOffice/QuizOffer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,MinimumScore,VideoFile,PhotoFile,GameId")] QuizOfferViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }
            ModelState.Remove("PhotoFile");
            ModelState.Remove("VideoFile");
            if (ModelState.IsValid)
            {
                try
                {
                    _quizOfferService.BeginTransaction();
                    var quizOffer = _quizOfferService.Get(x => x.Id == model.Id);
                    if (quizOffer != null)
                    {
                        model.GameId = quizOffer.GameId;
                        quizOffer.Description = model.Description;
                        quizOffer.MinimumScore = model.MinimumScore;
                        if (model.PhotoFile != null)
                        {
                            quizOffer.PhotoName = helper.GetFileName(model.PhotoFile);
                            await helper.FileUploadAsync(model.PhotoFile, quizOffer.PhotoName, "QuizOffer");
                        }
                        if (model.VideoFile != null)
                        {
                            quizOffer.VideoName = helper.GetFileName(model.VideoFile);
                            await helper.FileUploadAsync(model.VideoFile, quizOffer.VideoName, "QuizOffer");
                        }
                        _quizOfferService.Update(quizOffer);
                        _quizOfferService.CommitTransaction();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    _quizOfferService.RollbackTransaction();
                    if (!QuizOfferExists(model.Id))
                    {
                        return NotFound();
                    }
                }
                return RedirectToAction(nameof(Index), new { id = model.GameId });
            }
            return View(model);
        }

        // GET: BackOffice/QuizOffer/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quizOffer = _quizOfferService.Get(m => m.Id == id);
            if (quizOffer == null)
            {
                return NotFound();
            }

            return View(quizOffer);
        }

        // POST: BackOffice/QuizOffer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var quizOffer = _quizOfferService.Get(x => x.Id == id);
            _quizOfferService.Delete(quizOffer);
            return RedirectToAction(nameof(Index));
        }

        private bool QuizOfferExists(int id)
        {
            return _quizOfferService.Get(e => e.Id == id) != null;
        }
    }
}
