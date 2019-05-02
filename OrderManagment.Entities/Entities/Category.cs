using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagment.Entities.Entities
{
    public class Category : BaseEntity
    {
        public Category()
        {
            Products = new List<Product>();
        }
        [Display(Name = "Açıkalama :")]
        [Required(ErrorMessage = "Açıklama Alanı Zorunludur...")]
        public string Defination { get; set; }
        public List<Product> Products { get; set; }
    }
}
