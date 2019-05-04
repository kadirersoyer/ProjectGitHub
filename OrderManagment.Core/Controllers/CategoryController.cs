using OrderManagment.Core.Models;
using OrderManagment.Core.Repositories;
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
        private IRepository<Category> _categoryServices;

        public CategoryController(IRepository<Category> _categoryServices)
        {
            this._categoryServices = _categoryServices;
        }
        // GET: Category
        public ActionResult Index()
        {
            var DataList = _categoryServices.GetAll("category/getall");
            return View(DataList);
        }

        [HttpGet]
        public ActionResult CreateCategory(int? id)
        {
            Category model = new Category();

            if (id != null)
                model = _categoryServices.GetById((int)id, "category/GetCategoryById/");
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
                    _categoryServices.Insert(Category, "category/createcategory");
                }
                else
                {
                    _categoryServices.Update(Category, "category/updatecategory");
                }

                return RedirectToAction("Index");
            }
            return View(Category);

        }
       
        public ActionResult DeleteCategory(int id)
        {
            _categoryServices.Delete(id, "category/delete/");
            return RedirectToAction("Index");

        }
    }
}