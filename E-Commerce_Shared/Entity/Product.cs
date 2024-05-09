using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Shared.Entity
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        [Required]
        [Column(TypeName ="decimal(18,2)")]
        public decimal Price { get; set; }  
        public int CategoryId { get; set; }
        public Category Category { get; set; }  
        public string? ImageUrl { get; set; }
    }
}
