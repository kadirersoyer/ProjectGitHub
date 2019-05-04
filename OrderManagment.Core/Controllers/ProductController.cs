
using OrderManagment.Core.Models;
using OrderManagment.Core.Repositories;
using OrderManagment.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OrderManagment.Core.Controllers
{
    public class ProductController : Controller
    {
        private IRepository<Product> _productServices;
        private IRepository<Category> _categoryServices;
        private IProductViewRepository _productViewDataServices;

        public ProductController(IRepository<Product> _productServices, 
            IRepository<Category> _categoryServices,IProductViewRepository _productViewDataServices)
        {
            this._productServices = _productServices;
            this._categoryServices = _categoryServices;
            this._productViewDataServices = _productViewDataServices;
        }
        // GET: Product
        public ActionResult Index()
        {
            var viewedata = _productViewDataServices.GetProductDetailLists("product/getproductdatalist");
            return View(viewedata);
           
        }
        [HttpGet]
        public ActionResult CreateProduct(int? id)
        {
            ProductDataModel model = new ProductDataModel();

            if (id != null)
                model.Product = _productServices.GetById((int)id, "product/GetProductById");
            else
                model.Product = new Product();

            model.Categories = _categoryServices.GetAll("category/getall") as List<Category>;

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
                    _productServices.Insert(ProductDataModel.Product, "product/createproduct");
                }
                else
                {
                    _productServices.Update(ProductDataModel.Product, "product/updateproduct");
                }

                return RedirectToAction("Index");

            }
            else
            {
                ProductDataModel.Categories = _categoryServices.GetAll("category/getall") as List<Category>;
            }
            return View(ProductDataModel);

        }
       
        public ActionResult DeleteProduct(int id)
        {
            _categoryServices.Delete(id, "category/delete/");
            return RedirectToAction("Index");

        }
    }
}