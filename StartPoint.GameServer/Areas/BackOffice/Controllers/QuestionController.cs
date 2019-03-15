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
using Microsoft.AspNetCore.Authorization;

namespace StartPoint.GameServer.Areas.BackOffice.Controllers
{
    [Area("BackOffice")]
    [Authorize]
    public class QuestionController : Controller
    {
        private IQuestionService _questionService;
        private Helpers.Helper helper;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
            helper = new Helpers.Helper();
        }

        // GET: BackOffice/Question
        public IActionResult Index(int quizCategoryId)
        {
            ViewBag.QuizCategory = quizCategoryId;
            var questions = _questionService.List(x => x.QuizCategoryId == quizCategoryId).Include(x=> x.QuizCategory).ToList();
            return View(questions == null ? new List<Question>() : questions);
        }

        // GET: BackOffice/Question/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = _questionService.Get(m => m.Id == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // GET: BackOffice/Question/Create
        public IActionResult Create(int id)
        {
            return View(new QuestionViewModel { QuizCategoryId = id });
        }

        // POST: BackOffice/Question/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TitleInEnglish,TitleInFrench,TitleInGerman,TitleInSpain,TitleInItalian,Photo1,Photo2,Photo3,Photo4,Photo5,VideoFile,MaxTimeAllowed,ScoreToWin,QuizCategoryId")] QuestionViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _questionService.BeginTransaction();
                    //var question = _questionService.Get(x => x.GameId == model.GameId);
                    //if (question != null)
                    //{
                    //    question.MaxTimeAllowed = model.MaxTimeAllowed;
                    //    question.ScoreToWin = model.MaxTimeAllowed;
                    //    question.TitleInEnglish = model.TitleInEnglish;
                    //    question.TitleInFrench = model.TitleInFrench;
                    //    question.TitleInGerman = model.TitleInGerman;
                    //    question.TitleInItalian = model.TitleInItalian;
                    //    question.TitleInSpain = model.TitleInSpain;
                    //    if (model.Photo1 != null)
                    //    {
                    //        question.Photo1 = helper.GetFileName(model.Photo1);
                    //        await helper.FileUploadAsync(model.Photo1, question.Photo1, "Question");
                    //    }
                    //    if (model.Photo2 != null)
                    //    {
                    //        question.Photo2 = helper.GetFileName(model.Photo2);
                    //        await helper.FileUploadAsync(model.Photo2, question.Photo2, "Question");
                    //    }
                    //    if (model.Photo3 != null)
                    //    {
                    //        question.Photo3 = helper.GetFileName(model.Photo3);
                    //        await helper.FileUploadAsync(model.Photo3, question.Photo3, "Question");
                    //    }
                    //    if (model.Photo4 != null)
                    //    {
                    //        question.Photo4 = helper.GetFileName(model.Photo4);
                    //        await helper.FileUploadAsync(model.Photo4, question.Photo4, "Question");
                    //    }
                    //    if (model.Photo5 != null)
                    //    {
                    //        question.Photo5 = helper.GetFileName(model.Photo5);
                    //        await helper.FileUploadAsync(model.Photo5, question.Photo5, "Question");
                    //    }
                    //    if (model.VideoFile != null)
                    //    {
                    //        question.VideoName = helper.GetFileName(model.VideoFile);
                    //        await helper.FileUploadAsync(model.VideoFile, question.VideoName, "Question");
                    //    }
                    //    _questionService.Update(question);
                    //}
                    //else
                    {
                        var newQuestion = helper.GetQuestion(model);
                        newQuestion.Id = _questionService.Add(newQuestion);
                        await helper.FileUploadAsync(model.Photo1, newQuestion.Photo1, "Question");
                        await helper.FileUploadAsync(model.Photo2, newQuestion.Photo2, "Question");
                        await helper.FileUploadAsync(model.Photo3, newQuestion.Photo3, "Question");
                        await helper.FileUploadAsync(model.Photo4, newQuestion.Photo4, "Question");
                        await helper.FileUploadAsync(model.Photo5, newQuestion.Photo5, "Question");
                        await helper.FileUploadAsync(model.VideoFile, newQuestion.VideoName, "Question");
                    }
                    _questionService.CommitTransaction();
                    return RedirectToAction(nameof(Index), new { quizCategoryId = model.QuizCategoryId });
                }
                catch (Exception ex)
                {
                    _questionService.RollbackTransaction();
                }
            }
            return View(model);
        }

        // GET: BackOffice/Question/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = _questionService.Get(x=> x.Id == id);
            if (question == null)
            {
                return NotFound();
            }
            return View(question);
        }

        // POST: BackOffice/Question/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TitleInEnglish,TitleInFrench,TitleInGerman,TitleInSpain,TitleInItalian,Photo1,Photo2,Photo3,Photo4,Photo5,VideoFile,MaxTimeAllowed,ScoreToWin,QuizCategoryId")] Question question)
        {
            if (id != question.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _questionService.Update(question);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(question.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(question);
        }

        // GET: BackOffice/Question/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = _questionService.Get(m => m.Id == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // POST: BackOffice/Question/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var question = _questionService.Get(x=> x.Id == id);
            _questionService.Delete(question);
            return RedirectToAction(nameof(Index), new { quizCategoryId = question.QuizCategoryId });
        }

        private bool QuestionExists(int id)
        {
            return _questionService.Get(e => e.Id == id) != null;
        }
    }
}
