using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagment.Entities.Entities
{
    public class Order: BaseEntity
    {
        public int OrderNo { get; set; }

        public decimal TotalPrice { get; set; }

        public double Quantity { get; set; }

        public int CustomerID { get; set; }

        public Customer Customer { get; set; }

        public int ProductID{ get; set; }

        public Product Product { get; set; }
    }
}
