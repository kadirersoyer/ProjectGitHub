using OrderManagment.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderManagment.Core.Models
{
    public class ProductListDataModel: Product
    {
        public string CategoryName { get; set; }
    }
}