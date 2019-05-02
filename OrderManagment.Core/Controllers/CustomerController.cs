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
        private ApiBusiness.CustomerApiCall CustomerApiCall;

        public CustomerController()
        {
            CustomerApiCall = new ApiBusiness.CustomerApiCall();
        }
        // GET: Customer
        public ActionResult Index()
        {
            var DataList = CustomerApiCall.GetAllCustomers();
            return View(DataList);
        }

        [HttpGet]
        public ActionResult CreateCustomer(int? id)
        {
            Customer model = new Customer();

            if (id != null)
                model = CustomerApiCall.GetCustomerById((int)id);
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
                    CustomerApiCall.Insert(customer);
                }
                else
                {
                    CustomerApiCall.Update(customer);
                }

                return RedirectToAction("Index");

            }
            return View(customer);

        }
       
        public ActionResult DeleteCustomer(int id)
        {
            CustomerApiCall.Delete(id);
            return RedirectToAction("Index");

        }
    }
}