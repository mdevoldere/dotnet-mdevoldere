using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDevoldere.Domain.Models
{
    public class Product : Model
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public float Price { get; set; }

        public string? Description { get; set; }

        public Product() : this("Product", 0, null)
        {

        }

        public Product(string name, float price = 0, string? description = null)
        {
            Id = NewUid;
            Name = name;
            Price = price;
        }
    }
}
