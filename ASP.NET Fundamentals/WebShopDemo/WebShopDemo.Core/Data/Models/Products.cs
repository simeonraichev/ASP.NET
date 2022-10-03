using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopDemo.Core.Data.Models
{
    [Comment("Products to sell")]
    public class Products
    {
        [Key]
        [Comment("Primary key")]
        public Guid Id { get; set; }

        [Required]
        [Comment("Name of the item")]
        public string Name { get; set; }

        [Required]
        [Comment("Price of the item")]
        public decimal Price { get; set; }

        [Required]
        [Comment("Quantity of the item")]
        public int Quantity { get; set; }
    }
}
