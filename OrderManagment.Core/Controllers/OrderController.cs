using OrderManagment.Core.Models;
using OrderManagment.Core.Repositories;
using OrderManagment.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OrderManagment.Core.Controllers
{
    public class OrderController : Controller
    {
      
        private IRepository<Product> _productServices;
        private IRepository<Customer> _customerServices;
        private IRepository<Order> _orderServices;
        private IRepository<Category> _categoryServices;
      
        public OrderController(IRepository<Product> _productServices,
                    IRepository<Customer> _customerServices, 
                    IRepository<Order> _orderRepository,
                    IRepository<Category> _categoryServices)
        {

            this._productServices = _productServices;
            this._customerServices = _customerServices;
            this._orderServices = _orderRepository;
            this._categoryServices = _categoryServices;
        }
        // GET: Order
        public ActionResult Index()
        {
            var products = _productServices.GetAll("product/getall");
            var customers = _customerServices.GetAll("customer/getall");
            var orders = _orderServices.GetAll("order/getall");

            var data = (from p in products
                        join o in orders on p.id equals o.ProductID
                        join c in customers on o.CustomerID equals c.id
                        select new OrderViewListDataModel
                        {
                            CustomerName = c.name,
                            OrderId = o.id,
                            ProductName = p.name,
                            Quantity = o.Quantity,
                            TotalPrice = o.TotalPrice,
                            OrderNo = o.OrderNo,
                            Price = p.Price
                        }).OrderByDescending(a=>a.OrderNo).ToList();


            return View(data);
        }
        [HttpGet]
        public ActionResult CreateOrder(int? id)
        {
            OrderViewDataModel model = new OrderViewDataModel();

            var products = _productServices.GetAll("product/getall");
            var customers = _customerServices.GetAll("customer/getall");
            var orders = _orderServices.GetAll("order/getall");
            var categories = _categoryServices.GetAll("category/getall");

            var data = (from p in products
                        join o in orders on p.id equals o.ProductID
                        join c in customers on o.CustomerID equals c.id
                        join cp in categories on p.CategoryID equals cp.id
                        where o.OrderNo == id
                        select new OrderViewListDataModel
                        {
                            CustomerName = c.name,
                            OrderId = o.id,
                            ProductName = p.name,
                            Quantity = o.Quantity,
                            TotalPrice = o.TotalPrice,
                            OrderNo = o.OrderNo,
                            Price = p.Price,
                            CustomerID = c.id,
                            ProductId = p.id,
                            CategoryName = cp.name
                        }).ToList();

            if (data.Count > 0)
            {
                model.Customer = new Customer()
                {
                    id = data.FirstOrDefault().CustomerID,
                    name = data.FirstOrDefault().CustomerName
                };
            }
            else
            {
                model.Customer = new Customer();
            }

            model.OrderDetails = data;

            model.Customers = _customerServices.GetAll("customer/getall") as List<Customer>;

            return View(model);
        }
        [HttpPost]
        public JsonResult CreateOrder(List<Order> order)
        {
            DbResult result = new DbResult();

            try
            {
              
                int no = order.FirstOrDefault().OrderNo;

                var Orders = _orderServices.GetAll("order/getall");

                var DBRows = Orders.Where(a => a.OrderNo == no);

                var newRows = order;

                if (no == 0)
                {
                    if (Orders.Count() == 0)
                    {
                        no = 1;
                    }
                    else
                    {
                        no = Orders.LastOrDefault().OrderNo + 1;
                    }
                }
                // InsertedRows
                foreach (var item in newRows)
                {
                    var data = DBRows.Where(a => a.id == item.id).SingleOrDefault();
                    if (data == null)
                    {
                        item.name = "Satış";
                        //item.OrderNo = guid;
                        item.OrderNo = no;
                        _orderServices.Insert(item,"order/create");
                    }
                }
                // UpdateRows
                foreach (var item in newRows)
                {
                    var data = DBRows.Where(a => a.id == item.id).SingleOrDefault();
                    if (data != null)
                    {
                        item.name = "Satış";
                        _orderServices.Update(item,"order/update");
                    }
                }
                // DeletedRows

                foreach (var item in DBRows)
                {
                    var data = newRows.Where(a => a.id == item.id).SingleOrDefault();
                    if (data == null)
                    {
                        _orderServices.Delete(item.id,"order/delete");
                    }
                }
                result.message = "İşlem Tamamlandı";
                result.status = true;
            }
            catch (Exception ex)
            {
                result.data = null;
                result.message = ex.Message.ToString();
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public PartialViewResult GetProductList()
        {
            var products = _productServices.GetAll("product/getall");
            var categories = _categoryServices.GetAll("category/getall");

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

            return PartialView("_ProductList", DataList);
        }
    }
}