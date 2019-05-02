using OrderManagment.Core.Models;
using OrderManagment.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderManagment.Core.Controllers
{
    public class CategoryController : Controller
    {
        private ApiBusiness.CategoryApiCall categoryApiCal;

        public CategoryController()
        {
            categoryApiCal = new ApiBusiness.CategoryApiCall();
        }
        // GET: Category
        public ActionResult Index()
        {
            var DataList = categoryApiCal.GetAllCategories();
            return View(DataList);
        }

        [HttpGet]
        public ActionResult CreateCategory(int? id)
        {
            Category model = new Category();

            if (id != null)
                model = categoryApiCal.GetCategoryById((int)id);
            else
                model = new Category();

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory(Category Category)
        {
            if (ModelState.IsValid)
            {
                if (Category.id != 0)
                    Category.UpdatedDate = DateTime.Now;
                else
                    Category.CreateDate = DateTime.Now;

                if (Category.id == 0)
                {
                    categoryApiCal.Insert(Category);
                }
                else
                {
                    categoryApiCal.Update(Category);
                }

                return RedirectToAction("Index");
            }
            return View(Category);

        }
       
        public ActionResult DeleteCategory(int id)
        {
            categoryApiCal.Delete(id);
            return RedirectToAction("Index");

        }
    }
}