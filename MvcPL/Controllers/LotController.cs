using BLL.Interface.Entities;
using BLL.Interface.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models.Bid;
using MvcPL.Models.Lot;
using MvcPL.Pagination;
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
        private const int PageSize = 1;

        private IService<BllBid> bidService;
        private ILotService lotService;
        private IService<BllModerationStatus> moderationStatusService;
        private IService<BllCategory> categoryService;
        private IUserService userService;

        public LotController(IService<BllBid> bidService, ILotService lotService, IService<BllModerationStatus> moderationStatusService, IService<BllCategory> categoryService, IUserService userService)
        {
            this.bidService = bidService;
            this.lotService = lotService;
            this.moderationStatusService = moderationStatusService;
            this.categoryService = categoryService;
            this.userService = userService;
        }

        /*
        public ActionResult Index(int? categoryId, int? userId)
        {
            IEnumerable<LotViewModel> viewItems = lotService
                .GetByCategoryIdOrOwnerId(categoryId, userId)
                //.GetByPredicate( (l) => categoryId.HasValue && l.CategoryId == categoryId || userId.HasValue && l.OwnerId == userId ||  )
                .Select(l => l.ToLotViewModel());

            if (HttpContext.Request.IsAjaxRequest())
            {
                return PartialView("_List", viewItems);
            }
            else
            {
                return View(viewItems);
            }
        }
        */
        
        public ActionResult Index(int page = 1)
        {
            ViewBag.UserId = GetCurrntUserId();

            int itemsCount = lotService.Count(l => l.FinishDateTime > DateTime.Now);

            IEnumerable<LotViewModel> viewItems = lotService
                .GetByPredicate( l => l.FinishDateTime > DateTime.Now  )
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .Select(l => l.ToLotViewModel());
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = LotController.PageSize, TotalItems = itemsCount };
            ViewBag.PageInfo = pageInfo;
                


            if (HttpContext.Request.IsAjaxRequest())
            {
                return PartialView("_List", viewItems.ToPaginationList(pageInfo));
            }
            else
            {
                return View(viewItems.ToPaginationList(pageInfo));
            }
        }

        /*
        [Authorize(Roles = "User")]
        public ActionResult Owner()
        {
            // name == email????
            BllUser bllUser = userService.GetByPredicate(u => u.Email == User.Identity.Name).FirstOrDefault();

            if (bllUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = bllUser.Id;

            IEnumerable<LotViewModel> viewItems = lotService
                .GetByPredicate(l => l.OwnerId == bllUser.Id)
                .Select(l => l.ToLotViewModel());

            if (HttpContext.Request.IsAjaxRequest())
            {
                return PartialView("_List", viewItems);
            }
            else
            {
                return View("Index", viewItems);
            }
        }
        */

        public ActionResult Search(string searchString, int? userId, int? categoryId)
        {
            return search(searchString, userId, categoryId);
        }

        [Authorize(Roles = "User")]
        public ActionResult My()
        {
            int userId = GetCurrntUserId();
            ViewBag.UserId = userId;

            IEnumerable<LotViewModel> viewItems = lotService
                .GetByPredicate(l => l.OwnerId == userId)
                .Select(l => l.ToLotViewModel());

            if (HttpContext.Request.IsAjaxRequest())
            {
                return PartialView("_List", viewItems);
            }
            else
            {
                return View("Index", viewItems);
            }
        }

        private ActionResult search(string searchString = null, int? userId = null, int? categoryId = null)
        {
            ViewBag.UserId = GetCurrntUserId();

            BllModerationStatus moderationStatus = moderationStatusService.GetByPredicate(ms => ms.Name == "Проверен").FirstOrDefault();
            if (moderationStatus == null)
                return HttpNotFound();
            IEnumerable<LotViewModel> viewItems = lotService
                .GetByPredicate(l =>
                       (searchString == null || searchString != null && l.Name.Contains(searchString))
                    && (!userId.HasValue     || userId.HasValue      && l.OwnerId == userId)
                    && (!categoryId.HasValue || categoryId.HasValue  && l.CategoryId == categoryId)
                    && l.FinishDateTime > DateTime.Now)
                // убрать Where
                .Where(l => l.ModerationStatus.Id == moderationStatus.Id)
                .Select(l => l.ToLotViewModel());

            if (HttpContext.Request.IsAjaxRequest())
            {
                return PartialView("_List", viewItems);
            }
            else
            {
                return View("Index", viewItems);
            }
        }

        private int GetCurrntUserId()
        {
            int result = 0;
            if (User.Identity.IsAuthenticated)
            {
                BllUser bllUser = userService.GetByPredicate(u => u.Email == User.Identity.Name).FirstOrDefault();
                result = bllUser != null ? bllUser.Id : 0;
            }
            return result;
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
        public ActionResult Create(LotCreateModel item, HttpPostedFileBase uploadImage)
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

                    if (uploadImage != null)
                    {
                        byte[] imageData = null;
                        using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                        {
                            imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                        }
                        bllLot.Image = imageData;
                    }

                    // убрать пкркчисление
                    //bllLot.ModerationStatusId = (int)ModerationStatus.Unchecked;
                    BllModerationStatus moderationStatus = moderationStatusService.GetByPredicate(ms => ms.Name == "Не проверен").FirstOrDefault();
                    if (moderationStatus == null)
                        HttpNotFound();
                    bllLot.ModerationStatus = moderationStatus;
                    bllLot.ModeratorId = 2;

                    lotService.Create(bllLot);
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Categories = new SelectList(categoryService.GetAll(), "Id", "Name");
            return View(item);
        }

        public ActionResult Details(int id)
        {
            BllLot lot = lotService.GetById(id);

            if (lot == null)
            {
                return HttpNotFound();
            }

            BllUser user = userService.GetByPredicate(u => u.Email == User.Identity.Name).FirstOrDefault();
            ViewBag.OwnerView = user != null && user.Id == lot.OwnerId;

            LotViewModel viewLot = lot.ToLotViewModel();
            viewLot.Category = categoryService.GetById(lot.CategoryId).ToCategoryViewModel();
            return View(viewLot);
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public ActionResult Edit(int id)
        {
            // Проверка isOwner
            BllLot bllLot = lotService.GetById(id);

            if (bllLot == null)
            {
                return HttpNotFound();
            }

            ViewBag.Categories = categoryService.GetAll();
            return View(bllLot.ToLotEditModel());
           
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LotEditModel lot, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {
                BllLot bllLot = lotService.GetById(lot.Id);

                if (bllLot == null)
                {
                    return HttpNotFound();
                }
                else if (uploadImage != null && !IsImage(uploadImage))
                {
                    ModelState.AddModelError(string.Empty, "This file is not image");
                }
                else
                {
                    bllLot.Name = lot.Name;
                    bllLot.Description = lot.Description;
                    bllLot.CategoryId = lot.CategoryId;

                    if (uploadImage != null)
                    {
                        byte[] imageData = null;
                        using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                        {
                            imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                        }
                        bllLot.Image = imageData;
                    }

                    BllModerationStatus moderationStatus = moderationStatusService.GetByPredicate(ms => ms.Name == "Не проверен").FirstOrDefault();
                    if (moderationStatusService == null)
                        return HttpNotFound();
                    bllLot.ModerationStatus = moderationStatus;
                    lotService.Update(bllLot);

                    return RedirectToAction("Details", new { id = lot.Id });
                }
            }

            ViewBag.Categories = categoryService.GetAll();
            return View(lot);
        }

        //[HttpPost]
        [Authorize(Roles = "User")]
        public ActionResult Delete(int id)
        {
            BllLot bllLot = lotService.GetById(id);

            if (bllLot == null)
            {
                return HttpNotFound();
            }

            // Нужна ли проверка на NULL пользователя если он авторизован ????????
            BllUser bllUser = userService.GetByPredicate(u => u.Email == User.Identity.Name).FirstOrDefault();
            if (bllUser == null || bllLot.OwnerId != bllUser.Id)
            {
                return HttpNotFound();
            }
            lotService.Delete(bllLot);
            return new EmptyResult();
        }

        [HttpGet]
        [Authorize(Roles = "Moderator")]
        public ActionResult Unmoderated()
        {
            BllUser bllUser = userService.GetByPredicate(u => u.Email == User.Identity.Name).FirstOrDefault();
            if (bllUser == null)
            {
                return HttpNotFound();
            }

            BllModerationStatus moderationStatus = moderationStatusService.GetByPredicate(ms => ms.Name == "Не проверен").FirstOrDefault();
            if (moderationStatus == null)
            {
                return HttpNotFound();
            }

            IEnumerable<LotViewModel> unmoderatedLots = lotService
                //.GetByPredicate(l => l.ModerationStatus.Name == moderationStatus.Name)
                .GetUnmoderatedLots()
                .Select(l => l.ToLotViewModel());

            return View("Index", unmoderatedLots);
        }

        [HttpGet]
        [Authorize(Roles = "Moderator")]
        public ActionResult RecentlyModerated()
        {
            BllUser bllUser = userService.GetByPredicate(u => u.Email == User.Identity.Name).FirstOrDefault();
            if (bllUser == null)
            {
                return HttpNotFound();
            }

            IEnumerable<LotViewModel> recentlyModeratedLots = lotService
                .GetByPredicate(l => l.ModeratorId == bllUser.Id).OrderBy(l => l.ModerationDateTime)
                .Select(l => l.ToLotViewModel());

            return View("Index", recentlyModeratedLots);
        }

        [Authorize(Roles = "Moderator")]
        public ActionResult SetModerationStatus(int id, int moderationStatusId, string moderatorMessage)
        {
            BllUser moderator = userService.GetByPredicate(u => u.Email == User.Identity.Name).FirstOrDefault();
            if (moderator == null)
            {
                return HttpNotFound();
            }

            BllLot lot = lotService.GetById(id);
            if (lot == null)
                return HttpNotFound();

            BllModerationStatus moderationStatus = moderationStatusService.GetById(moderationStatusId);
            if (moderationStatus == null)
                return HttpNotFound();

            if (moderationStatus.Id == (int)ModerationStatus.Invalid && String.IsNullOrWhiteSpace(moderatorMessage))
            {
                return HttpNotFound();
            }

            lot.ModeratorId = moderator.Id;
            //lot.ModerationStatusId = moderationStatus.Id;
            lot.ModerationStatus = moderationStatus;
            lot.ModerationDateTime = DateTime.Now;
            lot.ModeratorMessage = moderatorMessage;

            lotService.Update(lot);
            return RedirectToAction("RecentlyModerated");
        }

        private bool IsImage(HttpPostedFileBase file)
        {
            if (file.ContentType.Contains("image"))
            {
                return true;
            }

            string[] formats = new string[] { ".jpg", ".png", ".gif", ".jpeg", ".svg" };

            return formats.Any(item => file.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase));
        }
    }
}
