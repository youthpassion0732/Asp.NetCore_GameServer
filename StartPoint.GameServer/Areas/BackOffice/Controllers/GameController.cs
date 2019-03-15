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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace StartPoint.GameServer.Areas.BackOffice.Controllers
{
    [Area("BackOffice")]
    [Authorize]
    public class GameController : Controller
    {
        private IGameService _gameService;
        private Game game;
        private Helper helper;
        private readonly IConfiguration Configuration;

        public GameController(IGameService gameService, IConfiguration configuration)
        {
            _gameService = gameService;
            game = new Game();
            helper = new Helper();
            Configuration = configuration;
        }

        // GET: Game
        public IActionResult Index()
        {
            int maxLimitForGames = int.Parse(Configuration["MaxLimitForGames"]);
            var games = _gameService.Get();
            if (games == null || (games != null && games.Count < maxLimitForGames))
            {
                ViewBag.AllowCreateGame = true;
            }
            else
            {
                ViewBag.AllowCreateGame = false;
            }
            return View(games);
        }

        // GET: Game/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = _gameService.Get(m => m.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // GET: Game/Create
        public IActionResult Create(int? id)
        {
            if (id == null)
            {
                return View(new GameViewModel { Id = 0 });
            }

            var game = _gameService.Get(m => m.Id == id);
            if (game == null)
            {
                return View(new GameViewModel { Id = 0 });
            }

            return View(helper.GetGameViewModel(game, true));
        }

        // POST: Game/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync([Bind("Id,GameType,GameName,GameDescription,LoginRule,LoginWithFacbook,LoginWithGoogle,LoginWithTwitter,Logo,Banner,CSSSkin,PrivacyAndGameConditions,EnableAnonymous,GameSlotType,EnableTimer,TimerMaxPerQuestion,DisplayScore")] GameViewModel model)
        {
            if (ModelState.IsValid || model.Id > 0)
            {
                try
                {
                    if (model.GameType == (int)Game.GameTypeEnum.Slot && model.GameSlotType == 0)
                    {
                        ModelState.AddModelError("GameSlotType", "must select 1 field");
                        return View("Create", model);
                    }

                    if (model.LoginRule == (int)Game.LoginRuleTypeEnum.SocialMediaLogin && !(model.LoginWithFacbook || model.LoginWithGoogle || model.LoginWithTwitter))
                    {
                        ModelState.AddModelError("LoginRule", "must check 1 field for socialmedia login");
                        return View("Create", model);
                    }
                    _gameService.BeginTransaction();
                    var game = _gameService.Get(x => x.Id == model.Id);
                    if (game != null)
                    {
                        game.DisplayScore = model.DisplayScore;
                        game.EnableAnonymous = model.EnableAnonymous;
                        game.EnableTimer = model.EnableTimer;
                        game.GameDescription = model.GameDescription;
                        game.GameName = model.GameName;
                        game.GameSlotType = model.GameSlotType;
                        game.GameType = model.GameType;
                        game.LoginRule = model.LoginRule;
                        game.LoginWithFacbook = model.LoginWithFacbook;
                        game.LoginWithGoogle = model.LoginWithGoogle;
                        game.LoginWithTwitter = model.LoginWithTwitter;
                        game.PrivacyAndGameConditions = model.PrivacyAndGameConditions;
                        game.TimerMaxPerQuestion = model.TimerMaxPerQuestion;
                        game.GameSlotsCount = model.GameSlotsCount;

                        if (model.Banner != null)
                        {
                            game.Banner = helper.GetFileName(model.Banner);
                            await helper.FileUploadAsync(model.Banner, game.Banner, "Game");
                        }
                        if (model.Logo != null)
                        {
                            game.Logo = helper.GetFileName(model.Logo);
                            await helper.FileUploadAsync(model.Logo, game.Logo, "Game");
                        }
                        if (model.CSSSkin != null)
                        {
                            game.CSSSkin = helper.GetFileName(model.CSSSkin);
                            await helper.FileUploadAsync(model.CSSSkin, game.CSSSkin, "Game");
                        }
                        _gameService.Update(game);
                    }
                    else if (ModelState.IsValid)
                    {
                        var newGame = helper.GetGame(model);
                        model.Id = _gameService.Add(newGame);
                        await helper.FileUploadAsync(model.Banner, newGame.Banner, "Game");
                        await helper.FileUploadAsync(model.Logo, newGame.Logo, "Game");
                        await helper.FileUploadAsync(model.CSSSkin, newGame.CSSSkin, "Game");
                    }
                    _gameService.CommitTransaction();
                    if (model.GameType == (int)Game.GameTypeEnum.Slot)
                        return Redirect($"~/BackOffice/Offer/Index?GameId={model.Id}");
                    else
                        return Redirect($"~/BackOffice/QuizCategory/Index?id={model.Id}");
                }
                catch (Exception ex)
                {
                    _gameService.RollbackTransaction();
                }
            }
            return View(model);
        }

        // GET: Game/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = _gameService.Get(x => x.Id == id.Value);
            if (game == null)
            {
                return NotFound();
            }
            return View(game);
        }

        // POST: Game/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,GameType,GameName,GameDescription,LoginRule,LoginWithFacbook,LoginWithGoogle,LoginWithTwitter,Logo,Banner,CSSSkin,PrivacyAndGameConditions,EnableAnonymous,GameSlotType,EnableTimer,TimerMaxPerQuestion,DisplayScore")] GameViewModel game)
        {
            if (id != game.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _gameService.Update(helper.GetGame(game));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.Id))
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
            return View(game);
        }

        // GET: Game/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = _gameService.Get(m => m.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Game/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var game = _gameService.Get(x => x.Id == id);
            _gameService.Delete(game);
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(int id)
        {
            return _gameService.Get(e => e.Id == id) != null;
        }
    }
}
