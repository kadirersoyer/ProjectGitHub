using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderManagment.Core.Models
{
    public class OrderViewListDataModel
    {
        public int OrderId { get; set; }
        public string  CustomerName { get; set; }
        public string ProductName { get; set; }
        public decimal TotalPrice { get; set; }
        public double Quantity { get; set; }
        public int? OrderNo { get; set; }
        public int CustomerID { get; set; }
        public decimal Price { get; set; }
        public int ProductId { get; set; }
        public string CategoryName { get; set; }
    }
}