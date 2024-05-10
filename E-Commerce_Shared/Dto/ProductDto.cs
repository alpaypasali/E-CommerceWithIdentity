using E_Commerce_Shared.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Shared.Dto
{
    public class ProductDto
    {

        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
       
        public string? ImageUrl { get; set; }
    }
}
