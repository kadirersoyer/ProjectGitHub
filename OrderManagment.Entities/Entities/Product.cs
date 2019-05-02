using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagment.Entities.Entities
{
    public class Product : BaseEntity
    {
        
        [Display(Name = "Fiyat")]
        [Required(ErrorMessage = "Fiyat Alanı Zorunludur.")]
        public decimal Price  { get; set; }

        [Display(Name = "Kategori :")]
        [Required(ErrorMessage = "Kategori Zorunludur.")]

        public int CategoryID { get; set; }

        public Category Category { get; set; }

        public ICollection<Order> Orders { get; set; }

    }
}
