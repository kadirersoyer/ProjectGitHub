using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagment.Entities.Entities
{
    [NotMapped]
    public class ProductDetailList : Product
    {
        public string CategoryName { get; set; }
    }
}
