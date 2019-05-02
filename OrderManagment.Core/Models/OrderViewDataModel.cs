using OrderManagment.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderManagment.Core.Models
{
    public class OrderViewDataModel
    {
        public Customer Customer { get; set; }
        public List<Customer> Customers { get; set; }
        public List<OrderViewListDataModel> OrderDetails { get; set; }
        public List<Order> Orders { get; set; }
    }
}