
using OrderManagment.Core.Models;
using OrderManagment.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OrderManagment.Core.Controllers
{
    public class ProductController : Controller
    {
        private ApiBusiness.ProductApiCall ProductApiCall;
        private ApiBusiness.CategoryApiCall CategoryApiCall;

        public ProductController()
        {
            ProductApiCall = new ApiBusiness.ProductApiCall();
            CategoryApiCall = new ApiBusiness.CategoryApiCall();
        }
        // GET: Product
        public ActionResult Index()
        {
            var products = ProductApiCall.GetProducts();
            var categories = CategoryApiCall.GetAllCategories();

            var DataList = (from p in products
                            join c in categories on p.CategoryID equals c.id
                            select new ProductListDataModel
                            {
                                CategoryName = c.name,
                                id = p.id,
                                CreateDate = p.CreateDate,
                                Price = p.Price,
                                name = p.name
                            }).ToList();


            return View(DataList);
        }
        [HttpGet]
        public ActionResult CreateProduct(int? id)
        {
            ProductDataModel model = new ProductDataModel();

            if (id != null)
                model.Product = ProductApiCall.GetProductById((int)id);
            else
                model.Product = new Product();

            model.Categories = CategoryApiCall.GetAllCategories() as List<Category>;

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProduct(ProductDataModel ProductDataModel)
        {
            if (ModelState.IsValid)
            {
                if (ProductDataModel.Product.id != 0)
                    ProductDataModel.Product.UpdatedDate = DateTime.Now;
                else
                    ProductDataModel.Product.CreateDate = DateTime.Now;

                if (ProductDataModel.Product.id == 0)
                {
                    ProductApiCall.Insert(ProductDataModel.Product);
                }
                else
                {
                    ProductApiCall.Update(ProductDataModel.Product);
                }

                return RedirectToAction("Index");

            }
            else
            {
                ProductDataModel.Categories = CategoryApiCall.GetAllCategories() as List<Category>;
            }
            return View(ProductDataModel);

        }
       
        public ActionResult DeleteProduct(int id)
        {
            ProductApiCall.Delete(id);
            return RedirectToAction("Index");

        }
    }
}