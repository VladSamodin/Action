using BLL.Interface.Entities;
using BLL.Interface.Services;
using MvcPL.Models.Bid;
using MvcPL.Infrastructure.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcPL.Controllers
{
    public class BidController : Controller
    {
        private IService<BllBid> bidService;
        private ILotService lotService;
        private IUserService userService;
        
        public BidController(IService<BllBid> bidService, ILotService lotService, IUserService userService)
        {
            this.bidService = bidService;
            this.lotService = lotService;
            this.userService = userService;
        }

        /*
        public ActionResult Index(int lotId)
        {
            if (lotService.GetById(lotId) == null)
            {
                return HttpNotFound();
            }
            IEnumerable<BidViewModel> bids = bidService
                .GetByPredicate(b => b.LotId == lotId)
                .Select(b => new BidViewModel() 
                {
                    Sum = b.Sum,
                    DateTime = b.DateTime,
                    Lot = lotService.GetById(b.LotId).ToLotViewModel(),
                    User = userService.GetById(b.UserId).ToUserViewModel()
                });
            return View(bids);
        }
         * */

        public ActionResult Lot(int lotId)
        {
            if (lotService.GetById(lotId) == null)
            {
                return HttpNotFound();
            }
            IEnumerable<BidViewModel> bids = bidService
                .GetByPredicate(b => b.LotId == lotId)
                .Select(b => new BidViewModel()
                {
                    Sum = b.Sum,
                    DateTime = b.DateTime,
                    Lot = lotService.GetById(b.LotId).ToLotViewModel(),
                    User = userService.GetById(b.UserId).ToUserViewModel()
                });
            return View("Index", bids);
        }

        [Authorize(Roles = "User")]
        public ActionResult My()
        {
            BllUser bllUser = userService.GetByPredicate(u => u.Email == User.Identity.Name).FirstOrDefault();
            if (bllUser == null)
            {
                return HttpNotFound();
            }
            IEnumerable<BidViewModel> bids = bidService
                .GetByPredicate(b => b.UserId == bllUser.Id)
                .Select(b => new BidViewModel()
                {
                    Sum = b.Sum,
                    DateTime = b.DateTime,
                    Lot = lotService.GetById(b.LotId).ToLotViewModel(),
                    User = userService.GetById(b.UserId).ToUserViewModel()
                });
            return View("Index", bids);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(int id, int newBid)
        {
            BllLot bllLot = lotService.GetById(id);

            if (bllLot == null)
            {
                return HttpNotFound();
            }

            //Переделать условие, чтобы проверялось не первая ли это ставка
            if (newBid <= bllLot.CurrentPrice)
            {
                // Заменить на ошибку
                return HttpNotFound();
            }

            BllUser bllUser = userService.GetByPredicate(u => u.Email == HttpContext.User.Identity.Name).FirstOrDefault();

            if (bllUser == null)
            {
                ModelState.AddModelError(string.Empty, "Cannot create new bid");
            }
            else if (bllLot.OwnerId == bllUser.Id)
            {
                ModelState.AddModelError(string.Empty, "You Cannot create bid for your lot");
            }
            else if (bllLot.FinishDateTime < DateTime.Now)
            {
                ModelState.AddModelError(string.Empty, "Time is finished");
            }
            else
            {
                BllBid bllBid = new BllBid
                {
                    LotId = bllLot.Id,
                    UserId = bllUser.Id,
                    Sum = newBid,
                    DateTime = DateTime.Now
                };
                bidService.Create(bllBid);
                return RedirectToAction("Details", "Lot", new { id = bllBid.LotId });
            }
            return HttpNotFound();
            /*


            BidCreateModel bidCreate = new BidCreateModel()
            {
                Sum = bllLot.CurrentPrice,
                LotId = id
            };

            return View(bidCreate);
             * */
        }
        /*
        [HttpPost]
        [Authorize(Roles = "User")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BidCreateModel bid)
        {
            if (ModelState.IsValid)
            {
                BllLot bllLot = lotService.Get(bid.LotId);

                if (bid.Sum > bllLot.CurrentPrice)
                {
                    BllUser bllUser = userService.Get(u => u.Email == HttpContext.User.Identity.Name).FirstOrDefault();

                    if (bllUser == null)
                    {
                        ModelState.AddModelError(string.Empty, "Cannot create new bid");
                    }
                    else if (bllLot.FinishDateTime < DateTime.Now)
                    {
                        ModelState.AddModelError(string.Empty, "Time is finished");
                    }
                    else
                    {
                        BllBid bllBid = bid.ToBllBid();
                        bllBid.UserId = bllUser.Id;
                        bidService.Create(bllBid);
                        return RedirectToAction("Details", "Lot", new { id = bllBid.LotId });
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "You should to set greater sum");
                }
            }

            return View(bid);
        }
        */
    }
}
