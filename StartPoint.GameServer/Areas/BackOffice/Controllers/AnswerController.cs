using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using DomainEntities;
using Microsoft.AspNetCore.Authorization;
using StartPoint.GameServer.Areas.BackOffice.Models;

namespace StartPoint.GameServer.Areas.BackOffice.Controllers
{
    [Area("BackOffice")]
    [Authorize]
    public class AnswerController : Controller
    {
        private IAnswerService _answerService;
        private Helpers.Helper helper;

        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;
            helper = new Helpers.Helper();
        }

        // GET: BackOffice/Answer
        public async Task<IActionResult> Index()
        {
            var apiDbContext = _answerService.List(x=> true).Include(a => a.Question);
            return View(apiDbContext.ToListAsync());
        }

        // GET: BackOffice/Answer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = _answerService.List(m => m.Id == id)
                .Include(a => a.Question)
                .FirstOrDefault();
            if (answer == null)
            {
                return NotFound();
            }

            return View(answer);
        }

        // GET: BackOffice/Answer/Create
        public IActionResult Create(int id, int quizCategoryId)
        {
            return View(new AnswerViewModel
            {
                QuestionId = id,
                QuizCategoryId = quizCategoryId,
                Answers = _answerService.List(x => x.QuestionId == id).ToList()
            });
        }

        // POST: BackOffice/Answer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AnswerViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.Answers == null)
                        model.Answers = new List<Answer>();

                    _answerService.BeginTransaction();
                    var answer = helper.GetAnswer(model);
                    answer.Id = _answerService.Add(answer);
                    if (answer.Id < 0)
                        throw new Exception("database error");
                    model.Answers.AddRange(_answerService.List(x => x.QuestionId == model.QuestionId));
                    _answerService.CommitTransaction();
                    return View(nameof(Create), model);
                }
                catch (Exception ex)
                {
                    _answerService.RollbackTransaction();
                }
            }
            return View(model);
        }

        // GET: BackOffice/Answer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = _answerService.Get(x=> x.Id == id);
            if (answer == null)
            {
                return NotFound();
            }
            return View(answer);
        }

        // POST: BackOffice/Answer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AnswerInEnglish,AnswerInFrench,AnswerInGerman,AnswerInSpain,AnswerInItalian,IsTrue,QuestionId")] Answer answer)
        {
            if (id != answer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _answerService.Update(answer);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnswerExists(answer.Id))
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
            return View(answer);
        }

        // GET: BackOffice/Answer/Delete/5
        public IActionResult Delete(int id, int questionId)
        {
            var question = _answerService.Get(m => m.Id == id);
            if (question == null)
            {
                return NotFound();
            }
            _answerService.Delete(question);
            return View("Create", new AnswerViewModel
            {
                QuestionId = questionId,
                Answers = _answerService.List(x => x.QuestionId == questionId).ToList()
            });

        }

        // POST: BackOffice/Answer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var answer = _answerService.Get(x=> x.Id == id);
            _answerService.Delete(answer);
            return RedirectToAction(nameof(Index));
        }

        private bool AnswerExists(int id)
        {
            return _answerService.Get(e => e.Id == id) != null;
        }
    }
}
