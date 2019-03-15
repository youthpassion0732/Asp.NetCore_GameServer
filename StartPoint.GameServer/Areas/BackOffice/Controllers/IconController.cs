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
using StartPoint.GameServer.Areas.BackOffice.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace StartPoint.GameServer.Controllers
{
    [Area("BackOffice")]
    [Authorize]
    public class IconController : Controller
    {
        private IIconService _iconService;
        private Helpers.Helper helper;

        public IconController(IIconService iconService)
        {
            _iconService = iconService;
            helper = new Helpers.Helper();
        }

        // GET: Icon
        public IActionResult Index()
        {
            return View(_iconService.Get());
        }

        // GET: Icon/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var icon = _iconService.Get(m => m.Id == id);
            if (icon == null)
            {
                return NotFound();
            }

            return View(icon);
        }

        // GET: Icon/Create
        public IActionResult Create(int id)
        {
            return View(new IconViewModel
            {
                GameId = id,
                Icons = _iconService.List(x => x.GameId == id).ToList()
            });
        }

        // POST: Icon/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IconViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.Icons == null)
                        model.Icons = new List<Icon>();
                    if (_iconService.Get(x => x.GameId == model.GameId && x.Value == model.Value) != null)
                    {
                        ModelState.AddModelError("Value", "Value must be unique");
                        model.Icons.AddRange(_iconService.List(x => x.GameId == model.GameId));
                        return View(model);
                    }
                    _iconService.BeginTransaction();
                    var icon = helper.GetIcon(model);
                    icon.Id = _iconService.Add(icon);
                    if (icon.Id < 0)
                        throw new Exception("database error");
                    await helper.FileUploadAsync(model.IconFile, icon.IconName, "Icon");
                    model.Icons.AddRange(_iconService.List(x => x.GameId == model.GameId));
                    _iconService.CommitTransaction();
                    return View(nameof(Create), model);
                }
                catch (Exception ex)
                {
                    _iconService.RollbackTransaction();
                }
            }
            return View(model);
        }

        // GET: Icon/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var icon = _iconService.Get(x => x.Id == id);
            if (icon == null)
            {
                return NotFound();
            }
            return View(icon);
        }

        // POST: Icon/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Value1,Icon1,Value2,Icon2,Value3,Icon3,Value4,Icon4,Value5,Icon5,Value6,Icon6,Value7,Icon7,Value8,Icon8,Value9,Icon9")] Icon icon)
        {
            if (id != icon.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _iconService.Update(icon);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IconExists(icon.Id))
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
            return View(icon);
        }

        // GET: Icon/Delete/5
        public IActionResult Delete(int id, int GameId)
        {
            var icon = _iconService.Get(m => m.Id == id);
            if (icon == null)
            {
                return NotFound();
            }
            _iconService.Delete(icon);
            return View("Create", new IconViewModel
            {
                GameId = GameId,
                Icons = _iconService.List(x => x.GameId == GameId).ToList()
            });

        }

        //// POST: Icon/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public IActionResult DeleteConfirmed(int id)
        //{
        //    var icon = _iconService.Get(x => x.Id == id);
        //    _iconService.Delete(icon);
        //    return RedirectToAction(nameof(Index));
        //}

        private bool IconExists(int id)
        {
            return _iconService.Get(e => e.Id == id) != null;
        }
    }
}
