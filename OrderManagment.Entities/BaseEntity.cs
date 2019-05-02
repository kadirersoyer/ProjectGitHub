using System;
using System.ComponentModel.DataAnnotations;

namespace OrderManagment.Entities
{
    public abstract class BaseEntity
    {
        public int id { get; set; }
        [Display(Name = "Adı : ")]
        [Required(ErrorMessage = "Ad Alanı Zorunludur.")]
        public string name { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
