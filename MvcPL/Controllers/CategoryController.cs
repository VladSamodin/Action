using BLL.Interface.Entities;
using BLL.Interface.Services;
using MvcPL.Models.Category;
using MvcPL.Infrastructure.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcPL.Controllers
{
    //[Authorize(Roles = "Administrator")]
    public class CategoryController : Controller
    {

        private IService<BllCategory> categoryService;
        // Вынести в метод Delete
        private ILotService lotService;

        public CategoryController(IService<BllCategory> categoryService, ILotService lotService)
        {
            this.categoryService = categoryService;
            this.lotService = lotService;
        }


        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View(categoryService.GetAll().Select(c => c.ToCategoryViewModel()));
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create(CategoryCreateModel category)
        {
            if (ModelState.IsValid)
            {
                categoryService.Create(category.ToBllCategory());
                RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id)
        {
            BllCategory toEdit = categoryService.GetById(id);
            if (toEdit == null)
            {
                return HttpNotFound();
            }
            return View(toEdit.ToCategoryEditModel());
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id, CategoryEditModel category)
        {
            if (ModelState.IsValid)
            {
                BllCategory toEdit = categoryService.GetById(id);
                if (toEdit == null)
                    return HttpNotFound();
                BllCategory categoryName = categoryService.GetByPredicate(c => c.Name == category.Name).FirstOrDefault();
                if (categoryName != null && categoryName.Id != toEdit.Id)
                    return HttpNotFound();
                toEdit.Name = category.Name;
                categoryService.Update(toEdit);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        //[HttpGet, HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id)
        {
            BllCategory toDelete = categoryService.GetById(id);
            if (toDelete == null)
                return HttpNotFound();
            if (Request.HttpMethod.ToUpper() == "GET")
                return View(toDelete.ToCategoryViewModel());
            // Вынести удаление лотов в логику сервиса Категорий?
            IEnumerable<BllLot> categoryLots = lotService.GetByPredicate(l => l.CategoryId == toDelete.Id).ToList();
            foreach(var lot in categoryLots)
            {
                lotService.Delete(lot);
            }
            categoryService.Delete(toDelete);
            //return View("Index");
            return RedirectToAction("Index");
        }


        public ActionResult ShowCategories()
        {
            IEnumerable<CategoryViewModel> list = categoryService.GetAll().Select(c => c.ToCategoryViewModel());
            return PartialView("_ListCategories", list);
        }

    }
}
