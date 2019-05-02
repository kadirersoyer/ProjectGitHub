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
    public class OrderController : ApiController
    {
        private OrderManagmentContext context;
        private IUnitOfWork _uow;
        private IRepository<Order> _orderRepository;
       
        public OrderController()
        {
            context = new OrderManagmentContext();
            _uow = new EFUnitOfWork(context);
            _orderRepository = new EFRepository<Order>(context);
        }
        [Route("order/getall")]
        public IEnumerable<Order> GetProducts()
        {
            return _orderRepository.GetAll();
        }
        [Route("order/createorder")]
        public void Create(Order order)
        {
            _orderRepository.Insert(order);
            _uow.SaveChanges();
        }
        [Route("order/updateorder")]
        public void Update(Order order)
        {
            _orderRepository.Update(order);
            _uow.SaveChanges();
        }
        [Route("order/GetOrderById/{id}")]
        public Order GetOrderById(int id)
        {
            return _orderRepository.GetById(id);
        }
        [Route("order/delete/{id}")]
        public void Delete(int id)
        {
            _orderRepository.Delete(id);
            _uow.SaveChanges();
        }
    }
}
