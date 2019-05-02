using OrderManagment.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderManagment.Core.Models
{
    public class ProductDataModel
    {
        public Product Product { get; set; }
        public List<Category> Categories { get; set; } = null;
    }
}