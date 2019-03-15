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

namespace StartPoint.GameServer.Areas.BackOffice.Controllers
{
    [Area("BackOffice")]
    [Authorize]
    public class OfferController : Controller
    {
        private IOfferService _offerService;
        private Helper helper;

        public OfferController(IOfferService offerService)
        {
            _offerService = offerService;
            helper = new Helper();
        }

        // GET: Offers
        public IActionResult Index(int GameId)
        {
            var offer = _offerService.Get(x => x.GameId == GameId);
            return View(offer == null ? new Offer { GameId = GameId } : offer);
        }

        // GET: Offers/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offer = _offerService.Get(m => m.Id == id);
            if (offer == null)
            {
                return NotFound();
            }

            return View(offer);
        }

        // GET: Offers/Create
        public IActionResult Create(int id)
        {
            var offerViewModel = _offerService.Get(x => x.GameId == id);
            return View(helper.GetOfferViewModel(offerViewModel, true) == null ? new OfferViewModel { GameId = id } : helper.GetOfferViewModel(offerViewModel, true));
        }

        // POST: Offers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VideoFile,VideoURL,Description,Photo1,Photo2,Photo3,Photo4,Photo5,SlotCode,GameId")] OfferViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _offerService.BeginTransaction();
                    var offer = _offerService.Get(x => x.GameId == model.GameId);
                    if (offer != null)
                    {
                        offer.Description = model.Description;
                        offer.SlotCode = model.SlotCode;
                        offer.VideoURL = model.VideoURL;
                        if (model.Photo1 != null)
                        {
                            offer.Photo1 = helper.GetFileName(model.Photo1);
                            await helper.FileUploadAsync(model.Photo1, offer.Photo1, "Offer");
                        }
                        if (model.Photo2 != null)
                        {
                            offer.Photo2 = helper.GetFileName(model.Photo2);
                            await helper.FileUploadAsync(model.Photo2, offer.Photo2, "Offer");
                        }
                        if (model.Photo3 != null)
                        {
                            offer.Photo3 = helper.GetFileName(model.Photo3);
                            await helper.FileUploadAsync(model.Photo3, offer.Photo3, "Offer");
                        }
                        if (model.Photo4 != null)
                        {
                            offer.Photo4 = helper.GetFileName(model.Photo4);
                            await helper.FileUploadAsync(model.Photo4, offer.Photo4, "Offer");
                        }
                        if (model.Photo5 != null)
                        {
                            offer.Photo5 = helper.GetFileName(model.Photo5);
                            await helper.FileUploadAsync(model.Photo5, offer.Photo5, "Offer");
                        }
                        if (model.VideoFile != null)
                        {
                            offer.VideoName = helper.GetFileName(model.VideoFile);
                            await helper.FileUploadAsync(model.VideoFile, offer.VideoName, "Offer");
                        }
                        _offerService.Update(offer);
                    }
                    else
                    {
                        var newOffer = helper.GetOffer(model);
                        newOffer.Id = _offerService.Add(newOffer);
                        await helper.FileUploadAsync(model.Photo1, newOffer.Photo1, "Offer");
                        await helper.FileUploadAsync(model.Photo2, newOffer.Photo2, "Offer");
                        await helper.FileUploadAsync(model.Photo3, newOffer.Photo3, "Offer");
                        await helper.FileUploadAsync(model.Photo4, newOffer.Photo4, "Offer");
                        await helper.FileUploadAsync(model.Photo5, newOffer.Photo5, "Offer");
                        await helper.FileUploadAsync(model.VideoFile, newOffer.VideoName, "Offer");
                    }
                    _offerService.CommitTransaction();
                    return RedirectToAction(nameof(Index), new { GameId = model.GameId });
                }
                catch (Exception ex)
                {
                    _offerService.RollbackTransaction();
                }
            }
            return View(model);
        }

        // GET: Offers/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offer = _offerService.Get(x => x.Id == id);
            if (offer == null)
            {
                return NotFound();
            }
            return View(offer);
        }

        // POST: Offers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,VideoName,VideoURL,Description,Photo1,Photo2,Photo3,Photo4,Photo5,SlotCode")] Offer offer)
        {
            if (id != offer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _offerService.Update(offer);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfferExists(offer.Id))
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
            return View(offer);
        }

        // GET: Offers/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offer = _offerService.Get(m => m.Id == id);
            if (offer == null)
            {
                return NotFound();
            }

            return View(offer);
        }

        // POST: Offers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id, int GameId)
        {
            var offer = _offerService.Get(x => x.Id == id);
            _offerService.Delete(offer);
            return RedirectToAction(nameof(Index), new { GameId = GameId });
        }

        private bool OfferExists(int id)
        {
            return _offerService.Get(e => e.Id == id) != null;
        }
    }
}
