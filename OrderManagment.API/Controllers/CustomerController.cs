using OrderManagment.API.Data.Repositories;
using OrderManagment.API.Models;
using OrderManagment.API.UnitOfWork;
using OrderManagment.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OrderManagment.API.Controllers
{
    public class CustomerController : ApiController
    {
        private OrderManagmentContext context;
        private IUnitOfWork _uow;
        private IRepository<Customer> _customerRepository;

        public CustomerController()
        {

            context = new OrderManagmentContext();
            _uow = new EFUnitOfWork(context);
            _customerRepository = new EFRepository<Customer>(context);

        }
        [Route("customer/getall")]
        public IEnumerable<Customer> GetCustomer()
        {
            return _customerRepository.GetAll();
        }
        [Route("customer/createcustomer")]
        public void Create(Customer Customer)
        {
            _customerRepository.Insert(Customer);
            _uow.SaveChanges();
        }
        [Route("customer/GetCustomerById/{id}")]
        public Customer GetCustomerById(int id)
        {
            return _customerRepository.GetById(id);
        }
        [Route("customer/updatecustomer")]
        public void Update(Customer Customer)
        {
            _customerRepository.Update(Customer);
            _uow.SaveChanges();
        }
        [Route("customer/delete/{id}")]
        public void Delete(int id)
        {
            _customerRepository.Delete(id);
            _uow.SaveChanges();
        }
    }
}
