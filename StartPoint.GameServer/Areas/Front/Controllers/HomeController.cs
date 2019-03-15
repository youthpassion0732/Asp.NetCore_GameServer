using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DAL;
using DomainEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StartPoint.GameServer.Areas.Front.Models;

namespace StartPoint.GameServer.Areas.Front.Controllers
{
    [Area("Front")]
    [Authorize]
    public class HomeController : Controller
    {
        private IGameService _gameService;
        private IIconService _iconService;
        private IQuizCategoryService _quizCategoryService;
        private IOfferService _offerService;
        private IGameUserService _gameUserService;
        private IQuestionService _questionService;
        private IAnswerService _answerService;
        private IQuizHistoryService _quizHistoryService;
        private IQuizSummaryService _quizSummaryService;
        private readonly IConfiguration _configuration;

        public HomeController(IGameService gameService, IIconService iconService, IQuizCategoryService quizCategoryService, IOfferService offerService, IGameUserService gameUserService, IQuestionService questionService, IAnswerService answerService, IQuizHistoryService quizHistoryService, IQuizSummaryService quizSummaryService, IConfiguration configuration)
        {
            _gameService = gameService;
            _iconService = iconService;
            _quizCategoryService = quizCategoryService;
            _offerService = offerService;
            _gameUserService = gameUserService;
            _questionService = questionService;
            _answerService = answerService;
            _quizHistoryService = quizHistoryService;
            _quizSummaryService = quizSummaryService;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var model = SetHomeModel();
            if (model.GameModel != null && model.GameModel.AllowedToPlay)
            {
                if (model.GameModel.GameType == (int)Game.GameTypeEnum.Quiz && model.QuizCategories != null && model.QuizCategories.Count == 1)
                {
                    var categoryId = model.QuizCategories.FirstOrDefault().Id;
                    return RedirectToAction("QuizQuestion", new { category = categoryId });
                }

                return View(model);
            }
            else
            {
                return RedirectToAction("Error", new { error = "You are not allowed to play the game at the moment." });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SlotGameWin()
        {
            int.TryParse(Request.Form["SlotMachineOptions"], out int slotCode);
            int.TryParse(Request.Form["GameId"], out int gameId);

            if (gameId > 0 && slotCode > 0)
            {
                // Add/Update Game User
                AddUpdateGameUser(gameId);

                Offer offer = _offerService.Get(x => x.GameId == gameId && x.SlotCode == slotCode);
                if (offer != null)
                {
                    OfferViewModel model = new OfferViewModel
                    {
                        OfferId = offer.Id,
                        GameId = offer.GameId,
                        VideoName = offer.VideoName,
                        VideoURL = offer.VideoURL,
                        Description = offer.Description,
                        Photo1 = offer.Photo1,
                        Photo2 = offer.Photo2,
                        Photo3 = offer.Photo3,
                        Photo4 = offer.Photo4,
                        Photo5 = offer.Photo5,
                        SlotCode = offer.SlotCode
                    };

                    return View(model);
                }
                else
                {
                    return RedirectToAction("SlotGameLost");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public IActionResult SlotGameLost()
        {
            return View();
        }

        [HttpGet]
        public IActionResult QuizQuestion(int category)
        {
            return View(SetQuizQuestionModel(category));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult QuizQuestion(QuizQuestionViewModel model)
        {
            bool isCorrectAnswer = false;
            int score = model.CurrentScore;

            if (model.SelectedAnswerId != null)
            {
                isCorrectAnswer = _answerService.Get(x => x.Id == model.SelectedAnswerId).IsTrue;
                if (isCorrectAnswer)
                {
                    score += model.CurrentQuestionScore;
                }
            }

            _quizHistoryService.Add(new QuizHistory
            {
                AnswerId = model.SelectedAnswerId,
                CreateDateTime = DateTime.Now,
                IsCorrectAnswer = isCorrectAnswer,
                QuestionId = model.QuestionId,
                SessionId = model.SessionId,
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                QuizCategoryId = model.CategoryId
            });

            if (model.IsLastQuestion)
            {
                var isPass = (int)Math.Round((double)(100 * score) / model.TotalScore) >= int.Parse(_configuration["PassingPercentageForQuiz"]);
                _quizSummaryService.Add(new QuizSummary
                {
                    IsPass = isPass,
                    ObtainedScore = score,
                    QuizCategoryId = model.CategoryId,
                    SessionId = model.SessionId,
                    TotalQuestions = model.TotalQuestions,
                    TotalScore = model.TotalScore,
                    UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                    CreateDateTime = DateTime.Now
                });
                return RedirectToAction("QuizResult", new ResultViewModel { IsPass = isPass, ObtainedScore = score, TotalScore = model.TotalScore });
            }

            var currentSessionId = model.SessionId;
            var totalScore = model.TotalScore;
            model = SetQuizQuestionModel(model.CategoryId, model.QuestionNumber);
            model.CurrentScore = score;
            model.SessionId = currentSessionId;
            model.TotalScore = totalScore + model.CurrentQuestionScore;

            return View(model);
        }

        public IActionResult QuizResult(ResultViewModel model)
        {
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string error = "")
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = error });
        }

        private HomeViewModel SetHomeModel()
        {
            HomeViewModel model = new HomeViewModel();

            Game gameInfo = _gameService.Get().FirstOrDefault();
            if (gameInfo != null)
            {
                model.GameModel = new GameViewModel
                {
                    GameId = gameInfo.Id,
                    GameType = gameInfo.GameType,
                    GameName = gameInfo.GameName,
                    GameDescription = gameInfo.GameDescription,
                    GameLogo = gameInfo.Logo,
                    GameBanner = gameInfo.Banner,
                    GameCSSSkin = gameInfo.CSSSkin,

                    GameSlotType = gameInfo.GameSlotType,
                    GameSlotsCount = gameInfo.GameSlotsCount,

                    EnableTimer = gameInfo.EnableTimer,
                    TimerMaxPerQuestion = gameInfo.TimerMaxPerQuestion,
                    DisplayScore = gameInfo.DisplayScore
                };

                if (model.GameModel.GameType == (int)Game.GameTypeEnum.Slot)
                {
                    model.GameModel.SlotIcons = _iconService.List(x => x.GameId == model.GameModel.GameId).ToList();
                    model.GameModel.AllowedToPlay = ValidateGameSlotType(gameInfo);
                }
                else
                {
                    model.QuizCategories = _quizCategoryService.List(x => x.GameId == model.GameModel.GameId).ToList();
                    model.GameModel.AllowedToPlay = true;
                }
            }

            return model;
        }

        private QuizQuestionViewModel SetQuizQuestionModel(int category, int skip = 0)
        {
            QuizQuestionViewModel model = new QuizQuestionViewModel();

            var questions = _questionService.List(x => x.QuizCategoryId == category).OrderBy(x => x.Id).ToList();
            if (questions != null)
            {
                int totalQuestions = questions.Count;
                if (totalQuestions > 0)
                {
                    var nextQuestion = questions.Skip(skip).FirstOrDefault();
                    if (nextQuestion != null)
                    {
                        var answers = _answerService.List(x => x.QuestionId == nextQuestion.Id).ToList();
                        int questionNumber = 1 + skip;

                        model = new QuizQuestionViewModel
                        {
                            CategoryId = category,
                            Question = nextQuestion,
                            Answers = answers,
                            QuestionNumber = questionNumber,
                            CurrentScore = 0,
                            TotalQuestions = totalQuestions,
                            SessionId = HttpContext.Session.Id,
                            QuestionId = nextQuestion.Id,
                            CurrentQuestionScore = nextQuestion.ScoreToWin,
                            TotalScore = nextQuestion.ScoreToWin
                        };
                    }
                }
            }

            return model;
        }

        private bool ValidateGameSlotType(Game gameInfo)
        {
            bool isValid = false;

            GameUser gameUser = _gameUserService.Get(x => x.UserId == User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (gameUser != null && gameUser.LastPlayedDate != null)
            {
                TimeSpan diffDate = DateTime.UtcNow - Convert.ToDateTime(gameUser.LastPlayedDate);
                switch (gameInfo.GameSlotType)
                {
                    case (int)Game.GameSlotTypeEnum.OncePerWeek:
                        isValid = diffDate.Days > 7;
                        break;
                    case (int)Game.GameSlotTypeEnum.OncePerMonth:
                        isValid = diffDate.Days > 30;
                        break;
                    case (int)Game.GameSlotTypeEnum.OncePerDay:
                        isValid = diffDate.TotalHours > 24;
                        break;
                    default:
                        isValid = true;
                        break;
                }
            }
            else
            {
                isValid = true;
            }

            return isValid;
        }

        private void AddUpdateGameUser(int gameId)
        {
            string currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            GameUser gameUser = _gameUserService.Get(x => x.UserId == currentUserId);
            if (gameUser == null)
            {
                gameUser = new GameUser
                {
                    GameId = gameId,
                    UserId = currentUserId,
                    LastPlayedDate = DateTime.UtcNow
                };
                _gameUserService.Add(gameUser);
            }
            else
            {
                gameUser.LastPlayedDate = DateTime.UtcNow;
                _gameUserService.Update(gameUser);
            }
        }
    }
}
