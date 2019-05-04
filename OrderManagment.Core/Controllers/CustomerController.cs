using OrderManagment.Core.Repositories;
using OrderManagment.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderManagment.Core.Controllers
{
    public class CustomerController : Controller
    {
        private IRepository<Customer> _customerServices;

        public CustomerController(IRepository<Customer> _customerServices)
        {
            this._customerServices = _customerServices;
        }
        // GET: Customer
        public ActionResult Index()
        {
            var DataList = _customerServices.GetAll("customer/getall");
            return View(DataList);
        }

        [HttpGet]
        public ActionResult CreateCustomer(int? id)
        {
            Customer model = new Customer();

            if (id != null)
                model = _customerServices.GetById((int)id, "customer/GetCustomerById/");
            else
                model = new Customer();

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCustomer(Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (customer.id != 0)
                    customer.UpdatedDate = DateTime.Now;
                else
                    customer.CreateDate = DateTime.Now;

                if (customer.id == 0)
                {
                    _customerServices.Insert(customer, "customer/createcustomer");
                }
                else
                {
                    _customerServices.Update(customer, "customer/updatecustomer");
                }

                return RedirectToAction("Index");

            }
            return View(customer);

        }
       
        public ActionResult DeleteCustomer(int id)
        {
            _customerServices.Delete(id, "customer/delete/");
            return RedirectToAction("Index");

        }
    }
}