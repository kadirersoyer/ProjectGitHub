using OrderManagment.Core.Models;
using OrderManagment.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OrderManagment.Core.Controllers
{
    public class OrderController : Controller
    {
        private ApiBusiness.ProductApiCall ProductApiCall;
        private ApiBusiness.CustomerApiCall CustomerApiCall;
        private ApiBusiness.OrderApiCall OrderApiCall;
        private ApiBusiness.CategoryApiCall CategoryApiCall;

        public OrderController()
        {
            ProductApiCall = new ApiBusiness.ProductApiCall();
            CustomerApiCall = new ApiBusiness.CustomerApiCall();
            OrderApiCall = new ApiBusiness.OrderApiCall();
            CategoryApiCall = new ApiBusiness.CategoryApiCall();
        }
        // GET: Order
        public ActionResult Index()
        {
            var products = ProductApiCall.GetProducts();
            var customers = CustomerApiCall.GetAllCustomers();
            var orders = OrderApiCall.GetAllOrders();

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

            var products = ProductApiCall.GetProducts();
            var customers = CustomerApiCall.GetAllCustomers();
            var orders = OrderApiCall.GetAllOrders();
            var categories = CategoryApiCall.GetAllCategories();

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

            model.Customers = CustomerApiCall.GetAllCustomers() as List<Customer>;

            return View(model);
        }
        [HttpPost]
        public JsonResult CreateOrder(List<Order> order)
        {
            DbResult result = new DbResult();

            try
            {
              
                int no = order.FirstOrDefault().OrderNo;

                var Orders = OrderApiCall.GetAllOrders();

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
                        OrderApiCall.Insert(item);
                    }
                }
                // UpdateRows
                foreach (var item in newRows)
                {
                    var data = DBRows.Where(a => a.id == item.id).SingleOrDefault();
                    if (data != null)
                    {
                        item.name = "Satış";
                        OrderApiCall.Update(item);
                    }
                }
                // DeletedRows

                foreach (var item in DBRows)
                {
                    var data = newRows.Where(a => a.id == item.id).SingleOrDefault();
                    if (data == null)
                    {
                        OrderApiCall.Delete(item.id);
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

            return PartialView("_ProductList", DataList);
        }
    }
}