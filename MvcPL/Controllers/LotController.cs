using BLL.Interface.Entities;
using BLL.Interface.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models.Bid;
using MvcPL.Models.Lot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace MvcPL.Controllers
{
    public class LotController : Controller
    {
        private IService<BllBid> bidService;
        private ILotService lotService;
        private IService<BllModerationStatus> moderationStatusService;
        private IService<BllCategory> categoryService;
        private IService<BllUser> userService;

        public LotController(IService<BllBid> bidService, ILotService lotService, IService<BllModerationStatus> moderationStatusService, IService<BllCategory> categoryService, IService<BllUser> userService)
        {
            this.bidService = bidService;
            this.lotService = lotService;
            this.moderationStatusService = moderationStatusService;
            this.categoryService = categoryService;
            this.userService = userService;
        }

        public ActionResult Index(int? categoryId, int? userId)
        {
            IEnumerable<LotViewModel> viewItems = lotService.
                GetByCategoryIdOrOwnerId(categoryId, userId).
                Select(l => l.ToLotViewModel());

            if (HttpContext.Request.IsAjaxRequest())
            {
                return PartialView("_List", viewItems);
            }
            else
            {
                return View(viewItems);
            }
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public ActionResult Create()
        {
            ViewBag.Categories = new SelectList(categoryService.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LotCreateModel item)
        {
            if (ModelState.IsValid)
            {
                BllUser bllUser = userService.GetByPredicate(u => u.Email == HttpContext.User.Identity.Name).FirstOrDefault();

                if (bllUser == null)
                {
                    ModelState.AddModelError(string.Empty, "Cannot create new lot");
                }
                else if (item.FinishDateTime < DateTime.Now)
                {
                    ModelState.AddModelError(string.Empty, "Wrong finish date time");
                }
                else
                {
                    BllLot bllLot = item.ToBllLot();
                    bllLot.ModerationDateTime = DateTime.Now;
                    bllLot.Sold = false;
                    bllLot.OwnerId = bllUser.Id;
                    bllLot.ModerationStatusId = (int)ModerationStatus.Unchecked;
                    bllLot.ModeratorId = 2;

                    lotService.Create(bllLot);
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Categories = new SelectList(categoryService.GetAll(), "Id", "Name");
            return View(item);
        }

    }
}
