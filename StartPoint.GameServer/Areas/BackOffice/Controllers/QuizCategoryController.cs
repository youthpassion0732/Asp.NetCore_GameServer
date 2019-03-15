using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using DomainEntities;
using StartPoint.GameServer.Areas.BackOffice.Models;
using StartPoint.GameServer.Helpers;

namespace StartPoint.GameServer.Areas.BackOffice.Controllers
{
    [Area("BackOffice")]
    public class QuizCategoryController : Controller
    {
        private IQuizCategoryService _quizCategoryService;
        private Helper helper;

        public QuizCategoryController(IQuizCategoryService quizCategoryService)
        {
            _quizCategoryService = quizCategoryService;
            helper = new Helper();
        }

        // GET: BackOffice/QuizCategory
        public IActionResult Index(int id)
        {
            var quizCategory = _quizCategoryService.Get(x => x.GameId == id);
            if (quizCategory == null)
                return View(new QuizCategory { GameId = id });
            return View(quizCategory);
        }

        // GET: BackOffice/QuizCategory/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quizCategory = _quizCategoryService.Get(m => m.Id == id);
            if (quizCategory == null)
            {
                return NotFound();
            }

            return View(quizCategory);
        }

        // GET: BackOffice/QuizCategory/Create
        public IActionResult Create(int id)
        {
            var quizCategory = _quizCategoryService.Get(x => x.GameId == id);
            return View(helper.GetQuizCategoryViewModel(quizCategory, true) == null ? new QuizCategoryViewModel { GameId = id } : helper.GetQuizCategoryViewModel(quizCategory, true));
        }

        // POST: BackOffice/QuizCategory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(QuizCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _quizCategoryService.BeginTransaction();
                    var quizCategory = helper.GetQuizCategory(model);
                    quizCategory.Id = _quizCategoryService.Add(quizCategory);
                    await helper.FileUploadAsync(model.IconInButtonFile, quizCategory.IconInButtonName, "QuizCategory");
                    await helper.FileUploadAsync(model.IconInPartyFile, quizCategory.IconInPartyName, "QuizCategory");
                    _quizCategoryService.CommitTransaction();
                    return RedirectToAction(nameof(Index), new {id = model.GameId });
                }
                catch
                {
                    _quizCategoryService.RollbackTransaction();
                }
            }
            return View(model);
        }

        // GET: BackOffice/QuizCategory/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quizCategory = _quizCategoryService.Get(x => x.Id == id);
            if (quizCategory == null)
            {
                return NotFound();
            }
            return View(helper.GetQuizCategoryViewModel(quizCategory, true));
        }

        // POST: BackOffice/QuizCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, QuizCategoryViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }
            ModelState.Remove("IconInButtonFile");
            ModelState.Remove("IconInPartyFile");
            if (ModelState.IsValid)
            {
                try
                {
                        _quizCategoryService.BeginTransaction();
                    var quizCategory = _quizCategoryService.Get(x => x.Id == model.Id);
                    if(quizCategory != null)
                    {
                        model.GameId = quizCategory.GameId;
                        quizCategory.TitleInEnglish = model.TitleInEnglish;
                        quizCategory.TitleInFrench = model.TitleInFrench;
                        quizCategory.TitleInGerman = model.TitleInGerman;
                        quizCategory.TitleInItalian = model.TitleInItalian;
                        quizCategory.TitleInSpain = model.TitleInSpain;
                        if(model.IconInButtonFile != null)
                        {
                            quizCategory.IconInButtonName = helper.GetFileName(model.IconInButtonFile);
                            await helper.FileUploadAsync(model.IconInButtonFile, quizCategory.IconInButtonName, "QuizCategory");
                        }
                        if(model.IconInPartyFile != null)
                        {
                            quizCategory.IconInButtonName = helper.GetFileName(model.IconInPartyFile);
                            await helper.FileUploadAsync(model.IconInPartyFile, quizCategory.IconInButtonName, "QuizCategory");
                        }
                        _quizCategoryService.Update(quizCategory);
                        _quizCategoryService.CommitTransaction();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    _quizCategoryService.RollbackTransaction();
                    if (!QuizCategoryExists(model.Id))
                    {
                        return NotFound();
                    }
                }
                return RedirectToAction(nameof(Index), new { id = model.GameId });
            }
            return View(model);
        }

        // GET: BackOffice/QuizCategory/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quizCategory = _quizCategoryService.Get(m => m.Id == id);
            if (quizCategory == null)
            {
                return NotFound();
            }

            return View(quizCategory);
        }

        // POST: BackOffice/QuizCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var quizCategory = _quizCategoryService.Get(x => x.Id == id);
            _quizCategoryService.Delete(quizCategory);
            return RedirectToAction(nameof(Index));
        }

        private bool QuizCategoryExists(int id)
        {
            return _quizCategoryService.Get(e => e.Id == id) != null;
        }
    }
}
