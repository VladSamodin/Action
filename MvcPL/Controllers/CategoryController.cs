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
    public class CategoryController : Controller
    {

        private IService<BllCategory> categoryService;
        
        public CategoryController(IService<BllCategory> categoryService)
        {
            this.categoryService = categoryService;
        }
        
        
        /*
        public ActionResult Index()
        {
            return View();
        }
         * */

        public ActionResult ShowCategories()
        {
            IEnumerable<CategoryViewModel> list = categoryService.GetAll().Select(c => c.ToCategoryViewModel());
            return PartialView("_ListCategories", list);
        }

    }
}
