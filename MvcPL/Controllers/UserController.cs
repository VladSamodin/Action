using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models;
using MvcPL.Models.User;
using System.Web.Security;
using MvcPL.Providers;

namespace MvcPL.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService service;

        public UserController(IUserService service)
        {
            this.service = service;
        }

        [ActionName("Index")]
        [Authorize(Roles = "Administrator")]
        public ActionResult GetAllUsers()
        {
            return View(service.GetAll().Select(user => user.ToMvcUser()));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserCreateModel userCreateModel)
        {
            //service.Create(userViewModel.ToBllUser());
            //return RedirectToAction("Index");

            if (ModelState.IsValid)
            {
                if (service.GetByPredicate(u => u.Email == userCreateModel.Email).FirstOrDefault() != null)
                {
                    ModelState.AddModelError("Email", "Email already registered");
                }
                else if (((CustomMembershipProvider)Membership.Provider).CreateUser(userCreateModel) == null)
                {
                    ModelState.AddModelError(string.Empty, "Registration error");
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(userCreateModel.Email, false);
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(userCreateModel);
        }

        //GET-запрос к методу Delete несет потенциальную уязвимость!
        [HttpGet]
        public ActionResult Delete(int id = 0)
        {
            BllUser user = service.GetById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user.ToMvcUser());
        }
        
        //Post/Redirect/Get (PRG) — модель поведения веб-приложений, используемая
        //разработчиками для защиты от повторной отправки данных веб-форм
        //(Double Submit Problem)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(BllUser user)
        {
            service.Delete(user);
            return RedirectToAction("Index");
        }



        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserLoginModel user, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(user.Email, user.Password))
                {
                    FormsAuthentication.SetAuthCookie(user.Email, user.Remember);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Wrong email or password");
                }
            }

            return View(user);
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //[Authorize(Roles = "User, Admin, Moderator")]
        [Authorize]
        public ActionResult Details(int id)
        {
            BllUser bllUser = service.GetById(id);

            if (bllUser == null)
            {
                return HttpNotFound();
            }
            else
            {
                //return View(bllUser.ToUserViewModel());
                return View(MvcPL.Infrastructure.Mappers.MvcMappers.ToMvcUser(bllUser));
            }
        }

        [Authorize(Roles = "User")]
        public ActionResult Lots()
        {
            // name == email????
            BllUser bllUser = service.GetByPredicate(u => u.Email == User.Identity.Name).FirstOrDefault();

            if (bllUser == null)
            {
                return HttpNotFound();
            }

           return RedirectToAction("Search", "Lot", new {userId = bllUser.Id});
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public ActionResult Private()
        {
            BllUser bllUser = service.GetByPredicate(u => u.Email == User.Identity.Name).FirstOrDefault();

            if (bllUser == null)
            {
                return HttpNotFound();
            }
            return View(bllUser.ToUserEditModel());   
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public ActionResult Private(UserEditModel userEditModel)
        {
            // name == email????
            /*
            BllUser bllUser = service.GetByPredicate(u => u.Email == User.Identity.Name).FirstOrDefault();

            if (bllUser == null)
            {
                return HttpNotFound();
            }
            return HttpNotFound();

            */
            if (ModelState.IsValid)
            {
                if (service.GetByPredicate(u => u.Email == userEditModel.Email).FirstOrDefault() != null && userEditModel.Email != User.Identity.Name)
                {
                    ModelState.AddModelError("Email", "Email already registered");
                }
                else if (((CustomMembershipProvider)Membership.Provider).UpdateUserData(userEditModel, User.Identity.Name) == null)
                {
                    ModelState.AddModelError(string.Empty, "Update error");
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(userEditModel.Email, false);
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(userEditModel);
            
        }
    }
}