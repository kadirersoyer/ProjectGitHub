using System.Collections.Generic;

namespace OrderManagment.Entities.Entities
{
    public class Customer : BaseEntity
    {
        public string Adress { get; set; }
       
        public ICollection<Order> Orders { get; set; }
    }
}
